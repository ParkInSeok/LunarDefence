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

    public TowerData(TowerData data)
    {
        this.uniqueKey              = data.uniqueKey;                //À¯´ÏÅ© Å°
        this.atk                    = data.atk;                       //°ø°Ý·Â
        this.hp                     = data.hp;                          //Ã¼·Â
        this.def                    = data.def;                       //¹æ¾î·Â
        this.spdef                  = data.spdef;                     //¸¶¹æ
        this.attackSpeed            = data.attackSpeed;               //°ø¼Ó
        this.propertyReinforcePower = data.propertyReinforcePower;    //¼Ó°­
        this.propertyResistPower    = data.propertyResistPower;       //¼ÓÀú
        this.attackMotionLength     = data.attackMotionLength;
        this.flashUniqueKey         = data.flashUniqueKey;           //¹ß»ç ÀÌÆåÆ®
        this.bulletUniqueKey        = data.bulletUniqueKey;          //ÅºÈ¯ ÀÌÆåÆ®
        this.hitUniqueKey           = data.hitUniqueKey;             //¸Â­ŸÀ»¶§ ÀÌÆåÆ®
        this.propertyState          = data.propertyState;               //¼Ó¼º»óÅÂ
        this.damageType             = data.damageType;               //µ¥¹ÌÁö Å¸ÀÔ
        this.unitType               = data.unitType;                 //À¯´Ö Å¸ÀÔ

        this.critical               = data.critical;
        this.criticalDamage         =  data.criticalDamage;     
        this.lifeBloodAbsorption    =  data.lifeBloodAbsorption;
        this.skillUniqueKey         = data.skillUniqueKey;
    }

    public TowerData(int _propertyState, int _damageType, int _unitType)
    {
        propertyState = _propertyState;
        damageType = _damageType;
        unitType = _unitType;
    }

}
