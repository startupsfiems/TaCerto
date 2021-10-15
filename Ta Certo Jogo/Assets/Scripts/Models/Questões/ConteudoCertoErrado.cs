using Newtonsoft.Json.Linq;
using System;

[Serializable]
public class ConteudoCertoErrado : Conteudo
{
    private bool isVerdadeiro;
    public bool IsVerdadeiro { get => isVerdadeiro; set => isVerdadeiro = value; }

    public ConteudoCertoErrado(Questao questao)
    {
        IdQuestao = questao.idQuestao;
        // Passar o json para o atributo
        JObject data = JObject.Parse(questao.jsonQuestao);
        IsVerdadeiro = (bool)data["isVerdadeiro"];
    }
}