using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartupDay_TextoInterativo : MonoBehaviour{
    public StartupDay_Gerente gerente;
    public StartupDay_InformacoesSO info;
    public StartupDay_TextoInterativoInterface textInterface;
    TMP_Text text;

    public int[] indexBlocosQuestaoUsados = new int[4];

    public void OnEnable(){textInterface.endDrag += EndDrag;}
    public void OnDisable(){textInterface.endDrag -= EndDrag;}

    void Start(){
        text = GetComponent<TMP_Text>();
        for(int i = 0; i < 4; i++) indexBlocosQuestaoUsados[i] = -1;
    }

    void Update(){UpdateText();}

    public void EndDrag(){
        if(Input.GetMouseButtonUp(0)){
            int index = TMP_TextUtilities.FindIntersectingCharacter(text, Input.mousePosition, Camera.main, true);
            int indexPalavra = TMP_TextUtilities.FindIntersectingWord(text, Input.mousePosition, Camera.main);
            
            if(index >= 0){
                bool flag = false;
                if(StringAux.CreckSprite(text.text, index) == 1){
                    flag = true;
                    for(int i = 0; i < 4; i++) indexBlocosQuestaoUsados[i] = info.indexOpcaoArrastada;
                }
                else if(StringAux.CreckPalavra(text.text, indexPalavra)){
                    flag = true;
                    for(int i = 0; i < 4; i++) indexBlocosQuestaoUsados[i] = info.indexOpcaoArrastada;
                }
                if(flag){
                    bool resp = false;
                    
                    if(((StartupDay_QuestaoLacuna)info.atividade.questoes[info.indexQuestao]).blocoLugar[indexBlocosQuestaoUsados[0]] == 1)
                        resp = true;
                    for(int i = 0; i < 4; i++) indexBlocosQuestaoUsados[i] = -1;
                    gerente.Resposta(resp);
                }
            }
        }
    }
    
    public void UpdateText(){
        if(info.atividade.questoes[info.indexQuestao].tipo == TIPO.LACUNA){
            StartupDay_QuestaoLacuna questao = (StartupDay_QuestaoLacuna)info.atividade.questoes[info.indexQuestao];
            string newText = "";
            for(int i = 0; i < questao.textos.Length; i++){
                if(questao.textosStatus[i] && indexBlocosQuestaoUsados[i]!=-1)
                    newText += "<b>" + questao.bloco[indexBlocosQuestaoUsados[i]] + "</b>";
                else newText += questao.textos[i];
            }
            text.text = newText;
        }
    }
}
/*
teste <size=150%><pos=-6.5%><sprite=0 color=#005500></pos></size> aeee tem coisa aqui <size=150%><sprite=0></size>
*/