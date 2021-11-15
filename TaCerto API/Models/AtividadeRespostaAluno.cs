using System;
using System.ComponentModel.DataAnnotations;
using ApiTaCerto.Models.Usuario;
using Newtonsoft.Json;

namespace ApiTaCerto.Models
{
    [JsonObject, Serializable]
    public class AtividadeRespostaAluno
    {
        [Key]
        public int IdAtividadeRespostaAluno { get; set; }
        public int IdAtividade { get; set; }
        public int IdPessoa { get; set; }
        public DateTime DataEnvio { get; set; }
        public float Nota { get; set; }

        //NAVIGATION PROPERTY
        public Atividade Atividade { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}