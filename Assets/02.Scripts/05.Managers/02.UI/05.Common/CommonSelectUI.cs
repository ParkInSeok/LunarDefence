using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CommonSelectUIType
{
    one,
    two,
    /// <summary>
    /// index 용 enum ui CommonSelectUI 함수에 사용 하지말것
    /// </summary>
    two_two,  
    three,

}

public enum DirectionType
{
    left,
    right,
    top,
    bottom,
}

public class CommonSelectUI : MonoBehaviour
{
    public RectTransform rectTransform;

    [Header("Type_Right")]
    [SerializeField] RectTransform parent_right;
    [SerializeField] RectTransform right_type_one;
    [SerializeField] RectTransform right_type_two;
    [SerializeField] RectTransform right_type_three;

    [Header("Type_Left")]
    [SerializeField] RectTransform parent_left;
    [SerializeField] RectTransform left_type_one;
    [SerializeField] RectTransform left_type_two;
    [SerializeField] RectTransform left_type_three;

    [SerializeField] List<UIEventTrigger> uIEventTriggers;
    [SerializeField] List<TextMeshProUGUI> texts; 

    void Start()
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowCommonSelectUI(Vector3 pos, CommonSelectUIType type, DirectionType dirType, Action<PointerEventData> action_one = null,
        Action<PointerEventData> action_two = null, Action<PointerEventData> action_three = null, string firstText = "",
        string secondText = "", string thirdText = "")
    {
        rectTransform.position = pos;
        gameObject.SetActive(true);
        SetCommonSelectUIType(type, dirType, action_one, action_two, action_three,firstText,secondText,thirdText);


    }

    public void HideCommonSelectUI()
    {
        gameObject.SetActive(false);
    }

    void SetCommonSelectUIType(CommonSelectUIType type, DirectionType dirType, Action<PointerEventData> action_one = null,
        Action<PointerEventData> action_two = null, Action<PointerEventData> action_three = null,string firstText = "", 
        string secondText = "", string thirdText = "")
    {
        bool isLeftMode = dirType == DirectionType.left;
        parent_left.gameObject.SetActive(isLeftMode);
        parent_right.gameObject.SetActive(!isLeftMode);

        int triggerFirstIndex_type = isLeftMode ? (int)CommonSelectUIType.one + 6 : (int)CommonSelectUIType.one;
        int triggerTargetIndex = triggerFirstIndex_type + (int)type;

        right_type_one.gameObject.SetActive(false);
        left_type_one.gameObject.SetActive(false);
        right_type_two.gameObject.SetActive(false);
        left_type_two.gameObject.SetActive(false);
        right_type_three.gameObject.SetActive(false);
        left_type_three.gameObject.SetActive(false);
        switch (type)
        {

            case CommonSelectUIType.one:
                right_type_one.gameObject.SetActive(!isLeftMode);
                left_type_one.gameObject.SetActive(isLeftMode);
                uIEventTriggers[triggerTargetIndex].onPointerDownEventHandler = action_one;
                if(string.IsNullOrEmpty(firstText) == false)
                    texts[triggerTargetIndex].text = firstText;
                break;
            case CommonSelectUIType.two:
                right_type_two.gameObject.SetActive(!isLeftMode);
                left_type_two.gameObject.SetActive(isLeftMode);
                uIEventTriggers[triggerTargetIndex].onPointerDownEventHandler = action_one;
                uIEventTriggers[triggerTargetIndex + 1].onPointerDownEventHandler = action_two;
                if (string.IsNullOrEmpty(firstText) == false)
                    texts[triggerTargetIndex].text = firstText;
                if (string.IsNullOrEmpty(secondText) == false)
                    texts[triggerTargetIndex + 1].text = secondText;
                break;
            case CommonSelectUIType.three:
                right_type_three.gameObject.SetActive(!isLeftMode);
                left_type_three.gameObject.SetActive(isLeftMode);
                uIEventTriggers[triggerTargetIndex].onPointerDownEventHandler = action_one;
                uIEventTriggers[triggerTargetIndex + 1].onPointerDownEventHandler = action_two;
                uIEventTriggers[triggerTargetIndex + 2].onPointerDownEventHandler = action_three;
                if (string.IsNullOrEmpty(firstText) == false)
                    texts[triggerTargetIndex].text = firstText;
                if (string.IsNullOrEmpty(secondText) == false)
                    texts[triggerTargetIndex + 1].text = secondText;
                if (string.IsNullOrEmpty(thirdText) == false)
                    texts[triggerTargetIndex + 2].text = thirdText;

                break;
        }
    }




}
