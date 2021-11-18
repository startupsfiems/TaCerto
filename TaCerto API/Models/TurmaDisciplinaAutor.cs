using ApiTaCerto.Models.Usuario;
using System.ComponentModel.DataAnnotations;

namespace ApiTaCerto.Models
{
    public class TurmaDisciplinaAutor
    {
        [Key]
        public int IdTurmaDisciplinaAutor { get; set; }
        public int IdAutor { get; set; }
        public int IdDisciplinaTurma { get; set; }

        //NAVIGATION PROPERTY
        public Pessoa Autor { get; set; }
        public DisciplinaTurma DisciplinaTurma { get; set; }
    }
}
