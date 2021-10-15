using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupDay_AnimacaoAcertoErro : MonoBehaviour{
    public StartupDay_Gerente gerente;
    public void OnEnable(){ gerente.resposta += Resposta; }
    public void OnDisable(){ gerente.resposta -= Resposta; }

    public void animacaoAcerto(){
        GetComponent<Animator>().SetInteger("acerto", 1);
    }
    public void animacaoErro(){
        GetComponent<Animator>().SetInteger("acerto", 2);
    }
    public void resetarAnimacao(){
        GetComponent<Animator>().SetInteger("acerto", 0);
    }

    public void Resposta(bool resp){
        if(resp)
            animacaoAcerto();
        else
            animacaoErro();
    }
}