using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_ModoCertoErrado_AlinhamentoTexto : MonoBehaviour{
    float initialY;
    bool isScrolling, startScroll;
    TextMeshProUGUI text;
    public int maxLinhas;

    float initialBottomY {
        get{
            return (Camera.main.ScreenToWorldPoint(
                new Vector3(text.bounds.extents.y * 1.8f, 0, 0)
            )).x;
        }
    }

    void Awake(){
        startScroll = false;
        text = GetComponent<TextMeshProUGUI>();
    }
    void Start(){
        initialY = transform.position.y;
    }
    void Update(){
        if(startScroll && text.textInfo.lineCount > maxLinhas)
            StartCoroutine(Scrolling((Camera.main.ScreenToWorldPoint(Input.mousePosition)).y, transform.position));
        else if(GetLastScrollInput() && isScrolling)
            StartCoroutine(SoltarScrolling());
        else if(!isScrolling && initialY > transform.position.y){
            Vector3 pos = transform.position;
            pos.y = Mathf.Lerp(pos.y, initialY, 0.2f);
            transform.position = pos;
        }
        else if(!isScrolling && initialBottomY > 0f && initialBottomY < transform.position.y){
            Vector3 pos = transform.position;
            pos.y = Mathf.Lerp(pos.y, initialBottomY, 0.2f);
            transform.position = pos;
        }
    }
    IEnumerator Scrolling(float yInicialDoClick, Vector3 posicaoInicial){
        startScroll = false;
        isScrolling = true;
        Vector3 pos = posicaoInicial;
        while(GetScrollInput()){
            pos.y = posicaoInicial.y + (Camera.main.ScreenToWorldPoint(Input.mousePosition)).y - yInicialDoClick;
            transform.position = pos;
            yield return null;
        }
    }
    IEnumerator SoltarScrolling(){
        isScrolling = false;
        yield return null;
    }
    bool GetScrollInput(){
        if( Input.GetButton("Fire1") ||
           (Input.touchCount == 1 && Input.touches[0].phase != TouchPhase.Began)
        ) return true;
        return false;
    }
    bool GetFirstScrollInput(){
        if( Input.GetMouseButtonDown(0) ||
           (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began)
        ) return true;
        return false;
    }
    bool GetLastScrollInput(){
        if( Input.GetMouseButtonUp(0) ||
           (Input.touchCount == 1 && (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled))
        ) return true;
        return false;
    }
    float yInputPoistion(){
        if(Input.GetMouseButtonDown(0))
            return (Camera.main.ScreenToWorldPoint(Input.mousePosition)).y;
        else
            return Input.touches[0].position.y;
    }
    public void StartScroling(){
        startScroll = true;
    }
}