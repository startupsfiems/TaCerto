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
        public double Nota { get; set; }
    }
}