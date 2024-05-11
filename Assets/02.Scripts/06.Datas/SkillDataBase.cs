using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public enum SkillActivationType
{
    passive,
    mana,
}


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
    public string skillIconUniqueKey;               //스킬 아이콘
    public int damageCoefficient;                 //스킬계수 
    public int activatePercent;                     //발동확률

    public int skillLevel;                          //스킬 레벨 
    [SerializeField] protected int skillType;                           //스킬 타입
    [SerializeField] protected int buffStatType;                        //버프 스탯타입
    [SerializeField] protected int activationType;                       //발동 타입

    

    //TODO 스킬 매커니즘 추가는 상속받아서 처리

    public SkillDataBase(){

    }

    public SkillDataBase(string _skillUniqueKey, string _skillIconUniqueKey, int _damageCoefficient,
        int _skillLevel, int _skillType, int _buffStatType, int _activationType, int _activatePercent)
    {
        skillUniqueKey = _skillUniqueKey;
        skillIconUniqueKey = _skillIconUniqueKey;
        damageCoefficient = _damageCoefficient;
        skillLevel = _skillLevel;
        skillType = _skillType;
        buffStatType = _buffStatType;
        activationType = _activationType;
        activatePercent = _activatePercent;
    }

    public SkillDataBase(int _skillType, int _buffStatType, int _activationType)
    {
        skillLevel = 1;
        skillType = _skillType;
        buffStatType = _buffStatType;
        activationType = _activationType;
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

    public SkillActivationType SkillActivationType { 
        get
        {
            if (activationType < System.Enum.GetValues(typeof(SkillActivationType)).Length && activationType >= 0)
                return (SkillActivationType)activationType;
            else
                return SkillActivationType.passive;
        } 
    }


    public Skill SkillDataConvertToSkill()
    {
        Skill skill = new Skill(skillUniqueKey, skillIconUniqueKey,activatePercent,
                        damageCoefficient, skillLevel, SkillType,BuffStatType,SkillActivationType);

        return skill;

    }

}
