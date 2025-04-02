using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_StageMode : UIManager
{

    public List<CanvasScaler> canvasScalers = new List<CanvasScaler>();

    RectTransform backCanvasRect;

    [SerializeField] StageBackUI backUI;
    [SerializeField] CommonSelectUI commonSelectUI;
    [SerializeField] UIEventTrigger fakeUI;

   // [SerializeField] CommonSelectUIType selectuitype;



    void Start()
    {

        BindEvents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected override void Init()
    {

        backCanvasRect = canvasScalers[(int)CanvasLayerType.backType].GetComponent<RectTransform>();

      
    }



    protected override void BindEvents()
    {

        //backUI.Init(backCanvasRect);

        StageManager.Instance.PathController.selectPathNodeEventHandler += BindOpenCommonSelectUIEvent;
        fakeUI.onPointerDownEventHandler = (x) =>
        {
            UtilityManager.Instance.DelayFunction_EndOfFrame(() =>
            {
                fakeUI.gameObject.SetActive(false);
                commonSelectUI.HideCommonSelectUI();

                LunarInputManager.Instance.isStopInput = false;
            });
        };

    }

    private void BindOpenCommonSelectUIEvent(PathNode obj)
    {
        if (commonSelectUI.gameObject.activeSelf == false)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(obj.position);

            var vaildUI_max = screenPosition.x + commonSelectUI.rectTransform.sizeDelta.x;

            DirectionType dirType = vaildUI_max > Screen.currentResolution.width ? DirectionType.left : DirectionType.right;

            //var targetuitype = ((int)selectuitype) + 1;

            //if (targetuitype >= System.Enum.GetValues(typeof(CommonSelectUIType)).Length)
            //    targetuitype = 0;
            //selectuitype = (CommonSelectUIType)targetuitype;
            commonSelectUI.ShowCommonSelectUI(screenPosition,  CommonSelectUIType.two, dirType,(x)=> 
            {
                Debug.Log("first Btn Click");
            }, (x)=>
            {
                Debug.Log("second Btn Click");
            });

            fakeUI.gameObject.SetActive(true);
         
            fakeUI.rectTransform.SetAsLastSibling();
            commonSelectUI.rectTransform.SetAsLastSibling();

            LunarInputManager.Instance.isStopInput = true;
        }

    }
}
