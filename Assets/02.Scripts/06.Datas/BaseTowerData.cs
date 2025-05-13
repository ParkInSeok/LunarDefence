using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StarState
{
    star_one,
    star_two,
    star_three,
    star_four,
    star_five,
    star_six,
    star_seven,
    max,
}

[Serializable]
public class BaseTowerData : BaseData
{
    public int critical;            //크확
    public int criticalDamage;      //크증뎀
    public int lifeBloodAbsorption; // 흡혈
    
    public StarState Star { get; private set; }

    public Action<StarState> onchangedStarStateEventHandler;

    public BaseTowerData(){
        
    }


    public BaseTowerData(int _propertyState, int _damageType, int _unitType)
    {
        propertyState = _propertyState;
        damageType = _damageType;
        unitType = _unitType;
    }

    public void ChangeStar(StarState _star)
    {
        Star = _star;
        onchangedStarStateEventHandler?.Invoke(Star);
    }

    public void LevelUpStar()
    {
        Star++;
    }


}
