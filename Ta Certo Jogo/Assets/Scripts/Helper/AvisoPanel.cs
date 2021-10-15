using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AvisoPanel : MonoBehaviour
{

    public TextMeshProUGUI textAviso;
    
    public void SetTextoAviso(string mensagem)
    {
        textAviso.SetText(mensagem);
    }
}
