using System.ComponentModel.DataAnnotations;

namespace ApiTaCerto.Models
{
    public class Instituicao
    {
        [Key]
        public int IdInstituicao { get; set; }
        public string CNPJ { get; set; }
    }
}