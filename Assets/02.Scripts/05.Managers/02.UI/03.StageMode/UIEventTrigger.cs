using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEventTrigger : MonoBehaviour ,  IPointerDownHandler, IPointerEnterHandler , IPointerExitHandler
{

    public Action<PointerEventData> onPointerDownEventHandler;
    public Action<PointerEventData> onPointerEnterEventHandler;
    public Action<PointerEventData> onPointerExitEventHandler;

    //PointerEventData  clickCount 클릭된 횟수  clickTime 클릭된 시간 delta 변화한 값  pointerId 포인터 ID position 현재 포인터 위치


    public virtual void OnPointerDown(PointerEventData eventData)
    {
        onPointerDownEventHandler?.Invoke(eventData);
    }


    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnterEventHandler?.Invoke(eventData);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        onPointerExitEventHandler?.Invoke(eventData);
        
    }



}
