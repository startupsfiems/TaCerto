using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Atividade Atual/main")]
public class SO_AtividadeAtual_main : ScriptableObject{
    public int numeroDeQuestao;
    public Sprite[] imagem;
    public string[] texto;
    public bool[] taCerto;
}