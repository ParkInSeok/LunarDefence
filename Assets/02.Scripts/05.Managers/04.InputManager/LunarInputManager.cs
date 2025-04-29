using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunarInputManager : Singleton<LunarInputManager>
{


    //LunarInputController _input = new LunarInputController();
    //public LunarInputController _Input { get { return Instance._input; } }


    public Action<Vector2> mouseDownEventHandler;
    public Action<Vector2> mouseUpEventHandler;
    public Action<Vector2> mouseEventHandler;

    //public bool isStopInput = false;

    Vector3 preMousePos;


    private void Update()
    {
        //if (isStopInput)
        //    return;

        //_input.OnUpdate();

        BindMouseDownEvnet();
    }

    protected override void Init()
    {

        BindEvents();
    }

    void BindEvents()
    {
        //_Input.keyaction += BindMouseDownEvnet;
    }


    private void OnDestroy()
    {
        //_Input.keyaction -= BindMouseDownEvnet;
    }

    private void BindMouseDownEvnet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownEventHandler?.Invoke(Input.mousePosition);

        }
        else if (Input.GetMouseButton(0))
        {
            if (preMousePos == Input.mousePosition)
                return;
            mouseEventHandler?.Invoke(Input.mousePosition);
            preMousePos = Input.mousePosition;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseUpEventHandler?.Invoke(Input.mousePosition);
        }



    }





}
