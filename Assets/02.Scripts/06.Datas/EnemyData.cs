using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EnemyType
{
    normal,
    boss,
}

[Serializable]
public class EnemyData : DataBase
{
    public float moveSpeed;             //이동속도

    public EnemyType enemyType;         //타입

   



}
