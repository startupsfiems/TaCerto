using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Modos/Questoes/Questoes Coluna")]
public class QuestaoColuna : IQuestao{
    public TipoDeQuestao tipo {get {return TipoDeQuestao.COLUNA;}}
}