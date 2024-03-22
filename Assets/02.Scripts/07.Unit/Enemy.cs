using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
{
    [Header("Enemy")]
    [SerializeField] EnemyStat stat;

    public EnemyStat Stat { get { return stat; } }


    public void Init(EnemyData _stat)
    {
        stat.InitStat(_stat);

        CreateModel(stat.CurrentTowerStat.modelUniqueKey);


    }

    protected override void CreateModel(string modelUniqueKey)
    {
        base.CreateModel(modelUniqueKey);
    }

    protected override void SetModel(GameObject loadedObject)
    {
        base.SetModel(loadedObject);
    }

    public void Move(Transform targetHero)
    {

    }





}
