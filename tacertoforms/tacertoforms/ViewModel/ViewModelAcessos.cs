using System;
using System.Collections.Generic;
using System.Globalization;

namespace TaCertoForms.Models {
    public class ViewModelAcessos {
        public int id_pessoa { get; set; }
        public string nome { get; set; }
        public int numero_acesso { get; set; }
        public int atividades_desenvolvidas { get; set; }
        public DateTime ultimo_acesso { get; set; }
        public string ultimo_acesso_string {
            get {
                return ultimo_acesso.ToString("dd/MM/yyyy hh:mm:ss", DateTimeFormatInfo.InvariantInfo);
            }
        }
    }
}