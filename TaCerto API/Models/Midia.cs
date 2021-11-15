using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ApiTaCerto.Models.Usuario
{
    public enum TipoMidia
    {
        Indefinido,
        Imagem,
        Video,
        YouTube
    }
    [JsonObject, Serializable]
    public class Midia
    {
        [Key]
        public Guid IdMidia { get; set; }
        public int IdOrigem { get; set; }
        [MaxLength(150)]
        public string Tabela { get; set; }
        [MaxLength(150)]
        public string Filename { get; set; }
        [MaxLength(150)]
        public string Link { get; set; }
        [MaxLength(150)]
        public string Extensao { get; set; }
        public TipoMidia Tipo { get; set; }
    }
}