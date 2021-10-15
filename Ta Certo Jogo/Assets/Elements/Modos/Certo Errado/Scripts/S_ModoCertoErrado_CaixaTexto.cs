using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class S_ModoCertoErrado_CaixaTexto : MonoBehaviour{
    
    public bool comFoto;
    public int characterCount;
    public int topVertical;
    public TextMeshProUGUI textContent;
    public RectTransform textRect;
    VerticalLayoutGroup verticalLayout;

    public int maxCharacter;
    public int maxHeight;

    void Awake() {
        verticalLayout = GetComponent<VerticalLayoutGroup>();
    }

    void FixedUpdate() {
        characterCount = textContent.textInfo.characterCount;
        if(characterCount <= maxCharacter && comFoto)
            textRect.localPosition = new Vector2(textRect.localPosition.x, maxHeight);
        else if(textRect.sizeDelta.y <= 530f && !comFoto)
            textRect.localPosition = new Vector2(textRect.localPosition.x, maxHeight);
            

        if(characterCount > maxCharacter && comFoto)
            topVertical = maxCharacter;
        
        verticalLayout.padding.top = topVertical;
    }
}