using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaCertoForms.Attributes;
using TaCertoForms.Contexts;
using TaCertoForms.Controllers.Base;
using TaCertoForms.Models;

namespace TaCertoForms.Controllers {
	[SomenteLogado]
	public class RelatorioAtividadesController : ControladoraBase {
		//relatório com todas as atividades do professor
		[Perfil(Perfil.Autor)]
		public ActionResult Relatorio1(int? IdTurma, int? IdDisciplina, bool? IsProva) {
			List<Turma> turmas = Collection.TurmaList();
			ViewBag.turmas = new SelectList(turmas, "IdTurma", "Serie");

			List<Disciplina> disciplinas = Collection.DisciplinaList();
			ViewBag.disciplinas = new SelectList(disciplinas, "IdDisciplina", "Nome");

			List<DisciplinaTurma> disciplinaTurmas = Collection.DisciplinaTurmaList();
			List<TurmaDisciplinaAutor> turmaDisciplinaAutors = Collection.TurmaDisciplinaAutorList();

			List<Atividade> atividades = Collection.AtividadeList();
			List<ViewModelAtividade> vmAtividades = new List<ViewModelAtividade>();
			foreach(var a in atividades) {
				int cont = vmAtividades.Count;

				TurmaDisciplinaAutor tda = turmaDisciplinaAutors.Where(aux => aux.IdTurmaDisciplinaAutor == a.IdTurmaDisciplinaAutor).FirstOrDefault();
				if(tda == null) continue;

				DisciplinaTurma dt = disciplinaTurmas.Where(aux => aux.IdDisciplinaTurma == tda.IdDisciplinaTurma).FirstOrDefault();
				if(dt == null) continue;

				if(IdTurma != null && dt.IdTurma != IdTurma) continue;
				if(IdDisciplina != null && dt.IdDisciplina != IdDisciplina) continue;
				if(IsProva != null && a.IsProva != IsProva) continue;

				vmAtividades.Add(new ViewModelAtividade());
				vmAtividades[cont].Atividade = a;

				vmAtividades[cont].nome_da_materia = ((Disciplina)(disciplinas.Where(aux => aux.IdDisciplina == dt.IdDisciplina).FirstOrDefault())).Nome;
				vmAtividades[cont].nome_da_turma = ((Turma)(turmas.Where(aux => aux.IdTurma == dt.IdTurma).FirstOrDefault())).Serie;
			}
			ViewBag.atividades = vmAtividades;

			return View();
		}

		//relatório de uma atividade especifica
		[Perfil(Perfil.Autor)]
		public ActionResult Relatorio2(int IdAtividade) {
			Atividade a = Collection.FindAtividade(IdAtividade);
			if(a == null) return HttpNotFound();
			TurmaDisciplinaAutor tda = Collection.FindTurmaDisciplinaAutor(a.IdTurmaDisciplinaAutor);
			if(tda == null) return HttpNotFound();
			DisciplinaTurma dt = Collection.FindDisciplinaTurma(tda.IdDisciplinaTurma);
			if(dt == null) return HttpNotFound();
			Disciplina d = Collection.FindDisciplina(dt.IdDisciplina);
			if(d == null) return HttpNotFound();
			Turma t = Collection.FindTurma(dt.IdTurma);
			if(t == null) return HttpNotFound();
			List<TurmaAluno> tas = Collection.TurmaAlunoList();
			if(tas == null) return HttpNotFound();
			List<int> alunos_id = new List<int>();
			foreach(var item in tas)
				if(item.IdTurma == t.IdTurma) alunos_id.Add(item.IdPessoa);

			Context db = new Context();
			List<AtividadeAluno> atividadeAlunos = db.AtividadeAluno.Where(aa => aa.IdAtividade == IdAtividade).ToList();
			if(atividadeAlunos == null) atividadeAlunos = new List<AtividadeAluno>();

			List<ViewModelAtividadeAluno> vmAtividadeAlunos = new List<ViewModelAtividadeAluno>();
			double nota = 0;
			foreach(var aa in atividadeAlunos) {
				ViewModelAtividadeAluno aa_aux = new ViewModelAtividadeAluno();
				aa_aux.AtividadeAluno = aa;
				aa_aux.nome_aluno = (Collection.FindPessoa(aa.IdPessoa))?.Nome;
				vmAtividadeAlunos.Add(aa_aux);
				nota += aa.MaiorNota;
			}
			nota /= atividadeAlunos.Count;

			ViewModelAtividade vmAtividade = new ViewModelAtividade();
			vmAtividade.Atividade = a;
			vmAtividade.nome_da_materia = d.Nome;
			vmAtividade.nome_da_turma = t.Serie;
			vmAtividade.media_nota = nota;

			ViewBag.atividade = vmAtividade;
			ViewBag.alunos = vmAtividadeAlunos;

			return View();
		}

        [HttpGet]
        [Perfil(Perfil.Autor)]
        public ActionResult AjaxDisciplinas(int? IdTurma, int? Selected) {
            List<DisciplinaTurma> disciplinaTurmas = Collection.DisciplinaTurmaList();
            List<Disciplina> disciplinas = new List<Disciplina>();
            foreach(var dt in disciplinaTurmas)
                if(dt.IdTurma == IdTurma)
                    disciplinas.Add(Collection.FindDisciplina(dt.IdDisciplina));
            ViewBag.Disciplinas = new SelectList(disciplinas, "IdDisciplina", "Nome", Selected);
            return View();
        }
    }
}