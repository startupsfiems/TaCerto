using System.ComponentModel.DataAnnotations;

namespace ApiTaCerto.Models
{
    public class Endereco
    {
        [Key]
        public int IdEndereco { get; set; }
        [MaxLength(150)]
        public string Pais { get; set; }
        [MaxLength(2)]
        public string UF { get; set; }
        [MaxLength(150)]
        public string Cidade { get; set; }
        public int Numero { get; set; }
        [MaxLength(150)]
        public string Complemento { get; set; }
        [MaxLength(10)]
        public string CEP { get; set; }
        [MaxLength(150)]
        public string Logradouro { get; set; }
        [MaxLength(150)]
        public string Bairro { get; set; }
    }
}
