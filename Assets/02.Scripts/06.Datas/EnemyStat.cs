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
    public float moveSpeed;             //�̵��ӵ�

    public EnemyType enemyType;

}
