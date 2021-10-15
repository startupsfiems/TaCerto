using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_UIGame_ButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, S_UIGame_IButton{
    public Vector3 btnScale;
    public Animator BtnAnim;
    public float num;

    public event Action OnClicked = delegate{};

    void Start(){
            // BtnAnim = GetComponent<Animator>();
    }

    public void OnPointerDown(PointerEventData eventData){
        if(BtnAnim != null){
            BtnAnim.enabled = false;
        }
        if(GetComponent<Button>().interactable == true){
            this.gameObject.transform.localScale = new Vector3(btnScale.x * num, btnScale.y * num, btnScale.z);
        }
        if(GetComponent<Button>().interactable == false){
            if(BtnAnim == null){
                return;
            }
            BtnAnim.SetTrigger("Click");
        }
    }

    public void OnPointerUp(PointerEventData eventData){
        if(BtnAnim != null){
            BtnAnim.enabled = true;
        }
        if( GetComponent<Button>().interactable == true){
            this.gameObject.transform.localScale = btnScale;
        }
        OnClicked();
    }
}