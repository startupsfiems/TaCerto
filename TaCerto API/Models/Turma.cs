using System.ComponentModel.DataAnnotations;

namespace ApiTaCerto.Models
{
    public class Turma
    {
        [Key]
        public int IdTurma { get; set; }
        public int IdInstituicao { get; set; }
        public string Serie { get; set; }
        public int Periodo { get; set; }
    }
}