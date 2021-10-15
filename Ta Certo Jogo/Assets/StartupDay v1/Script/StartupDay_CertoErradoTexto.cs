using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartupDay_CertoErradoTexto : MonoBehaviour{
    public StartupDay_InformacoesSO info;
    TMP_Text text;

    void Awake(){ text = GetComponent<TMP_Text>(); }
    
    void Update(){
        if(info.atividade.questoes[info.indexQuestao].tipo == TIPO.NORMAL){
            text.text = ((StartupDay_QuestaoNormal)info.atividade.questoes[info.indexQuestao]).frase;
        }
    }
}