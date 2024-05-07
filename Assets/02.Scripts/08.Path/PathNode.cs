using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathNode
{
    public Vector3 position;
    public GameObject ground;
    public Material material;
    public bool walkable;
    public bool isTower;
    public int gCost;
    public int hCost;

    public int row; //행가로
    public int column; //열 세로

    public PathNode parent;

    public int FCost { get { return gCost + hCost; } }

    private UnitBase unit;

    public UnitBase Unit { get { return unit; } }

    public Color origineColor;



    public void SetUnit(UnitBase _unit)
    {
        unit = _unit;
        isTower = true;

        unit.unitDieEventHandler += DieUnit;

    }

    public void DieUnit()
    {
        unit.unitDieEventHandler -= DieUnit;
        unit = null;
        isTower = false;
    }

}
