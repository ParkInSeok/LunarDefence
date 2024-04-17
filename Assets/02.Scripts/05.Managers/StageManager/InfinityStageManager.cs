using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfinityStageManager : Singleton<InfinityStageManager>
{
    [SerializeField] protected StageType stageType;

    public StageType StageType { get { return stageType; } }



    RoundController roundController;
    AdvantageController advantageController;
    ObjectPoolingController objectPoolingController; 


    public RoundController RoundController { get { return roundController; } }

    public AdvantageController AdvantageController { get { return advantageController; } }

    public ObjectPoolingController ObjectPoolingController { get { return objectPoolingController; } }



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

        roundController = GetComponentInChildren<RoundController>();
        advantageController = GetComponentInChildren<AdvantageController>();
        objectPoolingController = GetComponentInChildren<ObjectPoolingController>();


        roundController.Init();
        advantageController.Init();
        objectPoolingController.Init();

    }





}
