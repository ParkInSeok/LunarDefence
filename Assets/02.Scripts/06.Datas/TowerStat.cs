using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  System;




[Serializable]
public class TowerStat : StatBase
{

    protected TowerData origineTowerStat;
    [SerializeField] protected TowerData currentTowerStat;

    

    public TowerData CurrentTowerStat { get { return currentTowerStat; } }


    protected Skill skill;

    public Skill Skill { get { return skill; } }

    public virtual void InitStat(TowerData stat)
    {
        unitState = UnitState.alive;

        origineTowerStat = stat;

        currentTowerStat = origineTowerStat;

        skill = DataManager.Instance.GameData.GetSkill(stat.skillUniqueKey);
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
                calculationDamage -= (int)((float)calculationDamage * 100f / (100f+currentTowerStat.spdef)); /* *(currentEnemyStat.propertyResistPower))*/
                break;
        }

        Debug.Log("GetDamage : " + calculationDamage);

        currentTowerStat.hp -= calculationDamage;

        if(currentTowerStat.hp <= 0)
        {
            DieEvent();
        }


    }

    public override void DieEvent()
    {
        unitState = UnitState.die;

        dieEventHandler?.Invoke();
    }

}
