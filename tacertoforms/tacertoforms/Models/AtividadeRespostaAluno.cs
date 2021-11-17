using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaCertoForms.Models {
    [Table("AtividadeRespostaAluno")]
    public class AtividadeRespostaAluno {
        [Key]
        public int IdAtividadeRespostaAluno { get; set; }
        public int IdAtividade { get; set; }
        public int IdPessoa { get; set; }
        public DateTime DataEnvio { get; set; }
        public float Nota { get; set; }

        //NAVIGATION PROPERTY
        [ForeignKey("Atividade")]
        public int? AtividadeIdAtividade { get; set; }
        public Atividade Atividade { get; set; }
        [ForeignKey("Pessoa")]
        public int? PessoaIdPessoa { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}