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
        public int IdMatriz { get; set; }
        [MaxLength(150)]
        public string Nome { get; set; }
        [MaxLength(150)]
        public string Descricao { get; set; }
        public int? CorR;
        public int? CorG;
        public int? CorB;
        public int? CorA;

        //NAVIGATION PROPERTY
        public Instituicao Matriz { get; set; }
    }
}