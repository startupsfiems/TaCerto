using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ModoCertoErrado_TrocarLayer : MonoBehaviour{

    public Canvas canvas;

    void Awake(){
        canvas = GetComponent<Canvas>();
        if(gameObject.GetComponent<Canvas>() == null){
            canvas = gameObject.transform.parent.gameObject.GetComponent<Canvas>();
        }
    }

    public void TrocarOrderLayer(int i){
        canvas.sortingOrder = i;
    }
}
