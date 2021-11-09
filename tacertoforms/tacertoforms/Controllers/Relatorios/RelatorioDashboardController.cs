using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TaCertoForms.Attributes;
using TaCertoForms.Contexts;
using TaCertoForms.Controllers.Base;
using TaCertoForms.Factory;
using TaCertoForms.Models;

namespace TaCertoForms.Controllers {
	[SomenteLogado]
	public class RelatorioDashboardController : ControladoraBase {
		//ISSO É UMA GAMBIARRA
		IFactoryCollection LocalCollection;
		int AuxIdInstituicao;
		//flag: true = todas as instituições | false = somente a instituição da pessoa
		public ActionResult Relatorio1(bool? flag) {
			bool todasInstituicoes = flag != null ? (bool)flag : true;

			//ISSO É UMA GAMBIARRA
			gambiarra(todasInstituicoes);

			ViewBag.MediaTotalDeAtividadesLancadasPorProfessor = MediaTotalDeAtividadesLancadasPorProfessor();
			ViewBag.MediaDeAtividadePorProfessorNaUltimaSemana = MediaDeAtividadePorProfessorNaUltimaSemana();
			ViewBag.MediaDeNotasPorAluno = MediaDeNotasPorAluno();
			ViewBag.MediaDeAtividadeRealizadasPorAluno = MediaDeAtividadeRealizadasPorAluno();
			ViewBag.MediaDeAtividadesAlunoNaUltimaSemana = MediaDeAtividadesAlunoNaUltimaSemana();
			ViewBag.MediaDeNotaDaEscola = MediaDeNotaDaEscola();
			ViewBag.ListaAcessoProfessor = ListaAcessoProfessor();
			ViewBag.ListaAcessoAluno = ListaAcessoAluno();
			ViewBag.AlunosPorNota = AlunosPorNota();
			ViewBag.NomeInstituicao = NomeInstituicao();
			return View();
		}

		private float MediaTotalDeAtividadesLancadasPorProfessor() {
			List<Pessoa> professores = LocalCollection.PessoaList().Where(p => p.Perfil == Perfil.Autor).ToList();
			List<Atividade> atividades = LocalCollection.AtividadeList();
			return ((int)((atividades.Count / professores.Count) * 100f))/100f;
		}
		private float MediaDeAtividadePorProfessorNaUltimaSemana() {
			List<Pessoa> professores = LocalCollection.PessoaList().Where(p => p.Perfil == Perfil.Autor).ToList();
			List<Atividade> atividades = LocalCollection.AtividadeList().Where(a => a.DataInicio >= DateTime.Now.AddDays(-7)).ToList();
			return ((int)((atividades.Count / professores.Count) * 100f))/100f;
		}
		private float MediaDeNotasPorAluno() {
			int i;
			List<Pessoa> alunos = LocalCollection.PessoaList().Where(p => p.Perfil == Perfil.Aluno).ToList();
			List<int> id = new List<int>();
			for(i = 0; i < alunos.Count; i++) id.Add(alunos[i].IdPessoa);
			List<AtividadeAluno> atividadeAlunos = db.AtividadeAluno.Where(aa => id.Contains(aa.IdPessoa)).ToList();
			float total = 0;
			for(i = 0; i < atividadeAlunos.Count; i++) total += (float)atividadeAlunos[i].MaiorNota;
			return ((int)((total/i) * 100f))/100f;
		}
		private float MediaDeAtividadeRealizadasPorAluno() {
			List<Pessoa> alunos = LocalCollection.PessoaList().Where(p => p.Perfil == Perfil.Aluno).ToList();
			List<int> id = new List<int>();
			for(int i = 0; i < alunos.Count; i++) id.Add(alunos[i].IdPessoa);
			List<AtividadeAluno> atividadeAlunos = db.AtividadeAluno.Where(aa => id.Contains(aa.IdPessoa)).ToList();
			return ((int)((atividadeAlunos.Count / alunos.Count) * 100f))/100f;
		}
		private float MediaDeAtividadesAlunoNaUltimaSemana() {
			List<Pessoa> alunos = LocalCollection.PessoaList().Where(p => p.Perfil == Perfil.Aluno).ToList();
			List<Atividade> atividades = LocalCollection.AtividadeList().Where(a => a.DataInicio >= DateTime.Now.AddDays(-7)).ToList();
			List<int> id = new List<int>();
			for(int i = 0; i < atividades.Count; i++) id.Add(atividades[i].IdAtividade);
			List<AtividadeAluno> atividadeAlunos = db.AtividadeAluno.Where(aa => id.Contains(aa.IdAtividade)).ToList();
			return ((int)((atividadeAlunos.Count / alunos.Count) * 100f))/100f;
		}
		private float MediaDeNotaDaEscola() {
			int i;
			List<Pessoa> alunos = LocalCollection.PessoaList().Where(p => p.Perfil == Perfil.Aluno).ToList();
			List<int> id = new List<int>();
			for(i = 0; i < alunos.Count; i++) id.Add(alunos[i].IdPessoa);
			List<AtividadeAluno> atividadeAlunos = db.AtividadeAluno.Where(aa => id.Contains(aa.IdPessoa)).ToList();
			float total = 0;
			for(i = 0; i < atividadeAlunos.Count; i++) total += (float)atividadeAlunos[i].MaiorNota;
			return ((int)((total/i) * 100f))/100f;
		}
		private List<ViewModelAcessos> ListaAcessoProfessor() {
			List<ViewModelAcessos> acessos = new List<ViewModelAcessos>();
			List<Pessoa> professores = LocalCollection.PessoaList().Where(p => p.Perfil == Perfil.Autor).ToList();
			List<int> id = new List<int>();
			for(int i = 0; i < professores.Count; i++) id.Add(professores[i].IdPessoa);

			List<TurmaDisciplinaAutor> tda = LocalCollection.TurmaDisciplinaAutorList();
			List<Atividade> atividades = LocalCollection.AtividadeList();
			for (int i = 0; i < atividades.Count; i++) {
				for (int j = 0; j < tda.Count; j++) {
					if(atividades[i].IdTurmaDisciplinaAutor == tda[j].IdTurmaDisciplinaAutor) {
						atividades[i].IdTurmaDisciplinaAutor = tda[j].IdAutor;
						break;
					}
				}
			}

			List<LogLogin> logLogins = db.LogLogin.Where(ll => id.Contains(ll.IdPessoa)).ToList();
			for(int j = 0; j < professores.Count; j++) {
				ViewModelAcessos vmAcesso = new ViewModelAcessos();
				vmAcesso.id_pessoa = professores[j].IdPessoa;
				vmAcesso.nome = professores[j].Nome;
				vmAcesso.numero_acesso = 0;
				vmAcesso.atividades_desenvolvidas = 0;
				vmAcesso.ultimo_acesso = new DateTime(1);
				for(int i = 0; i < atividades.Count; i++) {
					if(atividades[i].IdTurmaDisciplinaAutor == professores[j].IdPessoa) {
						vmAcesso.atividades_desenvolvidas++;
					}
				}
				for(int i = 0; i < logLogins.Count; i++) {
					if(logLogins[i].IdPessoa == professores[j].IdPessoa) {
						vmAcesso.numero_acesso++;
						if(logLogins[i].HoraAcesso > vmAcesso.ultimo_acesso)
							vmAcesso.ultimo_acesso = logLogins[i].HoraAcesso;
					}
				}
				acessos.Add(vmAcesso);
			}
			return acessos;
		}
		private List<ViewModelAcessos> ListaAcessoAluno() {
			List<ViewModelAcessos> acessos = new List<ViewModelAcessos>();
			List<Pessoa> alunos = LocalCollection.PessoaList().Where(p => p.Perfil == Perfil.Aluno).ToList();
			List<int> id = new List<int>();
			for(int i = 0; i < alunos.Count; i++) id.Add(alunos[i].IdPessoa);

			List<AtividadeAluno> atividadeAlunos = db.AtividadeAluno.Where(aa => id.Contains(aa.IdPessoa)).ToList();
			List<LogLogin> logLogins = db.LogLogin.Where(aa => id.Contains(aa.IdPessoa)).ToList();
			for(int j = 0; j < alunos.Count; j++) {
				ViewModelAcessos vmAcesso = new ViewModelAcessos();
				vmAcesso.id_pessoa = alunos[j].IdPessoa;
				vmAcesso.nome = alunos[j].Nome;
				vmAcesso.numero_acesso = 0;
				vmAcesso.atividades_desenvolvidas = 0;
				vmAcesso.ultimo_acesso = new DateTime(1);
				for(int i = 0; i < atividadeAlunos.Count; i++) {
					if(atividadeAlunos[i].IdPessoa == alunos[j].IdPessoa) {
						vmAcesso.atividades_desenvolvidas++;
					}
				}
				for(int i = 0; i < logLogins.Count; i++) {
					if(logLogins[i].IdPessoa == alunos[j].IdPessoa) {
						vmAcesso.numero_acesso++;
						if(logLogins[i].HoraAcesso > vmAcesso.ultimo_acesso)
							vmAcesso.ultimo_acesso = logLogins[i].HoraAcesso;
					}
				}
				acessos.Add(vmAcesso);
			}
			return acessos;
		}

