using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TaCertoForms.Contexts;
using TaCertoForms.Factory;
using TaCertoForms.Models;

namespace TaCertoForms.Controllers.Base {
    public class ControladoraBase : Controller {
        protected Context db = new Context();
        IFactoryCollection collection;

        protected IFactoryCollection Collection {
            get {
                if(collection == null) {
                    if(Session["Perfil"].Equals(Perfil.Autor))
                        return collection = new FactoryCollectionProfessor(Session);
                    else if(Session["Perfil"].Equals(Perfil.Administrador))
                        return collection = new FactoryCollectionMatriz(Session);
                }
                else
                    return collection;
                return null;
            }
        }

        protected List<Pessoa> GetMeusAlunos() {
            Context db = new Context();
            List<TurmaAluno> turmaAlunos = new List<TurmaAluno>();
            List<Pessoa> alunos = new List<Pessoa>();
            List<TurmaDisciplinaAutor> tda_aux = db.TurmaDisciplinaAutor.Where(tda => tda.IdAutor == (int)Session["IdPessoa"]).ToList();
            foreach(var tda in tda_aux) {
                DisciplinaTurma dt = db.DisciplinaTurma.Find(tda.IdDisciplinaTurma);
                Turma t = db.Turma.Find(dt.IdTurma);
                List<TurmaAluno> ta_aux = db.TurmaAluno.Where(ta => ta.IdTurma == t.IdTurma && !turmaAlunos.Contains(ta)).ToList();
                turmaAlunos = turmaAlunos.Concat(ta_aux).ToList();
            }
            foreach(var ta in turmaAlunos) {
                Pessoa p = db.Pessoas.Find(ta.IdPessoa);
                if(p != null)
                    alunos.Add(p);
            }
            db.Dispose();
            return alunos;
        }

        protected List<Pessoa> GetPessoasMatriz() {
            Context db = new Context();
            List<Instituicao> instituicoes = GetMinhasInstituicoes();
            if(instituicoes == null) {
                db.Dispose();
                return null;
            }
            List<Pessoa> pessoas = new List<Pessoa>();
            foreach(var i in instituicoes) {
                List<Pessoa> p_aux = db.Pessoas.Where(p => p.IdInstituicao == i.IdInstituicao).ToList();
                if(p_aux != null)
                    pessoas = pessoas.Concat(p_aux).ToList();
            }
            db.Dispose();
            return pessoas;
        }

        protected List<Atividade> GetMinhasAtividades() {
            Context db = new Context();
            int id =(int)Session["IdPessoa"];
            Pessoa p = db.Pessoas.Find(id);
            List<TurmaDisciplinaAutor> turmaDisciplinaAutorList = db.TurmaDisciplinaAutor.Where(tda => tda.IdAutor == p.IdPessoa).ToList();
            List<Atividade> atividadeList = new List<Atividade>();
            if(turmaDisciplinaAutorList != null)
                foreach(var tda in turmaDisciplinaAutorList) {
                    List<Atividade> atividade_aux = db.Atividade.Where(a => a.IdTurmaDisciplinaAutor == tda.IdTurmaDisciplinaAutor && !atividadeList.Contains(a)).ToList();
                    atividadeList = atividadeList.Concat(atividade_aux).ToList();
                }
            db.Dispose();
            return atividadeList;
        }

        protected List<Instituicao> GetMinhasInstituicoes() {
            Context db = new Context();
            int idMatriz = (int)Session["IdMatriz"];
            List<Instituicao> instituicao = db.Instituicao.Where(i => i.IdMatriz == idMatriz || i.IdInstituicao == idMatriz).ToList();
            db.Dispose();
            if(instituicao == null)
                instituicao = new List<Instituicao>();
            return instituicao;
        }

        protected Instituicao FindMinhaInstituicao(int? id) {
            if(id == null) return null;
            Context db = new Context();
            int idMatriz = (int)Session["IdMatriz"];
            Instituicao instituicao = db.Instituicao.Where(i => i.IdInstituicao == id && (i.IdMatriz == idMatriz || i.IdInstituicao == idMatriz)).FirstOrDefault();
            db.Dispose();
            return instituicao;
        }
    }
}