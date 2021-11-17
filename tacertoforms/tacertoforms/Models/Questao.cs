using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaCertoForms.Models {
    [Table("Questao")]
    public class Questao {
        [Key]
        public int IdQuestao { get; set; }
        public int IdAtividade { get; set; }
        public int IdTipoQuestao { get; set; }
        public string Titulo { get; set; }
        public string Enunciado { get; set; }
        public string JsonQuestao { get; set; }
        public float PesoNota { get; set; }

        //NAVIGATION PROPERTY
        [ForeignKey("Atividade")]
        public int? AtividadeIdAtividade { get; set; }
        public Atividade Atividade { get; set; }
        [ForeignKey("TipoQuestao")]
        public int? TipoQuestaoIdTipoQuestao { get; set; }
        public TipoQuestao TipoQuestao { get; set; }
    }
}