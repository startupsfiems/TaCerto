using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Modos/Atividade")]
public class SO_ListaQuestoes : ScriptableObject{
    public List<IQuestao> listaQuestoes = new List<IQuestao>();
}