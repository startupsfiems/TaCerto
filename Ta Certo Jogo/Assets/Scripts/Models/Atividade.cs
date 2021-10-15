using System;
using System.Collections.Generic;

[Serializable]
public class Atividade
{
    public int idAtividade { get; set; }
    public int idTurmaDisciplinaAutor { get; set; }
    public string dataInicio { get; set; }
    public string dataFim { get; set; }
    public int numeroTentativas { get; set; }
    public bool isAleatorio { get; set; }
    public bool isProva { get; set; }
    public string titulo { get; set; }
    public int numeroTentativasAtuais { get; set; }
    public double maiorNota { get; set; }
    public int menorTempo { get; set; }
    public int maiorTempo { get; set; }
    public int idAtividadeAluno { get; set; }
    public int numeroQuestoes { get; set; }

    [NonSerialized]
    public List<Questao> questoes = new List<Questao>();
}