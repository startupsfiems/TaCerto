using System.Net;
using System.Web.Mvc;

using TaCertoForms.Attributes;
using TaCertoForms.Controllers.Base;
using TaCertoForms.Models;

namespace TaCertoForms.Controllers {
    [SomenteLogado]
    public class InstituicaoController : ControladoraBase {
        [Perfil(Perfil.Administrador)]
        public ActionResult Index() {
            return View(Collection.InstituicaoList());
        }

        [Perfil(Perfil.Administrador)]
        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [Perfil(Perfil.Administrador)]
        public ActionResult Create(ViewModelInstituicao viewModel) {
            //Todo validar se algum field veio null
            Endereco principal = viewModel.enderecoPrincipal;
            Collection.CreateEndereco(principal);
            //Capturando o id do endereço principal que foi inserido no banco
            int IdEnderecoPrincipal = principal.IdEndereco;
            int IdEnderecoCobranca;
            Endereco cobranca = viewModel.enderecoCobranca;
            if(cobranca != null) {
                Collection.CreateEndereco(cobranca);
                //Capturando o id do endereço de cobrança que foi inserido no banco
                IdEnderecoCobranca = cobranca.IdEndereco;
            }
            else
                IdEnderecoCobranca = IdEnderecoPrincipal;
            Instituicao instituicao = viewModel.instituicao;
            instituicao.IdEnderecoCobranca = IdEnderecoCobranca;
            instituicao.IdEnderecoPrincipal = IdEnderecoPrincipal;
            Collection.CreateInstituicao(instituicao);
            TempData["success"] = "Instituição cadastrada com sucesso!";
            return RedirectToAction("Index");
        }

        [Perfil(Perfil.Administrador)]
        public ActionResult Edit(int? id) {
            if(id == null) {
                TempData["error"] = "Ocorreu um erro inesperado. Entre em contato com o administrador do sistema.";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewModelInstituicao vmInstituicao = new ViewModelInstituicao();
            Instituicao instituicao = Collection.FindInstituicao(id);
            if(instituicao == null) {
                TempData["error"] = "Você não tem permissão para editar esta instituição.";
                return HttpNotFound();
            }

            vmInstituicao.instituicao = instituicao;

            Endereco enderecoPrincipal = Collection.FindEndereco(instituicao.IdEnderecoPrincipal);
            Endereco enderecoCobranca = null;
            if(instituicao.IdEnderecoPrincipal != instituicao.IdEnderecoCobranca)
                enderecoCobranca = Collection.FindEndereco(instituicao.IdEnderecoCobranca);
            ViewBag.enderecoCobranca = enderecoCobranca;
            ViewBag.enderecoPrincipal = enderecoPrincipal;

            vmInstituicao.Midia = Collection.FindMidia(id, "Instituicao");
            return View(vmInstituicao);
        }

        [HttpPost]
        [Perfil(Perfil.Administrador)]
        public ActionResult Edit(ViewModelInstituicao viewModel) {
            Instituicao instituicao = viewModel.instituicao;
            if(Collection.FindInstituicao(instituicao.IdInstituicao) == null) return HttpNotFound();

            int apagarEndereco = 0;
            //Caso o usuário já tinha cadastrado um endereço de cobrança diferente do principal e optou por tornar o endereço de cobrança como o mesmo endereço principal
            if(viewModel.EqualEnderecoCobranca && viewModel.IdEnderecoCobranca != viewModel.IdEnderecoPrincipal) {
                //Apagando endereço de cobrança do banco
                apagarEndereco = viewModel.IdEnderecoCobranca;
                viewModel.IdEnderecoCobranca = viewModel.IdEnderecoPrincipal;
                instituicao.IdEnderecoCobranca = viewModel.IdEnderecoPrincipal;
            }
            else if(viewModel.EqualEnderecoCobranca == false && viewModel.IdEnderecoCobranca == viewModel.IdEnderecoPrincipal) {
                Endereco cobranca = viewModel.enderecoCobranca;
                Collection.CreateEndereco(cobranca);
                instituicao.IdEnderecoCobranca = cobranca.IdEndereco;
            }
            else if(viewModel.EqualEnderecoCobranca == false) {
                //Atualizando endereço cobranca
                Endereco cobranca = viewModel.enderecoCobranca;
                Collection.EditEndereco(cobranca);
            }
            //Atualizando endereço principal
            Endereco principal = viewModel.enderecoPrincipal;
            Collection.EditEndereco(principal);
            Collection.EditInstituicao(instituicao);
            if(apagarEndereco != 0) Collection.DeleteEndereco(apagarEndereco);

            TempData["success"] = "Instituição atualizada com sucesso!";

            return RedirectToAction("Index");
        }

        /*[Perfil(Perfil.Administrador)]
        public ActionResult Delete(int? id) {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Instituicao instituicao = Collection.FindInstituicao(id);
            if(instituicao == null)
                return HttpNotFound();

            Endereco enderecoPrincipal = Collection.FindEndereco(instituicao.IdEnderecoPrincipal);
            Endereco enderecoCobranca = null;
            if(instituicao.IdEnderecoPrincipal != instituicao.IdEnderecoCobranca)
                enderecoCobranca = Collection.FindEndereco(instituicao.IdEnderecoCobranca);
            ViewBag.enderecoCobranca = enderecoCobranca;
            ViewBag.enderecoPrincipal = enderecoPrincipal;
            return View(instituicao);
        }

        [Perfil(Perfil.Administrador)]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            Collection.DeleteInstituicao(id);
            return RedirectToAction("Index");
        }*/
    }
}