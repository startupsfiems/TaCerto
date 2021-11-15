using System.ComponentModel.DataAnnotations;

namespace ApiTaCerto.Models
{
    public enum Periodo
    {
        Matutino,
        Vespertino,
        Noturno,
        Integral
    }

    public class Turma
    {
        [Key]
        public int IdTurma { get; set; }
        public int IdInstituicao { get; set; }
        public string Serie { get; set; }
        public Periodo Periodo { get; set; } //nullable + enum(matutino, vespertino, noturno, integral)

        //NAVIGATION PROPERTY
        public Instituicao Instituicao { get; set; }
    }
}