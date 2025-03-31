using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunarInputManager : Singleton<LunarInputManager>
{


    LunarInputController _input = new LunarInputController();
    public LunarInputController _Input { get { return Instance._input; } }

    [SerializeField]LayerMask tileMapLayer;

    public Action<Vector2> mouseDownEventHandler;

    public bool isStopInput = false;


    private void Update()
    {
        if (isStopInput)
            return;

        _input.OnUpdate();
    }

    protected override void Init()
    {

        BindEvents();
    }

    void BindEvents()
    {
        _Input.keyaction += BindMouseDownEvnet;
    }


    private void OnDestroy()
    {
        _Input.keyaction -= BindMouseDownEvnet;
    }

    private void BindMouseDownEvnet()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mouseDownEventHandler?.Invoke(Input.mousePosition);

        }



    }

    void OnGUI()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 90, Color.red);
        }

    }

    



}
