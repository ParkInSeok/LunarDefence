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
public class EnemyStat : StatBase
{
    public float moveSpeed;             //이동속도

    public EnemyType enemyType;

}
