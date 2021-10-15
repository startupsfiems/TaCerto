using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiTaCerto.Models
{
    [JsonObject, Serializable]
    public class Questao
    {
        [Key]
        public int IdQuestao { get; set; }
        public int IdAtividade { get; set; }
        public int IdTipoQuestao { get; set; }
        public string Titulo { get; set; }
        public string Enunciado { get; set; }
        public string JsonQuestao { get; set; }
        public float PesoNota { get; set; }
    }
}