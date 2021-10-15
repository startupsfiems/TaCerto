using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ApiTaCerto.Models.Usuario
{
    [JsonObject, Serializable]
    public class PessoaPerfil
    {
        [Key]
        public int IdPessoaToken { get; set; }
        [MaxLength(50)]
        public string Token { get; set; }
        [MaxLength(50)]
        public string Message { get; set; }
        public bool Autenticado { get; set; }
        public int IdPessoa { get; set; }
    }
}