		private List<int> AlunosPorNota() {
			List<int> alunosPorNota = new List<int>() {0, 0, 0, 0, 0, 0};
			List<Pessoa> alunos = LocalCollection.PessoaList().Where(p => p.Perfil == Perfil.Aluno).ToList();
			List<int> id = new List<int>();
			for(int i = 0; i < alunos.Count; i++) id.Add(alunos[i].IdPessoa);

			List<AtividadeAluno> atividadeAlunos = db.AtividadeAluno.Where(aa => id.Contains(aa.IdPessoa)).ToList();

			for (int i = 0; i < alunos.Count; i++) {
				double total = 0;
				int cont = 0;
				for (int j = 0; j < atividadeAlunos.Count; j++) {
					if(alunos[i].IdPessoa == atividadeAlunos[j].IdPessoa){
						cont++;
						total+=atividadeAlunos[j].MaiorNota;
					}
				}
				if(total/cont < 3)
					alunosPorNota[0]++;
				else if(total/cont < 6)
					alunosPorNota[1]++;
				else if(total/cont < 7)
					alunosPorNota[2]++;
				else if(total/cont < 8)
					alunosPorNota[3]++;
				else if(total/cont < 10)
					alunosPorNota[4]++;
				else if(total/cont == 10)
					alunosPorNota[5]++;
			}
			
			return alunosPorNota;
		}
		private string NomeInstituicao() {
			return (LocalCollection.FindInstituicao(AuxIdInstituicao)).NomeFantasia;
		}

		private void gambiarra(bool todasInstituicoes) {
			int IdInstituicao = (int)Session["IdInstituicao"];
			AuxIdInstituicao = (int)Session["IdInstituicao"];
			if(todasInstituicoes){
				Session["IdInstituicao"] = Session["IdMatriz"];
				AuxIdInstituicao = (int)Session["IdMatriz"];
			}

			LocalCollection = new FactoryCollectionMatriz(Session);
			Session["IdInstituicao"] = IdInstituicao;
		}
	}
}