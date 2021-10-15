using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ModoCertoErrado_VerificarCertoErrado : MonoBehaviour{

    public IQuestao questao;
    Animator anim;

    private void Start() {
        anim = this.GetComponent<Animator>();
    }

    public void VerificarCertoErrado(bool resposta){
        QuestaoCertoErrado q = (QuestaoCertoErrado)questao;

        if(q.certo == resposta){
            anim.SetBool("Certo", true);
            anim.SetTrigger("Clicked");
        }
        else{
            anim.SetBool("Certo", false);
            anim.SetTrigger("Clicked");
        }
    }
    public void VerificarColuna(){
        QuestaoColuna q = (QuestaoColuna)questao;
    
    
    }
}