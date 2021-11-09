using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

using TaCertoForms.Attributes;
using TaCertoForms.Controllers.Base;
using TaCertoForms.Models;

namespace TaCertoForms.Controllers {
    [SomenteLogado]
    public class PessoaController : ControladoraBase {
        [Perfil(Perfil.Administrador)]
        public ActionResult Index() {
            List<ViewModelPessoa> pessoas = new List<ViewModelPessoa>();
            foreach(var pessoa in Collection.PessoaList()) {
                ViewModelPessoa vmPessoa = new ViewModelPessoa();
                vmPessoa.Pessoa = pessoa;
                Instituicao i = Collection.FindInstituicao(pessoa.IdInstituicao);
                if(i != null) {
                    vmPessoa.Instituicao.Add(i);
                    pessoas.Add(vmPessoa);
                }
            }
            return View(pessoas);
        }

        [Perfil(Perfil.Administrador)]
        public ActionResult Create() {
            List<Instituicao> list = Collection.InstituicaoList();
            if(list == null)
                list = new List<Instituicao>();
            ViewBag.InstituicaoList = new SelectList(list, "IdInstituicao", "NomeFantasia");
            return View();
        }

        [HttpPost]
        [Perfil(Perfil.Administrador)]
        public ActionResult Create(Pessoa pessoa) {
            pessoa = Collection.CreatePessoa(pessoa);
            if(pessoa != null) { 
                TempData["success"] = "Usu�rio cadastrado com sucesso.";
                return RedirectToAction("Edit", "Pessoa", new { id = pessoa.IdPessoa });
            }
            return View(pessoa);
        }
        [Perfil(Perfil.Administrador, Perfil.Autor)]
        public ActionResult Edit(int? id){
            if(id == null) {
                TempData["error"] = "Ocorreu um erro inesperado. Entre em contato com o administrador do sistema.";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(Session["Perfil"].Equals(Perfil.Autor) && (int)Session["IdPessoa"] != id) {
                TempData["error"] = "Voc� n�o tem permiss�o para editar esse usu�rio.";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = Collection.FindPessoa(id);
            if(pessoa == null) {
                TempData["error"] = "Usu�rio n�o encontrado.";
                return HttpNotFound();
            };
            List<Instituicao> list = Collection.InstituicaoList();
            ViewBag.InstituicaoList = new SelectList(list, "IdInstituicao", "NomeFantasia");
            if(pessoa == null) {
                TempData["error"] = "Ocorreu um erro inesperado. Entre em contato com o administrador do sistema.";
                return HttpNotFound();
            }
            ViewBag.Midia = Collection.FindMidia(id, "Pessoa");
            return View(pessoa);
        }

        [HttpPost]
        [Perfil(Perfil.Administrador, Perfil.Autor)]
        public ActionResult Edit(Pessoa pessoa) {
            Pessoa current = Collection.FindPessoa(pessoa.IdPessoa);
            if(current == null) {
                TempData["error"] = "Ocorreu um erro inesperado. Entre em contato com o administrador do sistema.";
                return HttpNotFound();
            }
            pessoa.Perfil = current.Perfil;

            if(pessoa.Senha == null) 
                pessoa.Senha = current.Senha;
            
            if(Collection.EditPessoa(pessoa) != null){
                if(pessoa.IdPessoa == (int)Session["IdPessoa"])
                    Session["NomeUsuario"] = pessoa.Nome;
                TempData["success"] = "Usu�rio atualizado com sucesso.";
                return RedirectToAction("Index");
            }
            return View(pessoa);
        }

        /*[Perfil(Perfil.Administrador)]
        public ActionResult Delete(int? id) {
            Pessoa pessoa = Collection.FindPessoa(id);
            if(pessoa == null)
                return HttpNotFound();
            return View(pessoa);
        }

        [Perfil(Perfil.Administrador)]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            Collection.DeletePessoa(id);
            return RedirectToAction("Index");
        }*/
    }
}