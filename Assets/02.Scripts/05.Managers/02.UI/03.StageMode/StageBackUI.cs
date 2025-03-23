using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class StageBackUI : MonoBehaviour
{

    [Header("Info UIs")]
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] Button createWallButton;
    [SerializeField] Button createTowerButton;

    [Header("item UIs")]
    [SerializeField] List<ItemTrigger> itemButtons = new List<ItemTrigger>();


    [SerializeField] Button startRoundButton;


    Coroutine towerInteractionEvent;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BindCancelTowerInteractionEvent();
    }

    public void Init(RectTransform backCanvas)
    {
        for (int i = 0; i < itemButtons.Count; i++)
        {
            int index = i;
            itemButtons[index].onPointerDownEventHandler += (eventdata)=>
                BindTowerIntercationEvent(eventdata,backCanvas, itemButtons[index].ThumbnailImage);
        }




    }

    private void BindCancelTowerInteractionEvent()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (towerInteractionEvent != null)
            {
                StopCoroutine(towerInteractionEvent);
            }
        }

    }

    private void BindTowerIntercationEvent(PointerEventData obj, RectTransform backCanvas, Image image)
    {
        if (towerInteractionEvent != null)
        {
            StopCoroutine(towerInteractionEvent);
        }
        towerInteractionEvent = StartCoroutine(_TowerInteractionEvent(backCanvas, image));
    }

    IEnumerator _TowerInteractionEvent(RectTransform backCanvas, Image image)
    {
        Vector2 mousePosition;
        Vector2 originPos = image.rectTransform.anchoredPosition;
        while (true)
        {
            yield return null;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(backCanvas, Input.mousePosition, null, out mousePosition);
            originPos = image.rectTransform.anchoredPosition;

            image.rectTransform.anchoredPosition = Vector2.Lerp(originPos, mousePosition, Time.deltaTime);

            Debug.Log("origine : " + originPos + " mousePos : " + mousePosition);
        }
    }
    //느낌 너무없어서 타일 클릭해서 유아이 나오는 방식으로 해야할듯

}
