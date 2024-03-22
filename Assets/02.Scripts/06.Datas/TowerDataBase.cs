using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TowerDataBase : DataBase
{
    public int critical;            //크확
    public int criticalDamage;      //크증뎀
    public int lifeBloodAbsorption; // 흡혈
   
    public TowerDataBase(){
        
    }


    public TowerDataBase(int _propertyState, int _damageType, int _unitType)
    {
        propertyState = _propertyState;
        damageType = _damageType;
        unitType = _unitType;
    }

}
