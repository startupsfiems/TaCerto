using ApiTaCerto.Logic;
using ApiTaCerto.Models;
using ApiTaCerto.Models.Usuario;
using ApiTaCerto.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ApiTaCerto.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[Controller]")]
    //[Authorize()]
    public class MidiasController : Controller
    {
        private readonly IMidiaRepository _midiaRepositorio;
        private MidiaLogic midiaLogic;

        public MidiasController(IMidiaRepository midiaRepo)
        {
            _midiaRepositorio = midiaRepo;
            midiaLogic = new MidiaLogic(_midiaRepositorio);
        }

        [HttpGet("pessoafoto/{idOrigem}", Name="GetPessoaFoto")]
        public RespostaPadrao GetPessoaMidia(int idOrigem){
            RespostaPadrao resposta = new RespostaPadrao();
            if(idOrigem == 0)
            {
                resposta.SetCampoVazio("idOrigem");
                return resposta;
            }
            return midiaLogic.GetMidia(idOrigem, "Pessoa");
        }
 
        [AllowAnonymous]
        [HttpGet("questaoFoto/{idOrigem}", Name="GetQuestaoFoto")]
        public RespostaPadrao GetQuestaoMidia(int idOrigem){
            RespostaPadrao resposta = new RespostaPadrao();
            if (idOrigem == 0)
            {
                resposta.SetCampoVazio("idOrigem");
                return resposta;
            }
            return midiaLogic.GetMidia(idOrigem, "Questao");
        }
    }
}