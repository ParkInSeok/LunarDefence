using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HeroStat : StatBase
{
    protected HeroData origineHeroStat;
    [SerializeField] protected HeroData currentHeroStat;

    public HeroData CurrentTowerStat { get { return currentHeroStat; } }


    protected List<Skill> skills;

    public List<Skill> Skills { get { return skills; } }

    public virtual void InitStat(HeroData stat)
    {
        unitState = UnitState.alive;

        origineHeroStat = stat;
        origineHeroStat.SetSkills(stat.skillUniqueKeys);

        currentHeroStat = origineHeroStat;

        for (int i = 0; i < origineHeroStat.skillUniqueKeyList.Count; i++)
        {
            int index = i;
            skills[index] = DataManager.Instance.GameData.GetSkill(origineHeroStat.skillUniqueKeyList[index]);
        }

    }


    public override void DieEvent()
    {
        unitState = UnitState.die;

        dieEventHandler?.Invoke();
    }

    public override void GetDamage(int _damage, DamageType _damageType)
    {
        //TO DO property type , enemy type not exist 0225 
        if (unitState == UnitState.die)
            return;

        var calculationDamage = _damage;
        switch (_damageType)
        {
            case DamageType.physicalDamageType:
                calculationDamage -= (int)((float)calculationDamage * 100f / (100f + currentHeroStat.def)); /* *(currentEnemyStat.propertyResistPower))*/
                break;
            case DamageType.masicDamageType:
                calculationDamage -= (int)((float)calculationDamage * 100f / (100f + currentHeroStat.spdef)); /* *(currentEnemyStat.propertyResistPower))*/
                break;
        }

        Debug.Log("GetDamage : " + calculationDamage);

        currentHeroStat.hp -= calculationDamage;

        if (currentHeroStat.hp <= 0)
        {
            DieEvent();
        }
   
    }
}
