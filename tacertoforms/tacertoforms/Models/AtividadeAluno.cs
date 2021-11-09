using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaCertoForms.Models {
    [Table("AtividadeAluno")]
    public class AtividadeAluno {
        [Key]
        public int IdAtividadeAluno { get; set; }
        public int NumeroTentativas { get; set; }
        public int IdPessoa { get; set; }
        public int IdAtividade { get; set; }
        public double MaiorNota { get; set; }
        public int MenorTempo { get; set; }
        public int MaiorTempo { get; set; }

        //NAVIGATION PROPERTY
        public Pessoa Pessoa { get; set; }
        public Atividade Atividade { get; set; }
    }
}