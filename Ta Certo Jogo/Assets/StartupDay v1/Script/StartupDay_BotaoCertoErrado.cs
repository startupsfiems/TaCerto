using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupDay_BotaoCertoErrado : MonoBehaviour{
    public StartupDay_InformacoesSO info;
    public StartupDay_Gerente gerente;
    public bool minhaResposta;

    public void Resposta(){
        gerente.Resposta(((StartupDay_QuestaoNormal)info.atividade.questoes[info.indexQuestao]).ehCerto == minhaResposta);
    }
}