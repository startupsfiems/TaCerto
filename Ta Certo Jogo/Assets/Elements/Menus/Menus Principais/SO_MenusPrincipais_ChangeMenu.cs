using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Menus Principais/SO/ChangeMenu")]
public class SO_MenusPrincipais_ChangeMenu : ScriptableObject{
    public event Action AbrirMenuSalas = delegate {};
    public event Action AbrirMenuPerfil = delegate {};
    public event Action AbrirMenuDemo = delegate {};

    public void abrirMenuSalas(){ AbrirMenuSalas(); }
    public void abrirMenuPerfil(){ AbrirMenuPerfil(); }
    public void abrirMenuDemo(){ AbrirMenuDemo(); }
}