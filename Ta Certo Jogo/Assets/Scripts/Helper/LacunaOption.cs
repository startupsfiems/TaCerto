using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LacunaOption : MonoBehaviour , IDragHandler, IBeginDragHandler , IEndDragHandler
{
    public TextMeshProUGUI lacunaText;

    private Vector2 lastMousePosition;
    private Vector3 myDefaultTransformPosition;

    private GameObject hudLacuna;
    private Transform hudLacunaTransform;
    private Transform myRealParent;
    private bool hasToReturnToParent = false;

    private void Start()
    {
        hudLacuna = GameObject.FindWithTag("HudLacuna");
        hudLacunaTransform = hudLacuna.transform;
        myRealParent = transform.parent;
        myDefaultTransformPosition = transform.position;
    }

    public void setLacunaText(string text)
    {
        lacunaText.text = text;
    }

    /// <summary>
    /// This method will be called on the start of the mouse drag
    /// </summary>
    /// <param name="eventData">mouse pointer event data</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        hasToReturnToParent = true;
  
        transform.SetParent(hudLacunaTransform, false);
        lastMousePosition = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    /// <summary>
    /// This method will be called during the mouse drag
    /// </summary>
    /// <param name="eventData">mouse pointer event data</param>
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GetPosDrag();
    }

    Vector3 GetPosDrag()
    {
        Vector3 pos;
        if (Input.touchCount > 0)
            pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        else
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        pos.z = transform.position.z;
        return pos;
    }

    /// <summary>
    /// This method will be called at the end of mouse drag
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        foreach (GameObject go in eventData.hovered)
        {
            if(go.tag == "LacunaContentText")
            {
                hasToReturnToParent = eventData.pointerCurrentRaycast.gameObject.GetComponent<LacunaText>().TextEndDrag(eventData, lacunaText.text, gameObject);
            }
        }

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (hasToReturnToParent)
            VoltaParaOPai();
    }

    public void VoltaParaOPai()
    {
        StartCoroutine(LerpBack());
    }

    IEnumerator LerpBack()
    {
        for (int i = 0; i < 30; i++)
        {
            transform.position = Vector3.Lerp(transform.position, myDefaultTransformPosition, .3f);
            yield return null;
        }
        transform.position = Vector3.Lerp(transform.position, myDefaultTransformPosition, 1f);
        transform.SetParent(myRealParent, false);

        yield break;
    }

    /// <summary>
    /// This methods will check is the rect transform is inside the screen or not
    /// </summary>
    /// <param name="rectTransform">Rect Trasform</param>
    /// <returns></returns>
    private bool IsRectTransformInsideSreen(RectTransform rectTransform)
    {
        bool isInside = false;
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        int visibleCorners = 0;
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        foreach (Vector3 corner in corners)
        {
            if (rect.Contains(corner))
            {
                visibleCorners++;
            }
        }
        if (visibleCorners == 4)
        {
            isInside = true;
        }
        return isInside;
    }
}
