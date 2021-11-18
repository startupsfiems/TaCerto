using ApiTaCerto.Models.Usuario;
using System.ComponentModel.DataAnnotations;

namespace ApiTaCerto.Models
{
    public class TurmaAluno
    {
        [Key]
        public int IdTurmaAluno { get; set; }
        public int IdTurma { get; set; }
        public int IdPessoa { get; set; }

        //NAVIGATION PROPERTY
        public Turma Turma { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}