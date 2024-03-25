using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public enum SkillType
{
    normalDamage,
    doteDamage,
    areaBuff,                   //디버프는 -로 세팅 
    selfBuff,
    controllConfusion,

}

public enum BuffStatType
{
    none,
    atk,                       //공격력
    def,                      //방어력
    spdef,                     //마방
    attackSpeed,               //공속
    propertyReinforcePower,    //속강
    propertyResistPower,       //속저

}



[Serializable]
public class SkillDataBase
{
    public string skillUniqueKey;                   //스킬 유니크키
    public string skillModelUniqueKey;              //스킬 모델 유니크 키
    public string skillIconUniqueKey;               //스킬 아이콘
    public int damageCoefficient;                 //스킬계수 

    public int skillLevel;                          //스킬 레벨 
    protected int skillType;                           //스킬 타입
    protected int buffStatType;                        //버프 스탯타입

    //TODO 스킬 매커니즘 추가는 상속받아서 처리

    public SkillDataBase(){

    }

    public SkillDataBase(string _skillUniqueKey, string _skillModelUniqueKey, string _skillIconUniqueKey, int _damageCoefficient,
        int _skillLevel, int _skillType, int _buffStatType)
    {
        skillUniqueKey = _skillUniqueKey;
        skillModelUniqueKey = _skillModelUniqueKey;
        skillIconUniqueKey = _skillIconUniqueKey;
        damageCoefficient = _damageCoefficient;
        skillLevel = _skillLevel;
        skillType = _skillType;
        buffStatType = _buffStatType;
    }
    public SkillDataBase(int _skillType, int _buffStatType)
    {
        skillLevel = 1;
        skillType = _skillType;
        buffStatType = _buffStatType;
    }

    public void LevelUpSkill(int _damageCoefficient)
    {
        skillLevel++;
        damageCoefficient += _damageCoefficient;
    }




    public SkillType SkillType
    {
        get
        {
            if (skillType < System.Enum.GetValues(typeof(SkillType)).Length && skillType >= 0)
                return (SkillType)skillType;
            else
                return SkillType.normalDamage;
        }
    }

    public BuffStatType BuffStatType
    {
        get
        {
            if (buffStatType < System.Enum.GetValues(typeof(BuffStatType)).Length && buffStatType >= 0)
                return (BuffStatType)buffStatType;
            else
                return BuffStatType.none;
        }
    }

    public Skill SkillDataConvertToSkill()
    {
        Skill skill = new Skill(skillUniqueKey, skillModelUniqueKey,skillIconUniqueKey,
                        damageCoefficient, skillLevel, SkillType,BuffStatType);

        return skill;

    }

}
