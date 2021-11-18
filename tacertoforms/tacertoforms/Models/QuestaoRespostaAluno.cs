using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaCertoForms.Models {
    [Table("QuestaoRespostaAluno")]
    public class QuestaoRespostaAluno {
        [Key]
        public int IdQuestaoRespostaAluno { get; set; }
        public int IdAtividadeRespostaAluno { get; set; }
        public int IdQuestao { get; set; }
        public int NumAcerto { get; set; }
        public int NumErro { get; set; }
        public string JsonReposta { get; set; }
        public float Nota { get; set; }

        //NAVIGATION PROPERTY
        [ForeignKey("AtividadeRespostaAluno")]
        public int? AtividadeRespostaAlunoIdAtividadeRespostaAluno { get; set; }
        public AtividadeRespostaAluno AtividadeRespostaAluno { get; set; }
        [ForeignKey("Questao")]
        public int? QuestaoIdQuestao { get; set; }
        public Questao Questao { get; set; }
    }
}