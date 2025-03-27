using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_StageMode : Singleton<UIManager_StageMode>
{

    public List<CanvasScaler> canvasScalers = new List<CanvasScaler>();

    RectTransform backCanvasRect;

    [SerializeField] StageBackUI backUI;
    [SerializeField] CommonSelectUI commonSelectUI;
    [SerializeField] UIEventTrigger fakeUI;





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



    protected void BindEvents()
    {

        //backUI.Init(backCanvasRect);

        LunarInputManager.Instance.selectTileEventHandler += BindOpenCommonSelectUIEvent;
        fakeUI.onPointerDownEventHandler = (x) =>
        {
            UtilityManager.Instance.DelayFunction_EndOfFrame(() =>
            {
                fakeUI.gameObject.SetActive(false);
                commonSelectUI.gameObject.SetActive(false);

                LunarInputManager.Instance.isStopInput = false;
            });
        };

    }

    private void BindOpenCommonSelectUIEvent(PathNode obj)
    {
        if (commonSelectUI.gameObject.activeSelf == false)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(obj.position);

            commonSelectUI.rectTransform.position = screenPosition;
            commonSelectUI.gameObject.SetActive(true);
            fakeUI.gameObject.SetActive(true);
         
            fakeUI.rectTransform.SetAsLastSibling();
            commonSelectUI.rectTransform.SetAsLastSibling();

            LunarInputManager.Instance.isStopInput = true;
        }

    }
}
