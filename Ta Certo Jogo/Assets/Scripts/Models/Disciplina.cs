using System;
using System.Collections.Generic;

[Serializable]
public class Disciplina
{
    public int idDisciplina { get; set; }
    public string nome { get; set; }
    public string descricao { get; set; }
    public int idMatriz { get; set; }
    public int? corR { get; set; }
    public int? corG { get; set; }
    public int? corB { get; set; }
    public int? corA { get; set; }

    public bool isDefault { get; set; }

    [NonSerialized]
    public List<Atividade> atividades = new List<Atividade>();
}