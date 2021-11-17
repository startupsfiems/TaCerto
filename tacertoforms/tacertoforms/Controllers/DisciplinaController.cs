using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

using TaCertoForms.Attributes;
using TaCertoForms.Controllers.Base;
using TaCertoForms.Models;

namespace TaCertoForms.Controllers {
    [SomenteLogado]
    public class DisciplinaController : ControladoraBase {
        [Perfil(Perfil.Administrador)]
        public ActionResult Index() {
            List<ViewModelDisciplina> disciplinas = new List<ViewModelDisciplina>();
            List<Disciplina> disciplinasBanco = Collection.DisciplinaList();
            List<DisciplinaTurma> disciplinaTurmaList = Collection.DisciplinaTurmaList();
            if(disciplinasBanco != null) {
                foreach(var disc in disciplinasBanco) {
                    ViewModelDisciplina vmDisc = new ViewModelDisciplina() { IdDisciplina = disc.IdDisciplina, Nome = disc.Nome, Descricao = disc.Descricao };
                    //List<DisciplinaTurma> aux = disciplinaTurmaList.Where(dt => dt.IdDisciplina == disc.IdDisciplina).ToList();
                    if (disciplinaTurmaList != null) { 
                        List<DisciplinaTurma> aux = disciplinaTurmaList.Where(dt => dt.IdDisciplina == disc.IdDisciplina).ToList();

                        foreach(var discTurm in aux)
                            vmDisc.Turmas.Add(Collection.FindTurma(discTurm.IdTurma));
                    }
                    disciplinas.Add(vmDisc);
                }
            }
            return View(disciplinas);
        }

