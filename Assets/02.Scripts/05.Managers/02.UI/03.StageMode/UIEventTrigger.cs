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

    //PointerEventData  clickCount Ŭ���� Ƚ��  clickTime Ŭ���� �ð� delta ��ȭ�� ��  pointerId ������ ID position ���� ������ ��ġ


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
