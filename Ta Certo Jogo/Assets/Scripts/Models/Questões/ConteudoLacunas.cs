using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

[Serializable]
public class ConteudoLacunas : Conteudo
{
    private bool[] fraseIsTexto;
    private string[] fraseTexto;
    private int[][] alternativaIndex;
    private string[] alternativaTexto;

    public bool[] FraseIsTexto { get => fraseIsTexto; set => fraseIsTexto = value; }
    public string[] FraseTexto { get => fraseTexto; set => fraseTexto = value; }
    public int[][] AlternativaIndex { get => alternativaIndex; set => alternativaIndex = value; }
    public string[] AlternativaTexto { get => alternativaTexto; set => alternativaTexto = value; }

    public ConteudoLacunas(Questao questao)
    {
        IdQuestao = questao.idQuestao;
        // passar o json para os atributos
        JObject data = JObject.Parse(questao.jsonQuestao);

        List<Frase> frases = JsonConvert.DeserializeObject<List<Frase>>(data["frase"].ToString());
        fraseIsTexto = new bool[frases.Count];
        fraseTexto = new string[frases.Count];
        int count = frases.Count;
        for(int i = 0; i < count; i++)
        {
            fraseIsTexto[i] = frases[i].isTexto;
            fraseTexto[i] = frases[i].texto;
        }
        
        List<Alternativa> alternativas = JsonConvert.DeserializeObject<List<Alternativa>>(data["alternativa"].ToString());
        alternativaIndex = new int[alternativas.Count][];
        alternativaTexto = new string[alternativas.Count];
        count = alternativas.Count;
        for(int i = 0; i < count; i++)
        {
            int countJ = alternativas[i].index.Length;
            alternativaIndex[i] = new int[countJ];
            for(int j = 0; j < countJ; j++)
                alternativaIndex[i][j] = alternativas[i].index[j];
            alternativaTexto[i] = alternativas[i].texto;
        }
    }   
}