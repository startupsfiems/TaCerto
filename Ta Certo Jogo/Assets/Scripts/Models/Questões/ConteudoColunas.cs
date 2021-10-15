using System;

[Serializable]
public class ConteudoColunas : Conteudo
{
    private bool[] isTexto;
    private string[] frasesTexto;
    private int[] alternativaIndex;
    private string[] alternativaTexto;

    public bool[] IsTexto { get => isTexto; set => isTexto = value; }
    public string[] FrasesTexto { get => frasesTexto; set => frasesTexto = value; }
    public int[] AlternativaIndex { get => alternativaIndex; set => alternativaIndex = value; }
    public string[] AlternativaTexto { get => alternativaTexto; set => alternativaTexto = value; }

    public ConteudoColunas(Questao questao)
    {
        IdQuestao = questao.idQuestao;
        // passar o json para os atributos
    }
}