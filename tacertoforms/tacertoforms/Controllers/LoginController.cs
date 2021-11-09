using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

using TaCertoForms.Attributes;
using TaCertoForms.Contexts;
using TaCertoForms.Controllers.Base;
using TaCertoForms.Models;

namespace TaCertoForms.Controllers {
    [SomenteDeslogado]
    public class LoginController : ControladoraBase {
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Autenticar(string email, string senha) {
            Pessoa pessoa = db.Pessoa.Where(p => p.Email == email && p.Senha == senha).FirstOrDefault();
            ViewBag.ToastMessage = null;

            if(pessoa == null) {
                Session["Message"] = "Login ou senha inválidos!";
                return RedirectToRoute(
                    new RouteValueDictionary {
                        { "controller", "Login" },
                        { "action", "Index" }
                    }
                );
            }
            else {
                if(pessoa.Perfil.Equals(Perfil.Aluno)) {
                    Session["Message"] = "Login ou senha inválidos!";
                    return RedirectToRoute(new RouteValueDictionary {
                        { "controller", "Login" },
                        { "action", "Index" }
                    });
                }
                Instituicao instituicao = db.Instituicao.Find(pessoa.IdInstituicao);
                Session["Logado"] = true;
                Session["IdPessoa"] = pessoa.IdPessoa;
                Session["IdMatriz"] = GetIdMatriz(pessoa);
                Session["NomeUsuario"] = pessoa.Nome;
                Session["IdInstituicao"] = pessoa.IdInstituicao;
                Session["NomeInstituicao"] = instituicao.NomeFantasia;
                Session["Perfil"] = pessoa.Perfil;
                Session["Message"] = null;

                SaveLogLogin();

                Midia midia = db.Midia.Where(x => x.IdOrigem == pessoa.IdPessoa && x.Tabela == "Pessoa").FirstOrDefault();
                if(midia != null)
                    Session["FotoPerfil"] = midia.Tabela + '/' + midia.IdMidia + midia.Extensao;
                return RedirectToRoute(new RouteValueDictionary {
                    { "controller", "Home" },
                    { "action", "Index" }
               });
            }
        }

        public ActionResult LogOff() {
            Session["Logado"] = null;
            Session["IdPessoa"] = null;
            Session["IdMatriz"] = null;
            Session["NomeUsuario"] = null;
            Session["IdInstituicao"] = null;
            Session["NomeInstituicao"] = null;
            Session["FotoPerfil"] = null;
            Session["Perfil"] = null;

            return RedirectToRoute(
                new RouteValueDictionary {
                    { "controller", "Login" },
                    { "action", "Index" }
                }
            );
        }

        private int GetIdMatriz(Pessoa p) {
            Instituicao i = db.Instituicao.Find(p.IdInstituicao);
            if(i.IsMatriz)
                return i.IdInstituicao;
            else {
                i = db.Instituicao.Find(i.IdMatriz);
                return i.IdInstituicao;
            }
        }
        public ActionResult EsqueciSenha() {
            return View();
        }

        public ActionResult Token(string token) {
            Pessoa pessoa = db.Pessoa.Where(x => x.Token == token).FirstOrDefault();
            if(pessoa == null)
                TempData["error"] = "Token inválido ou expirado. Solicite novamente sua redefinição de senha";
            return View();
        }

        [HttpPost]
        public ActionResult Token(string senha, string token) {
            Pessoa pessoa = db.Pessoa.Where(x => x.Token == token).FirstOrDefault();

            if(pessoa == null || pessoa.Token == null || pessoa.TokenDate == null)
                TempData["error"] = "Token inválido ou expirado. Solicite novamente sua redefinição de senha";
            else {
                DateTime tokenDate = (DateTime) pessoa.TokenDate;
                if(DateTime.Now <= tokenDate.AddDays(+1)) {
                    pessoa.Senha = senha;
                    pessoa.Token = null;
                    pessoa.TokenDate = null;

                    Context db = new Context();
                    db.Entry(pessoa).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    db.Dispose();

                    TempData["success"] = "Sua senha foi redefinida com sucesso.";
                    return RedirectToRoute(new RouteValueDictionary {
                        { "controller", "Login" },
                        { "action", "Index" }
                    });
                }
                else
                    TempData["error"] = "Token inválido ou expirado. Solicite novamente sua redefinição de senha";
            }
            return View();
        }

        [HttpPost]
        public ActionResult EsqueciSenha(string email) {
            Pessoa pessoa = db.Pessoa.Where(p => p.Email == email).FirstOrDefault();
            if(pessoa == null)
                TempData["error"] = "Nenhum usuário encontrado.";
            else {
                if (SendEmail(email, this.tokenGenerate(50, pessoa)))
                    TempData["success"] = "Enviamos um e-mail para \"" + email +"\" com as orientações de redefinição de senha.";
                else
                    TempData["error"] = "Ocorreu um erro inesperado. Por favor, entre em contato com o administrador do sistema.";
            }
            return RedirectToRoute(new RouteValueDictionary {
                    { "controller", "Login" },
                    { "action", "EsqueciSenha" }
            });
        }
        protected bool SendEmail(string email, string token) {
            StreamReader reader = new StreamReader(Server.MapPath("~/Views/Login/EmailTemplate.cshtml"));
            string body = reader.ReadToEnd();

            Uri myuri = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            body = body.Replace("{Url}", myuri.ToString().Replace(myuri.PathAndQuery, "") + "/Login/Token?token="+ token);
            return this.SendHtmlFormattedEmail("Redefinição de Senha!", body, email);
        }
        private bool SendHtmlFormattedEmail(string subject, string message, string email) {
            try {
                var senderEmail = new MailAddress(ConfigurationManager.AppSettings["Username"], "Tá Certo Forms");
                var receiverEmail = new MailAddress(email);
                var password = ConfigurationManager.AppSettings["Password"];
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient {
                    Host = ConfigurationManager.AppSettings["Host"],
                    Port = int.Parse(ConfigurationManager.AppSettings["Port"]),
                    EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using(var mess = new MailMessage(senderEmail, receiverEmail) {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                }) {
                    smtp.Send(mess);
                }
                return true;
            }
            catch(Exception) {
                TempData["error"] = "Ocorreu um erro inesperado. Por favor, entre em contato com o administrador do sistema.";
            }
            return false;
        }
        private string tokenGenerate(int size, Pessoa pessoa) {
            if(pessoa == null) return null;
            // Characters except I, l, O, 1, and 0 to decrease confusion when hand typing tokens
            var charSet = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var chars = charSet.ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach(var b in data)
                result.Append(chars[b % (chars.Length)]);

            pessoa.Token = result.ToString();
            pessoa.TokenDate = DateTime.Now;

            Context db = new Context();
            db.Entry(pessoa).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            db.Dispose();

            return result.ToString();
        }

        private void SaveLogLogin() {
            Context db_local = new Context();
            LogLogin logLogin = new LogLogin();
            logLogin.IdPessoa = (int)Session["IdPessoa"];
            logLogin.HoraAcesso = DateTime.Now;
            logLogin.Plataforma = "Navegador";
            logLogin.DeviceId = "";
            logLogin.DeviceIp = Request.UserHostAddress;
            logLogin.Origem = Origem.TaCertoForms;
            db_local.LogLogin.Add(logLogin);
            db_local.SaveChanges();
            db_local.Dispose();
        }
    }  
}