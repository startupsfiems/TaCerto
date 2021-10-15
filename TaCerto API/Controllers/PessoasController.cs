using ApiTaCerto.Logic;
using ApiTaCerto.Models;
using ApiTaCerto.Models.DTOs;
using ApiTaCerto.Models.Usuario;
using ApiTaCerto.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiTaCerto.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[Controller]")]
    //[Authorize()]
    public class PessoasController : Controller
    {
        private readonly IPessoaRepository _pessoaRepositorio;
        private PessoaLogic pessoaLogic;

        public PessoasController(IPessoaRepository pessoaRepo)
        {
            _pessoaRepositorio = pessoaRepo;
            pessoaLogic = new PessoaLogic(_pessoaRepositorio);
        }

        [HttpGet]
        public RespostaPadrao GetAll(){
            return pessoaLogic.GetAll();
        }

        [HttpGet("{id}", Name="GetById")]
        public RespostaPadrao GetById(long id){
            RespostaPadrao resposta = new RespostaPadrao();

            if(id == 0)
            {
                resposta.SetCampoVazio("id");
                return resposta;
            }

            return pessoaLogic.GetPessoaById(id);
        }

        [HttpGet("token/{id}", Name="GetPessoaToken")]
        public RespostaPadrao GetTokenById(long id){
            RespostaPadrao resposta = new RespostaPadrao();

            if(id == 0)
            {
                resposta.SetCampoVazio("id");
                return resposta;
            }

            return pessoaLogic.GetTokenById(id);
        }

        [HttpPut("{id}")]
        public async Task<RespostaPadrao> Update(long id, [FromBody] PessoaUpdate pessoaUpdate)
        {
            RespostaPadrao resposta = new RespostaPadrao();

            if(pessoaUpdate.Id == 0)
            {
                resposta.SetCampoVazio("Id");
                return resposta;
            }

            if (string.IsNullOrEmpty(pessoaUpdate.Nome))
            {
                resposta.SetCampoVazio("Nome");
                return resposta;
            }

            return await pessoaLogic.AtualizaPessoa(id, pessoaUpdate);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<RespostaPadrao> Login([FromBody] PessoaLogin pessoaLogin){

            RespostaPadrao resposta = new RespostaPadrao();

            if (string.IsNullOrEmpty(pessoaLogin.Email))
            {
                resposta.SetCampoVazio("Email");
                return resposta;
            }

            if (string.IsNullOrEmpty(pessoaLogin.Senha))
            {
                resposta.SetCampoVazio("Senha");
                return resposta;
            }

            return await pessoaLogic.ValidarLogin(pessoaLogin.Email, pessoaLogin.Senha);
        }

        [HttpPost("logout", Name="RemoveToken")]
        public RespostaPadrao Logout([FromQuery] int IdPessoaToken){
            RespostaPadrao resposta = new RespostaPadrao();
            if(IdPessoaToken == 0)
            {
                resposta.SetCampoVazio("IdPessoaToken");
                return resposta;
            }

            return pessoaLogic.RemoveToken(IdPessoaToken);
        }

        [HttpPost("saveloglogin", Name="SaveLogLogin")]
        public async Task<RespostaPadrao> SaveLogLogin([FromBody] LogLogin logLogin){
            RespostaPadrao resposta = new RespostaPadrao();

            if(logLogin.IdPessoa == 0)
            {
                resposta.SetCampoVazio("IdPessoa");
                return resposta;
            }

            if (string.IsNullOrEmpty(logLogin.HoraAcesso.ToString()))
            {
                resposta.SetCampoVazio("HoraAcesso");
                return resposta;
            }

            if (string.IsNullOrEmpty(logLogin.Plataforma))
            {
                resposta.SetCampoVazio("Plataforma");
                return resposta;
            }

            if (string.IsNullOrEmpty(logLogin.DeviceId))
            {
                resposta.SetCampoVazio("DeviceId");
                return resposta;
            }

            if (string.IsNullOrEmpty(logLogin.DeviceIp))
            {
                resposta.SetCampoVazio("DeviceIp");
                return resposta;
            }

            return await pessoaLogic.SaveLogLogin(logLogin);
        }

        [HttpPost("trocasenha", Name="TrocaSenha")]
        public async Task<RespostaPadrao> TrocaSenha([FromBody] UsuarioTrocaSenha usuarioTrocaSenha)
        {
            RespostaPadrao resposta = new RespostaPadrao();

            if(usuarioTrocaSenha.Id == 0)
            {
                resposta.SetCampoVazio("Id");
                return resposta;
            }

            if (string.IsNullOrEmpty(usuarioTrocaSenha.Senha))
            {
                resposta.SetCampoVazio("Senha");
                return resposta;
            }

            if (string.IsNullOrEmpty(usuarioTrocaSenha.NovaSenha))
            {
                resposta.SetCampoVazio("NovaSenha");
                return resposta;
            }

            if (usuarioTrocaSenha.NovaSenha.Length < 6)
            {
                resposta.SetCampoIncorreto("A nova senha deve possuir ao menos 6 caracteres");
                return resposta;
            }

            if (string.IsNullOrEmpty(usuarioTrocaSenha.ConfirmacaoNovaSenha))
            {
                resposta.SetCampoVazio("ConfirmacaoNovaSenha");
                return resposta;
            }

            if (!usuarioTrocaSenha.NovaSenha.Equals(usuarioTrocaSenha.ConfirmacaoNovaSenha))
            {
                resposta.SetCampoIncorreto("A nova senha e a confirmação estão diferentes");
                return resposta;
            }

            return await pessoaLogic.TrocaSenha(usuarioTrocaSenha);
        }

        [HttpGet("turma/{idTurma}", Name="GetTurma")]
        public RespostaPadrao GetTurma(int idTurma){
            RespostaPadrao resposta = new RespostaPadrao();

            if(idTurma == 0)
            {
                resposta.SetCampoVazio("idTurma");
                return resposta;
            }

            return pessoaLogic.GetTurma(idTurma);
        }

        [AllowAnonymous]
        [HttpGet("checaConexao", Name="CheckConnection")]
        public RespostaPadrao CheckConnection(){
            RespostaPadrao resposta = new RespostaPadrao("Ok");
            return resposta;
        }   
    }
}