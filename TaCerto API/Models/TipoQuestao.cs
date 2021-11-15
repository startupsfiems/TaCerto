using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTaCerto.Models
{
    public class TipoQuestao
    {
        [Key]
        public int IdTipoQuestao { get; set; }
        [MaxLength(150)]
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
