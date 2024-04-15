using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfinityStageManager : StageManager
{


    RoundController roundController;
    AdvantageController advantageController;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Init()
    {
        base.Init();

        roundController = GetComponentInChildren<RoundController>();
        advantageController = GetComponentInChildren<AdvantageController>();


        roundController.Init();
        advantageController.Init();
    }





}
