using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class HeroData : TowerDataBase
{
    public string skillUniqueKeys;
    public List<string> skillUniqueKeyList = new List<string>();

    public HeroData()
    {

    }
    public HeroData(int _propertyState, int _damageType, int _unitType)
    {
        propertyState = _propertyState;
        damageType = _damageType;
        unitType = _unitType;
    }


    public void SetSkills(string _skillUniqueKeys)
    {
        string[] skills = _skillUniqueKeys.Split(',');

        for (int i = 0; i < skills.Length; i++)
        {
            if(skillUniqueKeyList.Contains(skills[i]) == false)
            {
                skillUniqueKeyList.Add(skills[i]);
            }
        }

    }




}
