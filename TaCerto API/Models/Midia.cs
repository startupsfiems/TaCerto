using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ApiTaCerto.Models.Usuario
{
    [JsonObject, Serializable]
    public class Midia
    {
        [Key]
        public Guid IdMidia { get; set; }       
        public int IdOrigem { get; set; }
        public string Tabela { get; set; }
        public string Filename { get; set; }
        public string Link { get; set; }
        public string Extensao { get; set; }
        public int Tipo { get; set; }
    }
}