using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupDay_QuestaoBase : ScriptableObject {
    public TIPO tipo { get {return _tipo; } set{_tipo = value;} }
    public TIPO _tipo;
}