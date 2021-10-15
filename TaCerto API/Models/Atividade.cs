using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiTaCerto.Models
{
    [JsonObject, Serializable]
    public class Atividade
    {
        [Key]
        public int IdAtividade { get; set; }
        public int IdTurmaDisciplinaAutor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int NumeroTentativas { get; set; }
        public bool IsAleatorio { get; set; }
        public bool IsProva { get; set; }
        public string Titulo { get; set; }
        public int NumeroQuestoes { get; set; }
    }
}