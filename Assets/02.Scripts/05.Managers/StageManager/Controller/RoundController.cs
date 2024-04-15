using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RoundState
{
    Rounding,
    TowerPlacement,
    BossKilled,
    SelectAdvantage,
    SelectedAdvantage,


}


public class RoundController : MonoBehaviour
{

    [SerializeField] RoundState state;

    public RoundState State { get { return state; }  }


    [Header("Debugging")]
    [SerializeField] int currentRound = 1;

    public int CurrentRound { get { return currentRound; } }


    [SerializeField] int currentEnemyCount;
    public int CurrentEnemyCount { get { return currentEnemyCount; } }



    public Action<RoundState> changedRoundStateEventHandler;
    public Action roundStartEventHandler;
    public Action selectAdvantageEventHandler;
    public Action selectedAdvantageEventHandler;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        currentRound = 1;
        StartAdvantageSelectFlow();
    }

    public void BindEvents()
    {

    }
    public void StartAdvantageSelectFlow()
    {
        state = RoundState.SelectAdvantage;
        changedRoundStateEventHandler?.Invoke(state);
    }

    public void StartTowerPlacementFlow()
    {
        Debug.Log("StartTowerPlacementFlow ");
        state = RoundState.TowerPlacement;
        NextRound();
        changedRoundStateEventHandler?.Invoke(state);
    }

    public void StartRoundFlow()
    {

        //���� ������
        //currentEnemyCount = 
        state = RoundState.Rounding;

        roundStartEventHandler?.Invoke();
        changedRoundStateEventHandler?.Invoke(state);
    }

    public void KilledAllEnemy()
    {
        currentEnemyCount--;
        if (currentEnemyCount <= 0)
        {
            //���� ��
            StartTowerPlacementFlow();
        }
    }

    public void NextRound()
    {
        currentRound++;
    }





}
