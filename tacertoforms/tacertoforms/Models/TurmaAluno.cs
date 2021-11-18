using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaCertoForms.Models {
    [Table("TurmaAluno")]
    public class TurmaAluno {
        [Key]
        public int IdTurmaAluno { get; set; }
        public int IdTurma { get; set; }
        public int IdPessoa { get; set; }

        //NAVIGATION PROPERTY
        [ForeignKey("Turma")]
        public int? TurmaIdTurma { get; set; }
        public Turma Turma { get; set; }
        [ForeignKey("Pessoa")]
        public int? PessoaIdPessoa { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}