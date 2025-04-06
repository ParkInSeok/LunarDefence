using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileWallState
{
    empty,
    wall,
}

public enum TileUnitState
{
    empty,
    tower,
    hero,
}

public enum TileBuffState
{
    empty,
    atk,
    speed,
    def,
}



[System.Serializable]
public class PathNode
{
    public Vector3 position;
    public GameObject ground;
    public Material material;
    public TileWallState walkable { get; private set; }
    public Action<TileWallState> onvalueChangedWakableEventHandler;
    public TileUnitState unitState { get; private set; }
    public Action<TileUnitState> onvalueChangedUnitStateEventHandler;
    public TileBuffState buffState { get; private set; }
    public Action<TileBuffState> onvalueChangedBuffStateEventHandler;
    public int gCost;
    public int hCost;

    public int row { get; private set; }//행가로
    public int column { get; private set; } //열 세로

    public PathNode parent;

    public int FCost { get { return gCost + hCost; } }

    //삭제예정 -> objectpooling 에서 get 해오는 방식으로 처리 [row,column] index
    public Color origineColor;


    public PathNode(Vector3 worldPoint, TileWallState walkable, GameObject tile, Material mat, int x, int y)
    {
        position = worldPoint;
        this.walkable = walkable;
        ground = tile;
        material = mat;
        row = x;
        column = y;
        unitState = TileUnitState.empty;
        buffState = TileBuffState.empty;
    }

    public void ChangeBuff(TileBuffState state)
    {
        buffState = state;
        onvalueChangedBuffStateEventHandler?.Invoke(state);
    }
    public void ChangeWalkable(TileWallState state)
    {
        walkable = state;
        onvalueChangedWakableEventHandler?.Invoke(state);
    }


    public void SetUnit(TileUnitState state)
    {
        // unit = _unit;
        var unit = StageManager.Instance.ObjectPoolingController.GetTargetTower(row, column).unitDieEventHandler += DieUnit;
        unitState = state;
        onvalueChangedUnitStateEventHandler?.Invoke(unitState);
        //unit.unitDieEventHandler += DieUnit;

    }

    public void DieUnit()
    {
        //unit.unitDieEventHandler -= DieUnit;
        //unit = null;
        unitState = TileUnitState.empty;
        onvalueChangedUnitStateEventHandler?.Invoke(unitState);
    }

}
