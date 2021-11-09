using System;
using System.Collections.Generic;
using System.Globalization;

namespace TaCertoForms.Models {
    public class ViewModelAtividadeAluno {
        public int IdAtividadeAluno { get; set; }
        public int NumeroTentativas { get; set; }
        public int IdPessoa { get; set; }
        public int IdAtividade { get; set; }
        public double MaiorNota { get; set; }
        public int MenorTempo { get; set; }
        public int MaiorTempo { get; set; }

        public string nome_aluno { get; set; }
        public string menor_tempo {
            get {
                int h = (int) Math.Floor((double)(MenorTempo/3600));
                int m = (int) Math.Floor((double)((MenorTempo/60)%60));
                int s = MenorTempo%60;
                string txt = "";
                txt += h > 0 ? h + "h " : "";
                txt += m > 0 ? m + "m " : "";
                txt += s > 0 ? s + "s" : "";
                return txt;
            }
        }
        public string maior_tempo {
            get {
                int h = (int) Math.Floor((double)(MaiorTempo/3600));
                int m = (int) Math.Floor((double)((MaiorTempo/60)%60));
                int s = MaiorTempo%60;
                string txt = "";
                txt += h > 0 ? h + "h " : "";
                txt += m > 0 ? m + "m " : "";
                txt += s > 0 ? s + "s" : "";
                return txt;
            }
        }

        public AtividadeAluno AtividadeAluno {
            get {
                return new AtividadeAluno {
                    IdAtividadeAluno = this.IdAtividadeAluno,
                    NumeroTentativas = this.NumeroTentativas,
                    IdPessoa = this.IdPessoa,
                    IdAtividade = this.IdAtividade,
                    MaiorNota = this.MaiorNota,
                    MenorTempo = this.MenorTempo,
                    MaiorTempo = this.MaiorTempo
                };
            }
            set {
                this.IdAtividadeAluno = value.IdAtividadeAluno;
                this.NumeroTentativas = value.NumeroTentativas;
                this.IdPessoa = value.IdPessoa;
                this.IdAtividade = value.IdAtividade;
                this.MaiorNota = value.MaiorNota;
                this.MenorTempo = value.MenorTempo;
                this.MaiorTempo = value.MaiorTempo;
            }
        }
    }
}