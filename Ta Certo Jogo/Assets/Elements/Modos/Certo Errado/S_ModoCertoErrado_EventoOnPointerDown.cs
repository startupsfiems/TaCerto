using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class S_ModoCertoErrado_EventoOnPointerDown : MonoBehaviour, IPointerDownHandler{
    public GameObject texto;
    public void OnPointerDown(PointerEventData eventData){
        texto.GetComponent<S_ModoCertoErrado_AlinhamentoTexto>().StartScroling();
    }
}