using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MenuDemo_openClose : MonoBehaviour{
    bool isOpen = false;
    public SO_MenusPrincipais_ChangeMenu cm;
    public void OpenClose(){
        isOpen = !isOpen;
        GetComponent<Animator>().SetBool("open", isOpen);
    }
    void Awake(){
        cm.AbrirMenuSalas += fastClose;
        cm.AbrirMenuPerfil += fastClose;
        cm.AbrirMenuDemo += resetFastClose;
    }
    void OnDisable(){
        cm.AbrirMenuSalas -= fastClose;
        cm.AbrirMenuPerfil -= fastClose;
        cm.AbrirMenuDemo -= resetFastClose;
    }
    public void fastClose(){
        GetComponent<Animator>().SetBool("fastexit", true);
        isOpen = false;
        GetComponent<Animator>().SetBool("open", false);
    }
    public void resetFastClose(){
        GetComponent<Animator>().SetBool("fastexit", false);
    }
}