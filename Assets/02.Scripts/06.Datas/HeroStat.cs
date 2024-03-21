using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HeroStat : StatBase
{
    protected HeroData origineTowerStat;
    protected HeroData currentTowerStat;

    public HeroData CurrentTowerStat { get { return currentTowerStat; } }


    protected List<Skill> skills;

    public List<Skill> Skills { get { return skills; } }

    public virtual void InitStat(HeroData stat)
    {
        unitState = UnitState.alive;

        origineTowerStat = stat;

        currentTowerStat = origineTowerStat;

        for (int i = 0; i < stat.skillUniqueKeys.Count; i++)
        {
            int index = i;
            skills[index] = DataManager.Instance.GameData.GetSkill(stat.skillUniqueKeys[index]);
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
                calculationDamage -= (int)((float)calculationDamage * 100f / (100f + currentTowerStat.def)); /* *(currentEnemyStat.propertyResistPower))*/
                break;
            case DamageType.masicDamageType:
                calculationDamage -= (int)((float)calculationDamage * 100f / (100f + currentTowerStat.spdef)); /* *(currentEnemyStat.propertyResistPower))*/
                break;
        }

        Debug.Log("GetDamage : " + calculationDamage);

        currentTowerStat.hp -= calculationDamage;

        if (currentTowerStat.hp <= 0)
        {
            DieEvent();
        }
   
    }
}
