using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunarInputManager : Singleton<LunarInputManager>
{


    LunarInputController _input = new LunarInputController();
    public LunarInputController _Input { get { return Instance._input; } }

    [SerializeField]LayerMask tileMapLayer;

    public Action<int, int> selectTileEventHandler;


    private void Update()
    {
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
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 60f, tileMapLayer))
            {
                if (!hit.collider.tag.Equals("Tile"))
                    return;
                hit.transform.TryGetComponent<TileEventTrigger>(out var tile);
                if (tile == null)
                    return;

                selectTileEventHandler?.Invoke(tile.pathNode.row, tile.pathNode.column);

               



            }
        }



    }








}
