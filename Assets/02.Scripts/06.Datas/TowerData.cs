using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[Serializable]
public class TowerData : BaseTowerData
{
    public string skillUniqueKey;


    public TowerData()
    {

    }
    public TowerData(int _propertyState, int _damageType, int _unitType)
    {
        propertyState = _propertyState;
        damageType = _damageType;
        unitType = _unitType;
    }

}
