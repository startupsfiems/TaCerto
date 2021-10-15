using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTaCerto.Logic;
using ApiTaCerto.Models;
using ApiTaCerto.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApiTaCerto.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[Controller]")]
    //[Authorize()]
    public class AtividadesController : Controller
    {
        private AtividadeLogic atividadeLogic;

        public AtividadesController(IAtividadeRepository atividadeRepo)
        {
            atividadeLogic = new AtividadeLogic(atividadeRepo);
        }

        [AllowAnonymous]
        [HttpGet]
        public RespostaPadrao GetAllDefaultSubjects(){
            return atividadeLogic.GetAllDefaultSubjects();
        }

        [HttpGet("disciplinas/{idTurma}", Name="GetAllClassSubjects")]
        public RespostaPadrao GetAllClassSubjects(long idTurma){
            RespostaPadrao resposta = new RespostaPadrao();

            if(idTurma == 0)
            {
                resposta.SetCampoVazio("idTurma");
                return resposta;
            }

            return atividadeLogic.GetAllClassSubjects(idTurma);
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name="GetAtividadesDisciplina")]
        public RespostaPadrao GetAtividadesDisciplinaByDisciplinaId(long id){
            RespostaPadrao resposta = new RespostaPadrao();
            if (id == 0)
            {
                resposta.SetCampoVazio("id");
                return resposta;
            }

            return atividadeLogic.GetAtividadesDisciplinaByDisciplinaId(id);
        }
        

        [AllowAnonymous]
        [HttpGet("info/{id}", Name="GetAtividades")]
        public RespostaPadrao GetAtividadesById(long id)
        {
            RespostaPadrao resposta = new RespostaPadrao();

            if (id == 0)
            {
                resposta.SetCampoVazio("id");
                return resposta;
            }

            return atividadeLogic.GetAtividadesByIdTurmaDisciplinaAutor(id);
        }

        [AllowAnonymous]
        [HttpGet("questoes/{idatividade}", Name="GetQuestoes")]
        public RespostaPadrao GetQuestoesDeAtividade(long idatividade){
            RespostaPadrao resposta = new RespostaPadrao();

            if(idatividade == 0)
            {
                resposta.SetCampoVazio("idatividade");
                return resposta;
            }

            return atividadeLogic.GetAllQuestions(idatividade);
        }


        // Método para Salvar uma AtividadeRespostaAluno
        [HttpPost("saveatividaderesposta", Name="SaveAtividadeResposta")]
        public async Task<RespostaPadrao> SaveAtividadeResposta([FromBody] AtividadeRespostaAluno atividadeRespostaAluno){
            RespostaPadrao resposta = new RespostaPadrao();

            if(atividadeRespostaAluno.IdAtividade == 0)
            {
                resposta.SetCampoVazio("IdAtividade");
                return resposta;
            }

            if(atividadeRespostaAluno.IdPessoa == 0)
            {
                resposta.SetCampoVazio("IdPessoa");
                return resposta;
            }

            if (string.IsNullOrEmpty(atividadeRespostaAluno.DataEnvio.ToString()))
            {
                resposta.SetCampoVazio("DataEnvio");
                return resposta;
            }
            
            return await atividadeLogic.SaveAtividadeResposta(atividadeRespostaAluno);
        }


        // Método para Salvar uma QuestaoRespostaAluno
        [HttpPost("questaorespostaluno", Name="QuestaoRespostaAluno")]
        public async Task<RespostaPadrao> SaveQuestaoResposta([FromBody] List<QuestaoRespostaAluno> questoesRespostaAluno){
            RespostaPadrao resposta = new RespostaPadrao();

            if(questoesRespostaAluno.Count == 0)
            {
                resposta.SetCampoVazio("questoesRespostaAluno");
                return resposta;
            }
            
            return await atividadeLogic.SaveQuestaoResposta(questoesRespostaAluno);
        }

        [HttpGet("peganumerodeatividadesfeitas/{idaluno}", Name="PegaNumeroDeAtividadesFeitas")]
        public RespostaPadrao GetNumeroDeAtividadesFeitas(long idaluno){
            RespostaPadrao resposta = new RespostaPadrao();

            if(idaluno == 0)
            {
                resposta.SetCampoVazio("idaluno");
                return resposta;
            }
            
            return atividadeLogic.GetAtividadesFeitas(idaluno);
        }

        [HttpPost("addorupdateatividadealuno", Name="AddOrUpdateAtividadeAluno")]
        public async Task<RespostaPadrao> AddOrUpdateAtividadeAluno([FromBody]AtividadeAluno atividadeAluno){
            RespostaPadrao resposta = new RespostaPadrao();

            if(atividadeAluno.IdPessoa == 0)
            {
                resposta.SetCampoVazio("IdPessoa");
                return resposta;
            }

            if(atividadeAluno.IdAtividade == 0)
            {
                resposta.SetCampoVazio("IdAtividade");
                return resposta;
            }
            
            return await atividadeLogic.AddOrUpdateAtividadeAluno(atividadeAluno);
        }
    }
}