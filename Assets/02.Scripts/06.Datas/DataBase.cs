using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PropertyState
{
    none,
    fire,
    water,

}

public enum DamageType{
    physicalDamageType,
    masicDamageType,
}

public enum UnitType{
    towerData,
    enemyData,
    heroData,
}




[Serializable]
public class DataBase
{
    public string uniqueKey;                //유니크 키
    public float atk;                       //공격력
    public int hp;                          //체력
    public float def;                       //방어력
    public float spdef;                     //마방
    public float attackSpeed;               //공속
    public float propertyReinforcePower;    //속강
    public float propertyResistPower;       //속저
    public int attackMotionLength;

    [SerializeField] protected int propertyState;               //속성상태
    [SerializeField] protected int damageType;               //데미지 타입

    [SerializeField] protected int unitType;                 //유닛 타입

    public PropertyState PropertyState { get { 
            if(propertyState >= 0 && propertyState < System.Enum.GetValues(typeof (PropertyState)).Length)
            {
                return (PropertyState)propertyState;
            }
            else
            {
                return PropertyState.none;
            }
        }
    }  //

    public DamageType DamageType {
        get
        {
            if (damageType >= 0 && damageType < System.Enum.GetValues(typeof (DamageType)).Length)
                return (DamageType)damageType;
            else
            {
                return DamageType.physicalDamageType;
            }
        }
    }

    public UnitType UnitType {
        get{
            if(unitType >= 0 && unitType < System.Enum.GetValues(typeof (UnitType)).Length)
                return (UnitType)unitType;
            else
                return UnitType.towerData;
        }

    }


}
