using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TaCertoForms.Contexts;
using TaCertoForms.Controllers.Base;
using TaCertoForms.Models;

namespace tacertoforms.Controllers {
    public class TesteController : ControladoraBase {
        public bool limpaBanco() {
            //DELETE FROM [StartupJogos_SESI].[TaCerto].[AtividadeAluno];DELETE FROM [StartupJogos_SESI].[TaCerto].[PessoaToken]
            Context db = new Context();
            List<Midia> midias = db.Midia.Select(x => x).ToList(); db.Dispose(); db = new Context();
            List<AtividadeRespostaAluno> atividaderespostaalunos = db.AtividadeRespostaAluno.Select(x => x).ToList(); db.Dispose();db = new Context();
            List<QuestaoRespostaAluno> qras = db.QuestaoRespostaAluno.Select(x => x).ToList(); db.Dispose();db = new Context();
            List<Questao> questaos = db.Questao.Select(x => x).ToList(); db.Dispose();db = new Context();
            List<TurmaAluno> turmaalunos = db.TurmaAluno.Select(x => x).ToList(); db.Dispose();db = new Context();
            List<Atividade> atividades = db.Atividade.Select(x => x).ToList(); db.Dispose();db = new Context();
            List<TurmaDisciplinaAutor> tdas = db.TurmaDisciplinaAutor.Select(x => x).ToList(); db.Dispose();db = new Context();
            List<DisciplinaTurma> dts = db.DisciplinaTurma.Select(x => x).ToList(); db.Dispose();db = new Context();
            List<Disciplina> disciplinas = db.Disciplina.Select(x => x).ToList(); db.Dispose();db = new Context();
            List<Turma> turmas = db.Turma.Select(x => x).ToList(); db.Dispose();db = new Context();
            List<Pessoa> pessoas = db.Pessoas.Select(x => x).ToList(); db.Dispose();db = new Context();
            List<Instituicao> instituicoes_filhas = db.Instituicao.Where(x => x.IsMatriz == false).ToList(); db.Dispose();db = new Context();
            List<Instituicao> instituicoes_pais = db.Instituicao.Where(x => x.IsMatriz == true).ToList(); db.Dispose(); db = new Context();
            List<Endereco> enderecos = db.Endereco.Select(x => x).ToList(); db.Dispose();db = new Context();

            if(midias.Count > 0) { foreach(var midia in midias) { db.Entry(midia).State = System.Data.Entity.EntityState.Deleted; db.SaveChanges(); db.Dispose(); db = new Context(); } } db = new Context();
            if(qras.Count > 0) { foreach (var qra in qras) { db.Entry(qra).State = System.Data.Entity.EntityState.Deleted; db.SaveChanges(); db.Dispose(); db = new Context(); } } db = new Context();
            if(atividaderespostaalunos.Count > 0) { foreach(var atra in atividaderespostaalunos) { db.Entry(atra).State = System.Data.Entity.EntityState.Deleted; db.SaveChanges(); db.Dispose();db = new Context();}} db = new Context();
            if(questaos.Count > 0) { foreach(var quest in questaos) { db.Entry(quest).State = System.Data.Entity.EntityState.Deleted; db.SaveChanges(); db.Dispose();db = new Context();}} db = new Context();
            if(turmaalunos.Count > 0) { foreach(var taaa in turmaalunos) { db.Entry(taaa).State = System.Data.Entity.EntityState.Deleted; db.SaveChanges(); db.Dispose();db = new Context();}} db = new Context();
            if(atividades.Count > 0) { foreach(var at in atividades) { db.Entry(at).State = System.Data.Entity.EntityState.Deleted; db.SaveChanges(); db.Dispose();db = new Context();}} db = new Context();
            if(tdas.Count > 0) { foreach(var tda in tdas) { db.Entry(tda).State = System.Data.Entity.EntityState.Deleted; db.SaveChanges(); db.Dispose();db = new Context();}} db = new Context();
            if(dts.Count > 0) { foreach(var dt in dts) { db.Entry(dt).State = System.Data.Entity.EntityState.Deleted; db.SaveChanges(); db.Dispose();db = new Context();}} db = new Context();
            if(disciplinas.Count > 0) { foreach(var disc in disciplinas) { db.Entry(disc).State = System.Data.Entity.EntityState.Deleted; db.SaveChanges(); db.Dispose();db = new Context();}} db = new Context();
            if(turmas.Count > 0) { foreach(var turma in turmas) { db.Entry(turma).State = System.Data.Entity.EntityState.Deleted; db.SaveChanges(); db.Dispose();db = new Context();}} db = new Context();
            if(pessoas.Count > 0) { foreach(var pessoa in pessoas) { db.Entry(pessoa).State = System.Data.Entity.EntityState.Deleted; db.SaveChanges(); db.Dispose();db = new Context();}} db = new Context();
            if(instituicoes_filhas.Count > 0) { foreach(var insti in instituicoes_filhas) { db.Entry(insti).State = System.Data.Entity.EntityState.Deleted; db.SaveChanges(); db.Dispose();db = new Context();}} db = new Context();
            if(instituicoes_pais.Count > 0) { foreach(var insti in instituicoes_pais) { db.Entry(insti).State = System.Data.Entity.EntityState.Deleted; db.SaveChanges(); db.Dispose();db = new Context();}} db = new Context();
            if(enderecos.Count > 0) { foreach(var ende in enderecos) { db.Entry(ende).State = System.Data.Entity.EntityState.Deleted; db.SaveChanges(); db.Dispose();db = new Context();}} db = new Context();

            return true;
        }

