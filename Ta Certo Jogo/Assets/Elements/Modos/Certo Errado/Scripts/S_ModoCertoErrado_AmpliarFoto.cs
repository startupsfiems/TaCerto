using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_ModoCertoErrado_AmpliarFoto : MonoBehaviour, IPointerDownHandler{
    
    Animator anim;
    public AudioManager audioManager;
    bool open;

    void Start(){
        anim = GetComponentInParent<Animator>();
    }
    
    public void OnPointerDown(PointerEventData pointerEventData){
        open = !open;
        anim.SetBool("Open", open);
        if(open)
            audioManager.PlaySound("ImagemAbrindo");
        else
            audioManager.PlaySound("ImagemAbrindo");
    }
}
