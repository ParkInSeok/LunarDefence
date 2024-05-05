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
    public int gCost;
    public int hCost;

    public int row; //�డ��
    public int column; //�� ����

    public PathNode parent;

    public int FCost { get { return gCost + hCost; } }





}
