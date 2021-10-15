using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StartupDay.v1/Texto Interativo Interface")]
public class StartupDay_TextoInterativoInterface : ScriptableObject
{
    public event Action endDrag = delegate {};
    public void EndDrag(){
        endDrag();
    }
}
