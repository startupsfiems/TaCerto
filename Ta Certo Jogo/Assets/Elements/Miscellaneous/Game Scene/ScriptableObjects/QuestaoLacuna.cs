using UnityEngine.UI;

public class QuestaoLacuna : IQuestao{
    public TipoDeQuestao tipo {get {return TipoDeQuestao.LACUNA;}}
    public string[] texto;
    public bool certo;
}