using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ModoCertoErrado_HabilitarBotoes : MonoBehaviour{
    
    public GameObject gameObj;

    public void HabilitaBotoes(){
        gameObj.SetActive(true);
    }

    public void DesabilitaBotoes(){
        gameObj.GetComponent<Animator>().SetTrigger("Sair");
    }
}