        //GET: Ajax
        [HttpGet]
        [Perfil(Perfil.Administrador, Perfil.Autor)]
        public ActionResult AjaxDisciplinas(int IdTurma) {

            List<ViewModelDisciplina> disciplinas = new List<ViewModelDisciplina>();
            List<DisciplinaTurma> disciplinasTurma = Collection.DisciplinaTurmaList()?.Where(x => x.IdTurma == IdTurma).ToList();
            List<Disciplina> disciplinaList = Collection.DisciplinaList();
            if(disciplinasTurma != null) {
                foreach(var disciplinaTurma in disciplinasTurma) {
                    Disciplina disciplina = disciplinaList?.Where(d => d.IdDisciplina == disciplinaTurma.IdDisciplina).FirstOrDefault();
                    if(disciplina != null) {
                        ViewModelDisciplina vmDisc = new ViewModelDisciplina() { IdDisciplinaTurma = disciplinaTurma.IdDisciplinaTurma, Nome = disciplina.Nome };
                        disciplinas.Add(vmDisc);
                    }
                }
            }
            ViewBag.DisciplinasList = new SelectList(disciplinas, "IdDisciplinaTurma", "Nome");
            return View();
        }
        public ActionResult Create() {
            ViewBag.turmas = Collection.TurmaList();
            ViewBag.InstituicaoList = Collection.InstituicaoList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(ViewModelDisciplina vmDisciplina) {
            Disciplina disciplina = vmDisciplina.Disciplina;
            disciplina.IdMatriz = (int)Session["IdMatriz"];
            if(Collection.CreateDisciplina(disciplina) != null) {
                TempData["sucess"] = "Disciplina cadastrada com sucesso.";
                return RedirectToAction("Edit", "Disciplina", new { id = disciplina.IdDisciplina });
            }
            return View(disciplina);
        }

        public ActionResult Edit(int? id) {
            if(id == null) {
                TempData["error"] = "Disciplina n�o encontrada!";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disciplina disciplina = Collection.FindDisciplina(id);
            if(disciplina == null) {
                TempData["error"] = "Disciplina n�o encontrada!";
                return HttpNotFound();
            }

            ViewModelDisciplina viewModelDisciplina = new ViewModelDisciplina();
            viewModelDisciplina.Disciplina = disciplina;

            List<DisciplinaTurma> aux = Collection.DisciplinaTurmaList()?.Where(dt => dt.IdDisciplina == viewModelDisciplina.IdDisciplina).ToList();
            if(aux != null) {
                foreach(var discTurm in aux)
                    viewModelDisciplina.Turmas.Add(Collection.FindTurma(discTurm.IdTurma));
                viewModelDisciplina.EncherTurmas();

                ViewBag.turmas = Collection.TurmaList();
            }

            List<Instituicao> instituicoes = Collection.InstituicaoList().ToList();
            if(instituicoes == null) instituicoes = new List<Instituicao>();
            ViewBag.InstituicaoList = new SelectList(instituicoes, "IdInstituicao", "NomeFantasia");

            return View(viewModelDisciplina);
        }

        [HttpPost]
        public ActionResult Edit(ViewModelDisciplina vmDisciplina) {
            Disciplina disciplina = Collection.EditDisciplina(vmDisciplina.Disciplina);
            if(disciplina == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            string[] idTurmas = vmDisciplina.idTurmas != null ? vmDisciplina.idTurmas.Split(';') : new string[0];
            foreach(var id in idTurmas)
                vmDisciplina.Turmas.Add(Collection.FindTurma(int.Parse(id)));

            List<DisciplinaTurma> dts = Collection.DisciplinaTurmaList().Where(dt => dt.IdDisciplina == vmDisciplina.IdDisciplina).ToList();
            List<Turma> turmasBanco = new List<Turma>();
            foreach(var aux in dts)
                turmasBanco.Add(Collection.FindTurma(aux.IdTurma));

            for(int i = vmDisciplina.Turmas.Count - 1; i >= 0; i--) {
                bool flag = false;
                for(int j = turmasBanco.Count - 1; j >= 0; j--) {
                    if(turmasBanco[j].IdTurma == vmDisciplina.Turmas[i].IdTurma) {
                        flag = true;
                        turmasBanco.RemoveAt(j);
                        break;
                    }
                }
                if(!flag) {
                    DisciplinaTurma dt = new DisciplinaTurma() { IdDisciplina = disciplina.IdDisciplina, IdTurma = vmDisciplina.Turmas[i].IdTurma };
                    Collection.CreateDisciplinaTurma(dt);
                }
            }
            foreach(var item in turmasBanco) {
                DisciplinaTurma dt = Collection.DisciplinaTurmaList().Where(aux => aux.IdDisciplina == vmDisciplina.IdDisciplina && aux.IdTurma == item.IdTurma).Single();
                if(dt != null)
                    Collection.DeleteDisciplinaTurma(dt.IdDisciplinaTurma);
            }
            TempData["success"] = "Disciplina atualizada com sucesso.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Perfil(Perfil.Administrador)]
        public ActionResult AjaxTurmasDisciplinas(int IdDisciplina) {
            List<ViewModelDisciplina> disciplinaTurma = new List<ViewModelDisciplina>();

            List<DisciplinaTurma> dt = Collection.DisciplinaTurmaList()?.Where(x => x.IdDisciplina == IdDisciplina).ToList();
            if(dt != null) { 
                foreach(var discTurm in dt) {
                    Disciplina disciplina = Collection.FindDisciplina(discTurm.IdDisciplina);
                    ViewModelDisciplina vmDisc = new ViewModelDisciplina() { Nome = disciplina.Nome, IdDisciplinaTurma = discTurm.IdDisciplinaTurma };
                    Turma turma = Collection.FindTurma(discTurm.IdTurma);

                    Instituicao instituicao = Collection.FindInstituicao(turma.IdInstituicao);
                    vmDisc.Turmas.Add(turma);
                    vmDisc.Instituicao = instituicao;
                    disciplinaTurma.Add(vmDisc);
                }
            }

            return View(disciplinaTurma);
        }

        [HttpPost]
        [Perfil(Perfil.Administrador)]
        public void SalvarTurmaDisciplina(int IdDisciplina, int IdTurma) {
            DisciplinaTurma disciplinaTurma = new DisciplinaTurma() { IdDisciplina = IdDisciplina, IdTurma = IdTurma };
            Collection.CreateDisciplinaTurma(disciplinaTurma);
            TempData["success"] = "Registro salvo com sucesso.";
        }

        [HttpPost]
        [Perfil(Perfil.Administrador)]
        public JsonResult AjaxDesvincularDisciplinaTurma(int id) {
            Collection.DeleteDisciplinaTurma(id);

            var message = new { code = 200, message = "Cadastrado com sucesso!" };
            TurmaDisciplinaAutor tda = db.TurmaDisciplinaAutor.Where(x => x.IdDisciplinaTurma == id).FirstOrDefault();
            if(tda != null) {
                Atividade atividade = db.Atividade.Where(x => x.IdTurmaDisciplinaAutor == tda.IdTurmaDisciplinaAutor).FirstOrDefault();
                if(atividade != null)
                    message = new { code = 400, message = "N�o � poss�vel deletar v�nculo, pois j� existe uma atividade vinculada a disciplina." };
            }
            TempData["success"] = "Desv�nculo realizado com sucesso.";
            return Json(message);
        }
    }
}