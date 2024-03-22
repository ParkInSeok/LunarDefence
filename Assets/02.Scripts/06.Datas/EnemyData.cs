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

    [SerializeField] protected int enemyType;         //타입
    public EnemyData()
    {
        
    }

    public EnemyData(int _propertyState, int _damageType, int _unitType)
    {
        propertyState = _propertyState;
        damageType = _damageType;
        unitType = _unitType;
    }

    public EnemyType EnemyType
    {
        get
        {
            if (enemyType >= 0 && enemyType < System.Enum.GetValues(typeof(EnemyType)).Length)
                return (EnemyType)enemyType;
            else
                return EnemyType.normal;
        }
    }



}
