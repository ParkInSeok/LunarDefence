using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Skill
{
    public string skillUniqueKey;                   //스킬 유니크키
    public string skillModelUniqueKey;              //스킬 모델 유니크 키음
    public GameObject skillModel;
    public string skillIconUniqueKey;               //스킬 아이콘
    public Sprite skillIcon;
    public int damageCoefficient;                 //스킬계수 

    public int skillLevel;                          //스킬 레벨 
    //스킬 매커니즘 
    public SkillType skillType;                           //스킬 타입
    public BuffStatType buffStatType;                        //버프 스탯타입

    public Skill()
    {

    }
    public Skill(string _skillUniqueKey, string _skillModelUniqueKey, string _skillIconUniqueKey, int _damageCoefficient,
                    int _skillLevel, SkillType _skillType, BuffStatType _BuffStatType)
    {
        skillUniqueKey = _skillUniqueKey;
        skillModelUniqueKey = _skillModelUniqueKey;
        skillIconUniqueKey = _skillIconUniqueKey;
        damageCoefficient = _damageCoefficient;
        skillLevel = _skillLevel;
        skillType = _skillType;
        buffStatType = _BuffStatType;

        //todo set skillmodel , set skillicon

    }

    public Skill(SkillDataBase _skillDataBase)
    {
        skillUniqueKey = _skillDataBase.skillUniqueKey;
        skillModelUniqueKey = _skillDataBase.skillModelUniqueKey;
        skillIconUniqueKey = _skillDataBase.skillIconUniqueKey;
        damageCoefficient = _skillDataBase.damageCoefficient;
        skillLevel = _skillDataBase.skillLevel;
        skillType = _skillDataBase.SkillType;
        buffStatType = _skillDataBase.BuffStatType;

        //todo set skillmodel , set skillicon

    }


}
