using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : UnitBase
{

    [SerializeField] HeroStat stat;

    public HeroStat Stat { get { return stat; } }


    public void Init(HeroData _stat)
    {
        stat.InitStat(_stat);

        CreateModel(stat.CurrentTowerStat.modelUniqueKey);
    }


}
