using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ApiTaCerto.Models
{
    [JsonObject, Serializable]
    public class LogLogin
    {
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
        public int Origem { get; set; }
    }
}