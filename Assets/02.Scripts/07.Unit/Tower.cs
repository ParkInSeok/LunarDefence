using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : UnitBase
{
    [SerializeField] TowerStat stat;

    public TowerStat Stat { get { return stat; } }


    public void Init(TowerData _stat)
    {
        stat.InitStat(_stat);

        CreateModel(stat.CurrentTowerStat.modelUniqueKey);


    }


}
