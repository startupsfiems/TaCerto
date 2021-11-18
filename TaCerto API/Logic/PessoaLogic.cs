using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiTaCerto.Models;
using ApiTaCerto.Models.DTOs;
using ApiTaCerto.Models.Usuario;
using ApiTaCerto.Repositorio;
using Microsoft.IdentityModel.Tokens;

namespace ApiTaCerto.Logic
{
    public class PessoaLogic
    {
        private readonly IPessoaRepository _pessoaRepositorio;

        private readonly string SecurityKey = "fiowejfjwdofqjwodfijwedoifjwoifjawodfijawo";

        public PessoaLogic(IPessoaRepository pessoaRepo)
        {
            _pessoaRepositorio = pessoaRepo;
        }

        public RespostaPadrao GetAll()
        {
            RespostaPadrao resposta = new RespostaPadrao();

            IEnumerable<Pessoa> pessoas = _pessoaRepositorio.GetAll();
            if (pessoas == null)
            {
                resposta.SetNaoEncontrado("Pessoas não encontradas");
                return resposta;
            }
            resposta.Dado = pessoas;

            return resposta;
        }

        public RespostaPadrao GetPessoaById(long id)
        {
            RespostaPadrao resposta = new RespostaPadrao();

            Pessoa pessoa = _pessoaRepositorio.Find(id);
            if (pessoa == null)
            {
                resposta.SetNaoEncontrado("Pessoa não encontrada");
                return resposta;
            }

            PessoaInfo pessoaInfo = new PessoaInfo(pessoa.IdPessoa, pessoa.IdInstituicao, (int)pessoa.Perfil, pessoa.Nome, pessoa.Email);

            resposta.Dado = pessoaInfo;

            return resposta;
        }

        public RespostaPadrao GetTokenById(long id)
        {
            RespostaPadrao resposta = new RespostaPadrao();

            PessoaToken pessoaToken = _pessoaRepositorio.FindPessoaToken(id);
            if(pessoaToken == null)
            {
                resposta.SetNaoEncontrado("Pessoa Token não encontrado");
                return resposta;
            }

            pessoaToken.IdTurma = GetIdTurmaOrNull(pessoaToken.IdPessoa);
            resposta.Dado = pessoaToken;

            return resposta;
        }

        public async Task<RespostaPadrao> ValidarLogin(string email, string senha){

            RespostaPadrao resposta = new RespostaPadrao("Autenticado");
            // code == 1 == Autenticado
            // code == 0 == Não autenticado (Email ou senha incorreto)
            Pessoa pessoa = _pessoaRepositorio.Find(email, 2);
            if(pessoa == null)
            {
                resposta.SetNaoEncontrado("Email e/ou Senha incorreto(s)");
                return resposta;
            }

            if (!pessoa.Senha.Equals(senha))
            {
                resposta.SetNaoEncontrado("Email e/ou Senha incorreto(s)");
                return resposta;
            }

            PessoaToken pessoaToken = new PessoaToken();
            pessoaToken.IdPessoa = pessoa.IdPessoa;
            pessoaToken.Autenticado = true;

            pessoaToken.IdTurma = GetIdTurmaOrNull(pessoaToken.IdPessoa);
            pessoaToken = await SetNewToken(pessoaToken, pessoa);
            resposta.Dado = pessoaToken;

            return resposta;
        }

        public RespostaPadrao RemoveToken(long IdPessoaToken){
            RespostaPadrao resposta = new RespostaPadrao("Token removido com sucesso");

            PessoaToken pessoaToken = _pessoaRepositorio.FindPessoaToken(IdPessoaToken);
            if (pessoaToken == null)
            {
                resposta.SetNaoEncontrado("Token não encontrado");
                return resposta;
            }

            try
            {
                _pessoaRepositorio.RemovePessoaToken(pessoaToken);
            }
            catch
            {
                resposta.SetErroInterno("Erro ao remover Token");
            }

            return resposta; ;
        }

