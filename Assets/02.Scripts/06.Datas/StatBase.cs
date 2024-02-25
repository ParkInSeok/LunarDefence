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



[Serializable]
public class StatBase
{
    public string uniqueKey;                //유니크 키
    public float atk;                       //공격력
    public int hp;                        //체력
    public float sp;                        //마력
    public float def;                       //방어력
    public float spdef;                     //마법방어력
    public float attackSpeed;               //공속
    public float propertyReinforcePower;    //속성강화
    public float propertyResistPower;       //속성저항
    protected int propertyState;               //속성상태

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
    }  //속성상태




}
