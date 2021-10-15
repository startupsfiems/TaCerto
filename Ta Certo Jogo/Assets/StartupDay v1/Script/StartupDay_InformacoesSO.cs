using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StartupDay.v1/Informacoes")]
public class StartupDay_InformacoesSO : ScriptableObject{
    public StartupDay_Atividade atividade;
    public int indexQuestao;

    public int acertos, erros;
    public float tempo;

    public int indexOpcaoArrastada;
}