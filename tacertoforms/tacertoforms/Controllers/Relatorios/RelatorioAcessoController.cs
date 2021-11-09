using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using TaCertoForms.Attributes;
using TaCertoForms.Contexts;
using TaCertoForms.Controllers.Base;
using TaCertoForms.Models;

namespace TaCertoForms.Controllers {
	[SomenteLogado]
	public class RelatorioAcessoController : ControladoraBase {
		//relatório de acessos
		[Perfil(Perfil.Autor, Perfil.Administrador)]
		public ActionResult Relatorio1(string dataInicio, string dataFim) {
			DateTime DataInicio, DataFim;
			if(dataInicio == null) DataInicio = new DateTime(1900, 1, 1);
			else DataInicio = DateTime.Parse(dataInicio);
			if(dataFim == null) DataFim = DateTime.Now;
			else DataFim = DateTime.Parse(dataFim);

			List<Pessoa> alunos = Collection.PessoaList().Where(p => p.Perfil == Perfil.Aluno).ToList();
			if(alunos == null) alunos = new List<Pessoa>();

			List<int> alunosIds = new List<int>();
			foreach(var i in alunos)
				alunosIds.Add(i.IdPessoa);

			Context db = new Context();
			List<LogLogin> logs = db.LogLogin.Where(ll => alunosIds.Contains(ll.IdPessoa) && ll.HoraAcesso >= DataInicio && ll.HoraAcesso <= DataFim).ToList();
			if(logs == null) logs = new List<LogLogin>();

			List<int> meses = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

			List<ViewModelAcessos> vmAcessos = new List<ViewModelAcessos>();
			foreach(var a in alunos) {
				int cont = 0;
				ViewModelAcessos vma = new ViewModelAcessos();
				vma.id_pessoa = a.IdPessoa;
				vma.nome = a.Nome;
				foreach(var log in logs) {
					if(log.IdPessoa == a.IdPessoa) {
						cont++;
						if(vma.ultimo_acesso == null || vma.ultimo_acesso < log.HoraAcesso)
							vma.ultimo_acesso = log.HoraAcesso;
						if(log.HoraAcesso.Year == DateTime.Now.Year)
							meses[log.HoraAcesso.Month]++;
					}
				}
				vma.numero_acesso = cont;
				vmAcessos.Add(vma);
			}

			for(int i = meses.Count-1; i >= 0; i--)
				if(meses[i] == 0)
					meses.RemoveAt(i);
				else
					break;
			
			ViewBag.acessos = vmAcessos;
			ViewBag.acesso_mes = meses;

			return View();
		}
	}
}