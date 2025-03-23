using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StageType
{
    Loading,
    Start,
    Infinity,
    BossRaid,
}


public class StageManager : Singleton<StageManager>
{

    [SerializeField]protected StageType stageType;

    public StageType StageType { get { return stageType; } }



    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Init()
    {
        stageType = (StageType)SceneManager.GetActiveScene().buildIndex;

    }



}
