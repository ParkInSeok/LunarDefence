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

    public void GameOver()
    {
        //게임패배 연출 

        DataManager.Instance.ResetMemory(); //모든 메모리 리셋

        //연출끝나고 씬이동 -> 메인씬
        //해당 라운드 표시해줌
    }



}
