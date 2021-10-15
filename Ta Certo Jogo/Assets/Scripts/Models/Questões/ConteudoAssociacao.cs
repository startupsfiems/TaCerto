using System;

[Serializable]
public class ConteudoAssociacao : Conteudo
{
    private string[] certas;
    private string[] erradas;

    public string[] Certas { get => certas; set => certas = value; }
    public string[] Erradas { get => erradas; set => erradas = value; }

    public ConteudoAssociacao(Questao questao)
    {
        IdQuestao = questao.idQuestao;
        // passar o json para os atributos
    }
}