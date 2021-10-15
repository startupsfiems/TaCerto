using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_UIGame_OpenCards : MonoBehaviour{

    bool open;
    public Animator anim;

    private void Start() {
    }

    public void OpenCloseCards(){
        open = !open;
        anim.SetBool("Open", open);
    }
}
