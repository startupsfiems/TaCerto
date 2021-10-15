using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MenuPrincipais_ShapeAnimation : MonoBehaviour{
    public void playAnimation(int index){
        GetComponent<Animator>().SetInteger("indexAnim", index);
    }
}