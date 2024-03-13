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
    //스킬 매커니즘 
    protected int skillType;                           //스킬 타입
    protected int buffStatType;                        //버프 스탯타입


    public SkillDataBase(){

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
