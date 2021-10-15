using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
// IPointerDownHandler, IPointerClickHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler,
public class StartupDay_DragBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

    public StartupDay_InformacoesSO info;
    public StartupDay_TextoInterativoInterface text;
    public int index;
    Vector3 myPosition;
    public TMP_Text mytext;

    bool ativo;

    public void Start(){
        myPosition = transform.position;
    }
    public void Update(){
        if(info.atividade.questoes[info.indexQuestao].tipo == TIPO.LACUNA && ((StartupDay_QuestaoLacuna)info.atividade.questoes[info.indexQuestao]).bloco.Length > index){
            ativo = true;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
            mytext.text = ((StartupDay_QuestaoLacuna)info.atividade.questoes[info.indexQuestao]).bloco[index];
        }
        else{
            ativo = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void OnBeginDrag(PointerEventData eventData){
        if(ativo)
            info.indexOpcaoArrastada = index;
    }
    public void OnDrag(PointerEventData eventData){
        if(ativo)
            transform.position = GetPosDragg();
    }
    public void OnEndDrag(PointerEventData eventData){
        if(ativo){
            text.EndDrag();
            StartCoroutine(LerpBack());
        }
    }

    Vector3 GetPosDragg(){
        Vector3 pos;
        if(Input.touchCount > 0)
            pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        else
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        pos.z = transform.position.z;
        return pos;
    }

    IEnumerator LerpBack(){
        for(int i = 0; i < 30; i++){
            transform.position = Vector3.Lerp(transform.position, myPosition, .3f);
            yield return null;
        }
        transform.position = Vector3.Lerp(transform.position, myPosition, 1f);
        yield break;
    }
}