using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaCertoForms.Models {
    [Table("Disciplina")]
    public class Disciplina {
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
        [ForeignKey("Matriz")]
        public int? MatrizIdInstituicao { get; set; }
        public Instituicao Matriz { get; set; }
    }
}