using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTaCerto.Models.DTOs
{
    public class PessoaInfo
    {
        public int IdPessoa { get; set; }
        public int IdInstituicao { get; set; }
        public int Perfil { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public PessoaInfo(int id, int idInst, int perfil, string nome, string email)
        {
            IdPessoa = id;
            IdInstituicao = idInst;
            Perfil = perfil;
            Nome = nome;
            Email = email;
        }
    }
}
