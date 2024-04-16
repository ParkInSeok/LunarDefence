using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfinityStageManager : StageManager
{


    RoundController roundController;
    AdvantageController advantageController;
    ObjectPoolingController objectPoolingController; 



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
        objectPoolingController = GetComponentInChildren<ObjectPoolingController>();


        roundController.Init();
        advantageController.Init();

        objectPoolingController.Init();
    }





}
