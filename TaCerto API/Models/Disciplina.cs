using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiTaCerto.Models
{
    [JsonObject, Serializable]
    public class Disciplina
    {
        [Key]
        public int IdDisciplina { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int IdMatriz { get; set; }
        public int? CorR { get; set; }
        public int? CorG { get; set; }
        public int? CorB { get; set; }
        public int? CorA { get; set; }
    }
}