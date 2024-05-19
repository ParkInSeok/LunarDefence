using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HeroStat : BaseStat
{
    protected HeroData origineHeroStat;
    [SerializeField] protected HeroData currentHeroStat;

    public HeroData CurrentHeroStat { get { return currentHeroStat; } }


    protected List<Skill> skills;

    public List<Skill> Skills { get { return skills; } }

    public virtual void InitStat(HeroData stat)
    {

        origineHeroStat = stat;

        currentHeroStat = origineHeroStat;
        
        if (string.IsNullOrEmpty(stat.skillUniqueKeys) == false)
        {
            origineHeroStat.SetSkills(stat.skillUniqueKeys);

            for (int i = 0; i < origineHeroStat.skillUniqueKeyList.Count; i++)
            {
                int index = i;
                skills[index] = DataManager.Instance.GameData.GetSkill(origineHeroStat.skillUniqueKeyList[index]);
            }
        }

    }


    public override void DieEvent()
    {

        dieEventHandler?.Invoke();
    }

    public override void GetDamage(int _damage, DamageType _damageType)
    {
        //TO DO property type , enemy type not exist 0225 
        var calculationDamage = _damage;
        var currentDamage = _damage;
        switch (_damageType)
        {
            case DamageType.physicalDamageType:
                calculationDamage = (int)((float)currentDamage * 100f / (100f + currentHeroStat.def)); /* *(currentEnemyStat.propertyResistPower))*/
                break;
            case DamageType.masicDamageType:
                calculationDamage = (int)((float)currentDamage * 100f / (100f + currentHeroStat.spdef)); /* *(currentEnemyStat.propertyResistPower))*/
                break;
        }

        Debug.Log("GetDamage : " + calculationDamage + "current Unit : " + currentHeroStat.uniqueKey);

        currentHeroStat.hp -= calculationDamage;

        if (currentHeroStat.hp <= 0)
        {
            DieEvent();
        }
   
    }
}
