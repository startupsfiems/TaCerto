using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

using TaCertoForms.Attributes;
using TaCertoForms.Contexts;
using TaCertoForms.Controllers.Base;
using TaCertoForms.Models;

namespace TaCertoForms.Controllers {
    [SomenteLogado]
    public class MidiaController : ControladoraBase {
        private TipoMidia GetTipoArquivo(string contentType) {
            switch(contentType) {
                case "image/png":
                case "image/jpg":
                case "image/jpeg":
                    return TipoMidia.Imagem;
                case "ytb":
                    return TipoMidia.YouTube;
                default: return TipoMidia.Indefinido;
            }
        }

        public JsonResult Index(int IdOrigem, string Tabela) {
            return Json(Collection.FindMidia(IdOrigem, Tabela), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(int id, string tabela) {
            try {
                if(!Collection.HasPermissionMidia(id, tabela))
                    throw new UnauthorizedAccessException();
                List<Midia> arquivos = new List<Midia>();
                for(int i = 0; i < Request.Files.Count; i++) {
                    var file = Request.Files[i];
                    var hash = Guid.NewGuid();
                    if(file != null && file.ContentLength > 0) {
                        Midia fileDetail = new Midia() {
                            IdMidia = hash,
                            IdOrigem = id,
                            Tabela = tabela,
                            Filename = Path.GetFileName(file.FileName),
                            Extensao = Path.GetExtension(Path.GetFileName(file.FileName)),
                            Tipo = GetTipoArquivo(file.ContentType)
                        };
                        arquivos.Add(fileDetail);
                        string caminho = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/upload/" + tabela + '/');

                        var path = Path.Combine(caminho, fileDetail.IdMidia + fileDetail.Extensao);
                        if(!Directory.Exists(caminho))
                            Directory.CreateDirectory(caminho);
                        file.SaveAs(path);
                        Collection.CreateMidia(id, tabela, fileDetail);

                        if(tabela == "Pessoa" && id == (int)Session["IdPessoa"])
                            Session["FotoPerfil"] = fileDetail.Tabela + '/' + fileDetail.IdMidia + fileDetail.Extensao;

                        if(tabela == "Instituicao" || tabela == "Pessoa" || tabela == "Questao") { //Caso tenha alguma m??dia j?? salva
                            Context db = new Context();
                            Midia midiaApagar = db.Midia.Where(x => x.IdMidia != fileDetail.IdMidia && x.IdOrigem == id && x.Tabela == tabela).FirstOrDefault<Midia>();
                            if (midiaApagar != null && midiaApagar.IdMidia != hash)
                                Delete(midiaApagar.IdMidia);
                        }
                    }
                }
                return Json(arquivos);
            }
            catch(Exception) {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Erro no upload");
            }
        }
        public bool Delete(Guid id) {
            return Collection.DeleteMidia(id);
        }
    }
}