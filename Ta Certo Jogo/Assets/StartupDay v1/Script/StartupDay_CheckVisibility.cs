using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupDay_CheckVisibility : MonoBehaviour{
    public StartupDay_InformacoesSO info;
    public TIPO tipo;

    void Update(){
        CanvasGroup cg = GetComponent<CanvasGroup>();
        if(info.atividade.questoes[info.indexQuestao].tipo == tipo){
            cg.alpha = Mathf.Lerp(cg.alpha, 1f, 0.3f);
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }
        else{
            cg.alpha = Mathf.Lerp(cg.alpha, 0f, 0.3f);
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
    }
}