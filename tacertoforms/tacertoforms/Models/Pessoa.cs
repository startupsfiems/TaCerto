
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaCertoForms.Models {
    [Table("Pessoa")]
    public class Pessoa {
        [Key]
        public int IdPessoa { get; set; }
        public int IdInstituicao { get; set; }
        public Perfil Perfil { get; set; }
		[MaxLength(150)]
        public string Nome { get; set; }
		[MaxLength(14)]
        public string CPF { get; set; }
		[MaxLength(150)]
        public string Email { get; set; }
		[MaxLength(150)]
        public string Senha { get; set; }
        public string Token { get; set; }
        public DateTime? TokenDate { get; set; }

        //NAVIGATION PROPERTY
        [ForeignKey("Instituicao")]
        public int? InstituicaoIdInstituicao { get; set; }
        public Instituicao Instituicao { get; set; }
    }
}