        public bool populaBanco() {
            Context db = new Context();
            DateTime dataInicio1 = new DateTime(2020, 1, 1);
            DateTime dataFim1 = new DateTime(2020, 5, 20);

            DateTime dataInicio2 = new DateTime(2020, 1, 15);
            DateTime dataFim2 = new DateTime(2020, 3, 10);

            Endereco endereco = new Endereco { Pais = "Brasil", UF = "MS", Cidade = "Campo Grande", Numero = 1206, CEP = "79.005-901", Logradouro = "Av. Afonso Pena", Bairro = "Amambai"}; db.Endereco.Add(endereco); db.SaveChanges(); db.Dispose(); db = new Context();
            Instituicao instituicao = new Instituicao { RazaoSocial = "SESI Mato Grosso do Sul", NomeFantasia = "SESI Mato Grosso do Sul", CNPJ = "56.397.653/0001-00", Email = "contato@sesims.com.br", Telefone = "(67) 3389-9142", IdEnderecoPrincipal = endereco.IdEndereco, IdEnderecoCobranca = endereco.IdEndereco, IsMatriz = true }; db.Instituicao.Add(instituicao); db.SaveChanges(); db.Dispose(); db = new Context();
            Instituicao instituicaoatt = new Instituicao { RazaoSocial = "SESI Mato Grosso do Sul", NomeFantasia = "SESI Mato Grosso do Sul", CNPJ = "56.397.653/0001-00", Email = "contato@sesims.com.br", Telefone = "(67) 3389-9142", IdInstituicao = instituicao.IdInstituicao, IdMatriz = instituicao.IdInstituicao, IdEnderecoCobranca = endereco.IdEndereco, IdEnderecoPrincipal = endereco.IdEndereco, IsMatriz = true }; db.Entry(instituicaoatt).State = System.Data.Entity.EntityState.Modified; db.SaveChanges(); db.Dispose(); db = new Context();
            Pessoa pessoa = new Pessoa { IdInstituicao = instituicao.IdInstituicao, Perfil = Perfil.Administrador, Nome = "Admin", CPF = "279.588.971-46", Email = "admin@sesims.com.br", Senha = "123"}; db.Pessoas.Add(pessoa); db.SaveChanges(); db.Dispose(); db = new Context();
            
            /*
            Turma turma = new Turma { IdInstituicao = instituicao.IdInstituicao,Serie = "1º Ano A",Periodo = Periodo.Matutino }; db.Turma.Add(turma); db.SaveChanges(); db.Dispose(); db = new Context();
            Disciplina disciplina1 = new Disciplina { Nome = "Português",IdMatriz = instituicao.IdInstituicao }; db.Disciplina.Add(disciplina1); db.SaveChanges(); db.Dispose(); db = new Context();
            Disciplina disciplina2 = new Disciplina { Nome = "Matemática",IdMatriz = instituicao.IdInstituicao }; db.Disciplina.Add(disciplina2); db.SaveChanges(); db.Dispose(); db = new Context();
            DisciplinaTurma dt1 = new DisciplinaTurma { IdDisciplina = disciplina1.IdDisciplina,IdTurma = turma.IdTurma }; db.DisciplinaTurma.Add(dt1); db.SaveChanges(); db.Dispose(); db = new Context();
            DisciplinaTurma dt2 = new DisciplinaTurma { IdDisciplina = disciplina2.IdDisciplina,IdTurma = turma.IdTurma }; db.DisciplinaTurma.Add(dt2); db.SaveChanges(); db.Dispose(); db = new Context();
            Pessoa professor = new Pessoa { IdInstituicao = instituicao.IdInstituicao,Perfil = Perfil.Autor,Nome = "Elisa da Silva",CPF = "398.998.205-20",Email = "elisa@sesims.com.br",Senha = "123" }; db.Pessoa.Add(professor); db.SaveChanges(); db.Dispose(); db = new Context();
            TurmaDisciplinaAutor tda1 = new TurmaDisciplinaAutor { IdAutor = professor.IdPessoa,IdDisciplinaTurma = dt1.IdDisciplinaTurma }; db.TurmaDisciplinaAutor.Add(tda1); db.SaveChanges(); db.Dispose(); db = new Context();
            TurmaDisciplinaAutor tda2 = new TurmaDisciplinaAutor { IdAutor = professor.IdPessoa,IdDisciplinaTurma = dt2.IdDisciplinaTurma }; db.TurmaDisciplinaAutor.Add(tda2); db.SaveChanges(); db.Dispose(); db = new Context();
            Atividade atividade1 = new Atividade {  IdTurmaDisciplinaAutor = tda1.IdTurmaDisciplinaAutor, DataInicio = dataInicio1, DataFim = dataFim1, NumeroTentativas = 30, IsAleatorio = true, IsProva = true, Titulo = "Revisão da Prova", NumeroQuestoes = 3 }; db.Atividade.Add(atividade1); db.SaveChanges(); db.Dispose(); db = new Context();
            Atividade atividade2 = new Atividade { IdTurmaDisciplinaAutor = tda2.IdTurmaDisciplinaAutor, DataInicio = dataInicio2, DataFim = dataFim2, NumeroTentativas = 23, IsAleatorio = false, IsProva = false, Titulo = "Atividade Valendo Nota", NumeroQuestoes = 3 }; db.Atividade.Add(atividade2); db.SaveChanges(); db.Dispose(); db = new Context();
            Pessoa aluno = new Pessoa { IdInstituicao = instituicao.IdInstituicao, Perfil = Perfil.Aluno, Nome = "Juquinha Lins", CPF = "371.177.390-70", Email = "j@j.com", Senha = "123" }; db.Pessoa.Add(aluno); db.SaveChanges(); db.Dispose(); db = new Context();
            TurmaAluno ta = new TurmaAluno { IdTurma = turma.IdTurma, IdPessoa = aluno.IdPessoa }; db.TurmaAluno.Add(ta); db.SaveChanges(); db.Dispose(); db = new Context();
            Questao questao1 = new Questao { IdAtividade = atividade1.IdAtividade, IdTipoQuestao = 1, Titulo = "Se você corta uma minhoca pela metade, as duas partes podem regenerar seu corpo?", Enunciado = "Se você corta uma minhoca pela metade, as duas partes podem regenerar seu corpo?", JsonQuestao = "{\"isVerdadeiro\":false}", PesoNota = 1}; db.Questao.Add(questao1); db.SaveChanges(); db.Dispose(); db = new Context();
            Questao questao2 = new Questao { IdAtividade = atividade1.IdAtividade, IdTipoQuestao = 1, Titulo = "Um pão com manteiga jogado ao ar cai pelo lado da manteiga três de cada quatro vezes.", Enunciado = "Um pão com manteiga jogado ao ar cai pelo lado da manteiga três de cada quatro vezes.", JsonQuestao = "{\"isVerdadeiro\":false}", PesoNota = 1}; db.Questao.Add(questao2); db.SaveChanges(); db.Dispose(); db = new Context();
            Questao questao3 = new Questao { IdAtividade = atividade1.IdAtividade, IdTipoQuestao = 2, Titulo = "Complete a sentença!", Enunciado = "Complete a sentença!", JsonQuestao = "{ \"frase\":[{\"isTexto\":true,\"texto\":\"O rato \"},{\"isTexto\":false,\"texto\":\"\"},{\"isTexto\":true,\"texto\":\" a \"},{\"isTexto\":false,\"texto\":\"\"},{\"isTexto\":true,\"texto\":\" do rei de Roma\"}],\"alternativa\":[{\"index\":[1],\"texto\":\"roeu\"},{\"index\":[3],\"texto\":\"roupa\"},{\"index\":[-1],\"texto\":\"mordeu\"},{\"index\":[-1],\"texto\":\"lambeu\"},{\"index\":[-1],\"texto\":\"calça\"},{\"index\":[-1],\"texto\":\"bolsa\"}]}", PesoNota = 1 }; db.Questao.Add(questao3); db.SaveChanges(); db.Dispose(); db = new Context();
            Questao questao4 = new Questao { IdAtividade = atividade2.IdAtividade, IdTipoQuestao = 1, Titulo = "Tem mais andares do Empire State em seu subterrâneo que os que tem em cima.", Enunciado = "Tem mais andares do Empire State em seu subterrâneo que os que tem em cima.", JsonQuestao = "{\"isVerdadeiro\":false}", PesoNota = 1 }; db.Questao.Add(questao4); db.SaveChanges(); db.Dispose(); db = new Context();
            Questao questao5 = new Questao { IdAtividade = atividade2.IdAtividade, IdTipoQuestao = 1, Titulo = "O animal nacional da Escócia é um unicórnio.", Enunciado = "O animal nacional da Escócia é um unicórnio.", JsonQuestao = "{\"isVerdadeiro\":true}", PesoNota = 1 }; db.Questao.Add(questao5); db.SaveChanges(); db.Dispose(); db = new Context();
            Questao questao6 = new Questao { IdAtividade = atividade2.IdAtividade, IdTipoQuestao = 2, Titulo = "Complete a música!", Enunciado = "Complete a música!", JsonQuestao = "{\"frase\":[{\"isTexto\":true,\"texto\":\"E nessa \"},{\"isTexto\":false,\"texto\":\"\"},{\"isTexto\":true,\"texto\":\", de dizer que não te quero, vou \"},{\"isTexto\":false,\"texto\":\"\"},{\"isTexto\":true,\"texto\":\" as aparências\"}],\"alternativa\":[{\"index\":[1],\"texto\":\"loucura\"},{\"index\":[3],\"texto\":\"negando\"},{\"index\":[-1],\"texto\":\"tontura\"},{\"index\":[-1],\"texto\":\"doidura\"},{\"index\":[-1],\"texto\":\"achando\"}]}", PesoNota = 1}; db.Questao.Add(questao6); db.SaveChanges(); db.Dispose(); db = new Context();
            //Pessoa pessoa = new Pessoa {  }; db.Pessoa.Add(pessoa); db.SaveChanges(); db.Dispose(); db = new Context();
            */
            return true;
        }
    }
}