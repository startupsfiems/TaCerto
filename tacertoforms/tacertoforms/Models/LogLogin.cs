using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaCertoForms.Models {
    [Table("LogLogin")]
    public class LogLogin {
        [Key]
        public int IdLogLogin { get; set; }
        public int IdPessoa { get; set; }
        public DateTime HoraAcesso { get; set; }
        [MaxLength(150)]
        public string Plataforma { get; set; }
        [MaxLength(150)]
        public string DeviceId { get; set; }
        [MaxLength(150)]
        public string DeviceIp { get; set; }
        public Origem Origem { get; set; }

        //NAVIGATION PROPERTY
        public Pessoa Pessoa { get; set; }
    }
}