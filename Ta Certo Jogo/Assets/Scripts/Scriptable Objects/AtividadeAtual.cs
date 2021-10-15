using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Atividade/AtividadeAtual")]
public class AtividadeAtual : ScriptableObject
{
    public int idAtividade;
    public int idDisciplina;
    public string whereToReturn;
    public string disciplina;
    public string titulo;
    public int numeroTentativas;
    public int acertos;
    public int erros;
    public int numQuestoes;
    public double maiorNota;
    public int menorTempo;
    public int maiorTempo;
    public bool isAleatorio;
    public bool precisaSalvarRespostas;
    public int numeroTentativasAtuais;
    public int idAtividadeAluno;

    public bool userJaSabeCertoErrado;
    public bool userJaSabeLacunas;

    public List<Questao> questoes = new List<Questao>();
}