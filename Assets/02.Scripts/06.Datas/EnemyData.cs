using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EnemyType
{
    normal,
    boss,
    raid,
}

[Serializable]
public class EnemyData : DataBase
{
    public float moveSpeed;             //이동속도

    [SerializeField] protected int enemyType;         //타입
    public EnemyData()
    {
        
    }
    public EnemyData(EnemyData data)
    {
        this.uniqueKey = data.uniqueKey;
        this.atk = data.atk;
        this.hp = data.hp;
        this.def = data.def;
        this.spdef = data.spdef;
        this.attackSpeed = data.attackSpeed;
        this.propertyReinforcePower = data.propertyReinforcePower;
        this.propertyResistPower = data.propertyResistPower;
        this.propertyState = data.propertyState;
        this.damageType = data.damageType;
        this.unitType = data.unitType;
        this.enemyType = data.enemyType;
        this.moveSpeed = data.moveSpeed;
        this.attackMotionLength = data.attackMotionLength;
    }


    public EnemyData(int _propertyState, int _damageType, int _unitType, int _enemyType)
    {
        propertyState = _propertyState;
        damageType = _damageType;
        unitType = _unitType;
        enemyType = _enemyType;
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
