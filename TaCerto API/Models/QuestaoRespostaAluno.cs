using System.ComponentModel.DataAnnotations;

namespace ApiTaCerto.Models
{
    public class QuestaoRespostaAluno
    {
        [Key]
        public int IdQuestaoRespostaAluno { get; set; }
        public int IdAtividadeRespostaAluno { get; set; }
        public int IdQuestao { get; set; }
        public int NumAcerto { get; set; }
        public int NumErro { get; set; }
        public string JsonReposta { get; set; }
        public float Nota { get; set; }

        //NAVIGATION PROPERTY
        public AtividadeRespostaAluno AtividadeRespostaAluno { get; set; }
        public Questao Questao { get; set; }
    }
}