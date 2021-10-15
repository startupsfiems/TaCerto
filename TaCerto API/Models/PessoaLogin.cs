using System;
using Newtonsoft.Json;

namespace ApiTaCerto.Models.Usuario
{
    [JsonObject, Serializable]
    public class PessoaLogin
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}