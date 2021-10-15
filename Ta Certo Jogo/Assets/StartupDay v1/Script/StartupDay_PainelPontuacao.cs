using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartupDay_PainelPontuacao : MonoBehaviour{
    public TMP_Text acerto, erro, tempo;
    public StartupDay_InformacoesSO info;//
    
    public void Update(){
        acerto.text = info.acertos.ToString();
        erro.text = info.erros.ToString();
        info.tempo += Time.deltaTime;
        int aux = (int)info.tempo;
        tempo.text = ((aux / 60 < 10) ? "0" : "") + (aux / 60).ToString() + ":" + ((aux % 60 < 10) ? "0" : "") + (aux % 60).ToString();
    }
}