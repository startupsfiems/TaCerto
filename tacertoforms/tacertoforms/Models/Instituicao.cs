using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaCertoForms.Models {
    [Table("Instituicao")]
    public class Instituicao {
        [Key]
        public int IdInstituicao { get; set; }
        [MaxLength(150)]
        public string RazaoSocial { get; set; }
        [MaxLength(150)]
        public string NomeFantasia { get; set; }
        [MaxLength(18)]
        public string CNPJ { get; set; }
        [MaxLength(150)]
        public string Email { get; set; }
        [MaxLength(25)]
        public string Telefone { get; set; }
        public int IdEnderecoPrincipal { get; set; }
        public int IdEnderecoCobranca { get; set; }
        public bool IsMatriz { get; set; } = false;
        public int? IdMatriz { get; set; }

        //NAVIGATION PROPERTY
        [ForeignKey("EnderecoPrincipal")]
        public int? EnderecoPrincipalIdEndereco { get; set; }
        public Endereco EnderecoPrincipal { get; set; }
        [ForeignKey("EnderecoCobranca")]
        public int? EnderecoCobrancaIdEndereco { get; set; }
        public Endereco EnderecoCobranca { get; set; }
        [ForeignKey("Matriz")]
        public int? MatrizIdInstituicao { get; set; }
        public Instituicao Matriz { get; set; }
    }
}