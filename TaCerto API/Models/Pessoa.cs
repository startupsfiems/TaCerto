using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTaCerto.Models.Usuario
{
    public class Pessoa
    {
        [Key]
        public int IdPessoa { get; set; }
        public int IdInstituicao { get; set; }
        public int Perfil { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}