        public async Task<RespostaPadrao> AtualizaPessoa(long id, PessoaUpdate pessoaUpdate)
        {
            RespostaPadrao resposta = new RespostaPadrao("Atualizado com sucesso");

            Pessoa pessoa = _pessoaRepositorio.Find(pessoaUpdate.Id);
            if (pessoa == null)
            {
                resposta.SetNaoEncontrado("Usuário não encontrado");
                return resposta;
            }

            if(id != pessoa.IdPessoa)
            {
                resposta.SetCampoIncorreto("O Id informado não corresponde ao Id do Usuário");
                return resposta;
            }

            try
            {
                pessoa.Nome = pessoaUpdate.Nome;
                await _pessoaRepositorio.Update(pessoa);
            }
            catch
            {
                resposta.SetErroInterno("Erro ao atualizar Usuário");
            }

            return resposta;
        }

        public async Task<RespostaPadrao> TrocaSenha(UsuarioTrocaSenha usuarioTrocaSenha){
            RespostaPadrao resposta = new RespostaPadrao("Senha alterada com sucesso");

            // Tenta pegar o Usuario
            Pessoa pessoa = _pessoaRepositorio.Find(usuarioTrocaSenha.Id);
            if(pessoa == null)
            {
                resposta.SetNaoEncontrado("Usuário não encontrado");
                return resposta;
            }

            // string senhaDescri = Criptografia.Decrypt(pessoa.Senha);

            if (!usuarioTrocaSenha.Senha.Equals(pessoa.Senha))
            {
                resposta.SetCampoIncorreto("A senha atual não está correta");
                return resposta;
            }

            try
            {
                // string novaSenhaCript = Criptografia.Encrypt(usuarioTrocaSenha.NovaSenha);
                pessoa.Senha = usuarioTrocaSenha.NovaSenha;
                await _pessoaRepositorio.Update(pessoa);
            }
            catch
            {
                resposta.SetErroInterno("Erro ao remover Token");
            }

            return resposta;
        }

        public async Task<RespostaPadrao> SaveLogLogin(LogLogin logLogin)
        {
            RespostaPadrao resposta = new RespostaPadrao();

            Pessoa pessoa = _pessoaRepositorio.Find(logLogin.IdPessoa);
            if(pessoa == null)
            {
                resposta.SetNaoEncontrado("Usuário não encontrado");
                return resposta;
            }

            string result = await _pessoaRepositorio.SaveLogLogin(logLogin);
            resposta.SetMensagem(result);

            return resposta;
        }

        public RespostaPadrao GetTurma(int idTurma){
            RespostaPadrao resposta = new RespostaPadrao();

            Turma turma = _pessoaRepositorio.FindTurma(idTurma);
            if(turma == null)
            {
                resposta.SetNaoEncontrado("Turma não encontrada");
                return resposta;
            }
            resposta.Dado = turma;
            
            return resposta;
        }

        private async Task<PessoaToken> SetNewToken(PessoaToken pessoaToken, Pessoa pessoa)
        {
            var claims = new []{
                    new Claim(ClaimTypes.Name, pessoa.IdPessoa.ToString()),
                };

                string data = DateTime.Now.Date.ToString();
                string toGenerateKey = pessoa.CPF+SecurityKey + pessoa.Nome;

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(toGenerateKey)
                );

                pessoaToken.Message = "Usuário autenticado";

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "sesi",
                    audience: "usuário",
                    claims: claims,
                    expires: DateTime.Now.AddYears(13),
                    signingCredentials: creds);

                pessoaToken.Token = new JwtSecurityTokenHandler().WriteToken(token);
                await _pessoaRepositorio.AddPessoaToken(pessoaToken);

            return pessoaToken;
        }

        private int GetIdTurmaOrNull(int idPessoa)
        {
            TurmaAluno turmaAluno = _pessoaRepositorio.FindTurmaAluno(idPessoa);

            if (turmaAluno == null)
            {
                return -1;
            }
            else
            {
                return turmaAluno.IdTurma;
            }
        }
    }
}