using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Skill
{
    public string skillUniqueKey;                   //스킬 유니크키
    public GameObject skillModel;
    public string skillIconUniqueKey;               //스킬 아이콘
    public Sprite skillIcon;
    public int damageCoefficient;                 //스킬계수 
    public int activatePercent;                     //발동 확률

    public int skillLevel;                          //스킬 레벨 
    //스킬 매커니즘 
    public SkillType skillType;                           //스킬 타입
    public BuffStatType buffStatType;                        //버프 스탯타입
    public SkillActivationType activationType;              //발동타입

    public Skill()
    {

    }
    public Skill(string _skillUniqueKey,  string _skillIconUniqueKey, int _damageCoefficient, int _activatePercent,
                    int _skillLevel, SkillType _skillType, BuffStatType _BuffStatType, SkillActivationType _SkillActivationType)
    {
        skillUniqueKey = _skillUniqueKey;
        skillIconUniqueKey = _skillIconUniqueKey;
        damageCoefficient = _damageCoefficient;
        skillLevel = _skillLevel;
        skillType = _skillType;
        buffStatType = _BuffStatType;
        activationType = _SkillActivationType;
        activatePercent = _activatePercent;

        DataManager.Instance.GetGameObject(skillUniqueKey, SetModel);
        DataManager.Instance.GetSprite(skillIconUniqueKey, SetSprite);


    }

    public Skill(BaseSkillData _skillDataBase)
    {
        skillUniqueKey = _skillDataBase.skillUniqueKey;
        skillIconUniqueKey = _skillDataBase.skillIconUniqueKey;
        damageCoefficient = _skillDataBase.damageCoefficient;
        skillLevel = _skillDataBase.skillLevel;
        skillType = _skillDataBase.SkillType;
        buffStatType = _skillDataBase.BuffStatType;
        activationType = _skillDataBase.SkillActivationType;
        activatePercent = _skillDataBase.activatePercent;

        DataManager.Instance.GetGameObject(skillUniqueKey, SetModel);
        DataManager.Instance.GetSprite(skillUniqueKey, SetSprite);



    }

    void SetModel(GameObject obj)
    {
        skillModel = obj;

    }

    void SetSprite(Sprite _sprite)
    {
        skillIcon = _sprite;
    }

}
