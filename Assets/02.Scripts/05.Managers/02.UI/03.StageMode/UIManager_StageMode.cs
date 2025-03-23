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

        backUI.Init(backCanvasRect);




    }



   


}
