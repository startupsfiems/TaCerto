using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartupDay_EndGame : MonoBehaviour{
    public StartupDay_InformacoesSO info;
    void Start(){
        int aux = (int)info.tempo;
        GetComponent<TMP_Text>().text = "Tempo total: " + ((aux / 60 < 10) ? "0" : "") + (aux / 60).ToString() + ":" + ((aux % 60 < 10) ? "0" : "") + (aux % 60).ToString() + "\nAcertos: " + info.acertos + "\nErros: " + info.erros;
    }
}