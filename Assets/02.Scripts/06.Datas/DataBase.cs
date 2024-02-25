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



[Serializable]
public class DataBase
{
    public string uniqueKey;                //유니크 키
    public string modelUniqueKey;           //모델 유니크 키
    public float atk;                       //공격력
    public int hp;                          //체력
    public float def;                       //방어력
    public float spdef;                     //마방
    public float attackSpeed;               //공속
    public float propertyReinforcePower;    //속강
    public float propertyResistPower;       //속저
    protected int propertyState;               //속성상태
    protected int damageType;               //데미지 타입

    public PropertyState PropertyState { get { 
            if(propertyState >= 0)
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
            if (damageType >= 0)
                return (DamageType)damageType;
            else
            {
                return DamageType.physicalDamageType;
            }
        }
    }



}
