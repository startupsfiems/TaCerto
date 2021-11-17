using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaCertoForms.Models {
    [Table("Turma")]
    public class Turma {
        [Key]
        public int IdTurma { get; set; }
        public int IdInstituicao { get; set; }
        public string Serie { get; set; }
        public Periodo Periodo { get; set; } //nullable + enum(matutino, vespertino, noturno, integral)

        //NAVIGATION PROPERTY
        [ForeignKey("Instituicao")]
        public int? InstituicaoIdInstituicao { get; set; }
        public Instituicao Instituicao { get; set; }
    }
}