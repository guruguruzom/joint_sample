using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseMouseInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    protected int keyNum;

    private bool isDrag = false;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(keyNum))
        {
            isDrag = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDrag)
            return;
        // Do something when dragging...
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        // Do something when drag ends...
    }
}
