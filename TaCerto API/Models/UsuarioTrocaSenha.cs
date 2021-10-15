using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTaCerto.Models
{
    public class UsuarioTrocaSenha
    {
        public int Id { get; set; }
        public string Senha { get; set; }
        public string NovaSenha { get; set; }
        public string ConfirmacaoNovaSenha { get; set; }
    }
}
