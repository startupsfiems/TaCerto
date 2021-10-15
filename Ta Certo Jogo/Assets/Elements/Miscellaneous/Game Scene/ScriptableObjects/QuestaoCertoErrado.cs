using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Modos/Questoes/Questoes Certo Errado")]
public class QuestaoCertoErrado : IQuestao{
    public TipoDeQuestao tipo {get {return TipoDeQuestao.CERTO_ERRADO;}}
    public bool comImagem;
    public Sprite img;
    public string texto;
    public bool certo;
}