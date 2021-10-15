using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modos/ManagerDemo")]
public class SO_Modos_MateriasDemo : ScriptableObject{
    
    [Header("Materia")]
    public bool portugues;
    public bool geografia;
    public bool historia;
    public bool ciencias;
    public bool artes;
    public bool geral;
}