using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTaCerto.Models;
using ApiTaCerto.Models.Usuario;
using ApiTaCerto.Repositorio;

namespace ApiTaCerto.Logic
{
    public class AtividadeLogic
    {
        private readonly IAtividadeRepository _atividadeRepositorio;
        private readonly string OUR_CNPJ = "56.397.653/0001-00";

        public AtividadeLogic(IAtividadeRepository atividadeRepo)
        {
            _atividadeRepositorio = atividadeRepo;
        }

        public RespostaPadrao GetAllDefaultSubjects(){
            RespostaPadrao resposta = new RespostaPadrao();
            int defaultId = _atividadeRepositorio.GetDefaultInstituteId(OUR_CNPJ);
            if(defaultId == 0)
            {
                resposta.SetErroInterno("Erro ao recuperar Id da Instituição");
                return resposta;
            }

            IEnumerable<Disciplina> disciplinas = _atividadeRepositorio.GetAllSubjectsWithId(defaultId);
            resposta.Dado = disciplinas;

            return resposta;
        }

        public RespostaPadrao GetAtividadesFeitas(long idAluno)
        {
            RespostaPadrao resposta = new RespostaPadrao();

            resposta.Dado = _atividadeRepositorio.GetNumeroDeAtividadesFeitas(idAluno);

            return resposta;
        }

        public RespostaPadrao GetAllClassSubjects(long idTurma){
            RespostaPadrao resposta = new RespostaPadrao();

            IEnumerable<Disciplina> disciplinas = _atividadeRepositorio.GetAllClassSubjects(idTurma);
            resposta.Dado = disciplinas;

            return resposta;
        }

        public RespostaPadrao GetAtividadesDisciplinaByDisciplinaId(long id)
        {
            RespostaPadrao resposta = new RespostaPadrao();

            IEnumerable<AtividadeDisciplina> atividadesDeDisciplinas = _atividadeRepositorio.GetAllClassActivities(id);
            resposta.Dado = atividadesDeDisciplinas;

            return resposta;
        }

        public RespostaPadrao GetAtividadesByIdTurmaDisciplinaAutor(long id)
        {
            RespostaPadrao resposta = new RespostaPadrao();

            IEnumerable<Atividade> atividades = _atividadeRepositorio.GetAllActivities(id);
            resposta.Dado = atividades;

            return resposta;
        }

        public RespostaPadrao GetAllQuestions(long id)
        {
            RespostaPadrao resposta = new RespostaPadrao();

            IEnumerable<Questao> questoes = _atividadeRepositorio.GetAllQuestions(id);
            resposta.Dado = questoes;

            return resposta;
        }

        public async Task<RespostaPadrao> AddOrUpdateAtividadeAluno(AtividadeAluno atividadeAluno){

            RespostaPadrao resposta = new RespostaPadrao();
            int idAtividade = atividadeAluno.IdAtividade;
            int idAtividadeAluno = atividadeAluno.IdAtividadeAluno;
            int numeroDeTentativas = 0;
            int numeroDeTentativasTentadas = 0;
            try
            {
                numeroDeTentativas = _atividadeRepositorio.GetAttemptsNumber(idAtividade);
                numeroDeTentativasTentadas = _atividadeRepositorio.GetAttemptsNumberFromId(idAtividadeAluno);
            }
            catch
            {
                resposta.SetErroInterno("Erro ao salvar ou atualizar informações da atividade");
                return resposta;
            }

            atividadeAluno.NumeroTentativas = numeroDeTentativasTentadas + 1;

            if(atividadeAluno.IdAtividadeAluno == 1){
                try
                {
                    await _atividadeRepositorio.AddAtividadeAluno(atividadeAluno);
                }
                catch
                {
                    resposta.SetErroInterno("Erro ao salvar ou atualizar informações da atividade");
                    return resposta;
                }
                resposta.Dado = atividadeAluno;
                return resposta;
            }else{
                if(atividadeAluno.NumeroTentativas <= numeroDeTentativas || numeroDeTentativas == 0){
                    try
                    {
                        await _atividadeRepositorio.UpdateAtividadeAluno(atividadeAluno);
                    }
                    catch
                    {
                        resposta.SetErroInterno("Erro ao salvar ou atualizar informações da atividade");
                        return resposta;
                    }
                    resposta.Dado = atividadeAluno;
                    return resposta;
                }
                else{
                    resposta.SetLimitesExcedidos("Você já utilizou todas as tentativas disponíveis para essa atividade");
                    return resposta;
                }
            }
        }
    
        public async Task<RespostaPadrao> SaveAtividadeResposta(AtividadeRespostaAluno atividadeRespostaAluno)
        {
            RespostaPadrao resposta = new RespostaPadrao();

            try
            {
                int id = await _atividadeRepositorio.SaveAtividadeResposta(atividadeRespostaAluno);
                resposta.Dado = id;
            }
            catch
            {
                resposta.SetErroInterno("Erro ao salvar AtividadeResposta");
                return resposta;
            }
            return resposta;
        }
    
        public async Task<RespostaPadrao> SaveQuestaoResposta(List<QuestaoRespostaAluno> questoesRespostaAluno)
        {
            RespostaPadrao resposta = new RespostaPadrao();

            try
            {
                await _atividadeRepositorio.SaveQuestaoResposta(questoesRespostaAluno);
            }
            catch
            {
                resposta.SetErroInterno("Erro ao salvar QuestãoResposta");
                return resposta;
            }
            return resposta;
        }
    }
}