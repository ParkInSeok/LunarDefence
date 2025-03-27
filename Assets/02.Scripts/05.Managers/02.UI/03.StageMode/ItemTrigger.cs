using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemTrigger : UIEventTrigger
{
    [SerializeField] Image thumbnail; // Ŭ�� ���� �巡�� �ؼ� ���ͷ��� ����

    Coroutine interactionEvent;

    public Image ThumbnailImage { get { return thumbnail; } }

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
    }






}
