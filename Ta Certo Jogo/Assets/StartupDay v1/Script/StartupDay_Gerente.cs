using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "StartupDay.v1/GERENTE")]
public class StartupDay_Gerente : ScriptableObject{
    public event Action<bool> resposta = delegate {};

    public StartupDay_InformacoesSO info;
    public StartupDay_Atividade[] atividade;

    public void CarregarAtividade(int index){
        info.atividade = atividade[index];
        info.indexQuestao = 0;
        info.acertos = info.erros = 0;
        info.tempo = 0;

        SceneManager.LoadScene("Game");
    }
    public void Resposta(bool resp){
        if(resp)
            info.acertos++;
        else
            info.erros++;

        if(info.atividade.questoes.Length - 1 != info.indexQuestao){
            info.indexQuestao++;
            resposta(resp);
        }
        else{
            SceneManager.LoadScene("EndGame");
        }
    }
}