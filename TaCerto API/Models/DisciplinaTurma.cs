using System.ComponentModel.DataAnnotations;
using System;

namespace ApiTaCerto.Models
{
    [Serializable]
    public class DisciplinaTurma
    {
        [Key]
        public int IdDisciplinaTurma { get; set; }
        public int IdDisciplina { get; set; }
        public int IdTurma { get; set; }
    }
}