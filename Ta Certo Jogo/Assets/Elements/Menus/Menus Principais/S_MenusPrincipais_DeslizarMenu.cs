using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MenusPrincipais_DeslizarMenu : MonoBehaviour{
    float valorDeslize, intensidadeLerp;
    public SO_MenusPrincipais_ChangeMenu cm;
    void Awake(){
        valorDeslize = 0f;
        intensidadeLerp = 10f;
        cm.AbrirMenuSalas += menuSalas;
        cm.AbrirMenuPerfil += menuPerfil;
        cm.AbrirMenuDemo += menuDemo;
    }
    void OnDisable(){
        cm.AbrirMenuSalas -= menuSalas;
        cm.AbrirMenuPerfil -= menuPerfil;
        cm.AbrirMenuDemo -= menuDemo;
    }
    void Update(){
        Vector2 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, valorDeslize, intensidadeLerp * Time.deltaTime);
        transform.position = pos;
    }
    void menuSalas(){ valorDeslize = converterIndexParaPosicaoGlobal(-1); }
    void menuPerfil(){ valorDeslize = converterIndexParaPosicaoGlobal(0); }
    void menuDemo(){ valorDeslize = converterIndexParaPosicaoGlobal(1); }
    float converterIndexParaPosicaoGlobal(int indexDaTela){
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * (0.5f - indexDaTela), 0f, 0f)).x;
    }
}