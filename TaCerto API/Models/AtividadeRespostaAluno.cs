using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiTaCerto.Models
{
    [JsonObject, Serializable]
    public class AtividadeRespostaAluno
    {
        [Key]
        public int IdAtividadeRespostaAluno { get; set; }
        [Required]
        public int IdAtividade { get; set; }
        [Required]
        public int IdPessoa { get; set; }
        [Required]
        public DateTime DataEnvio { get; set; }
        [Required]
        public float Nota { get; set; }
    }
}