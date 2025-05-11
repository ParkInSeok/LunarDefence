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


public class StageManager : SingletonMono<StageManager>
{

    [SerializeField]protected StageType stageType;

    public StageType StageType { get { return stageType; } }

    [Header("RoundController")]
    [SerializeField] protected RoundController roundController;
    [Header("AdvantageController")]
    [SerializeField] protected AdvantageController advantageController;
    [Header("FusionController")]
    [SerializeField] protected FusionController fusionController;

    protected ObjectPoolingController objectPoolingController;
    protected PathController pathController;


    public RoundController RoundController { get { return roundController; } }

    public AdvantageController AdvantageController { get { return advantageController; } }
    public FusionController FusionController { get { return fusionController; } }

    public ObjectPoolingController ObjectPoolingController { get { return objectPoolingController; } }

    public PathController PathController { get { return pathController; } }

    [Header("Debugging")]
    public bool isDevelopMode;



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

        if (roundController == null)
            roundController = new RoundController();

        if (advantageController == null)
            advantageController = new AdvantageController();

        if (fusionController == null)
            fusionController = new FusionController();

        pathController = GetComponentInChildren<PathController>();
        objectPoolingController = GetComponentInChildren<ObjectPoolingController>();

        pathController.Init();
        roundController.Init();
        advantageController.Init();
        fusionController.Init();
        objectPoolingController.Init(pathController.GetStartPathNode, pathController.GetTargetPathNode);


    }

    public void GameOver()
    {
        //게임패배 연출 

        DataManager.Instance.ResetMemory(); //모든 메모리 리셋

        //연출끝나고 씬이동 -> 메인씬
        //해당 라운드 표시해줌
    }

}
