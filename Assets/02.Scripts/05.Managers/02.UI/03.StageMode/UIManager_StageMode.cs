using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager_StageMode : UIManager
{

    public List<CanvasScaler> canvasScalers = new List<CanvasScaler>();

    RectTransform backCanvasRect;

    [SerializeField] StageBackUI backUI;
    [Header("CommonInputUI")]
    [SerializeField] CommonSelectUI commonSelectUI;
    [SerializeField] UIEventTrigger fakeUI;

    // [SerializeField] CommonSelectUIType selectuitype;

    [Header("CommonNodeUnitStateUI")]
    [SerializeField] RectTransform commonUnitStateParent;
    Queue<Image> commonUnitStateUI = new Queue<Image>();
    List<Image> activated_commonUnitStateUI = new List<Image>();


    [SerializeField] Color canfusionColor;
    [SerializeField] Color cannotfusionColor;
    [SerializeField] Color heroColor;


    void Start()
    {

        BindEvents();
    }

    // Update is called once per frame
    void Update()
    {

    }


    protected override void Init()
    {

        backCanvasRect = canvasScalers[(int)CanvasLayerType.backType].GetComponent<RectTransform>();


    }



    protected override void BindEvents()
    {

        //backUI.Init(backCanvasRect);

        StageManager.Instance.PathController.selectPathNodeEventHandler_mouseDown += BindOpenCommonSelectUIEvent;
        StageManager.Instance.PathController.selectPathNodeEventHandler_mouseUp += BindQueueNodeStateUIEvent;

        //StageManager.Instance.PathController.selectPathNodeEventHandler_mouseUp += BindCloseCommonSelectUIEvent;
        fakeUI.onPointerDownEventHandler = (x) =>
        {
            StartCoroutine(Utility.CoroutineHelper.DelayFunction_EndOfFrame(() =>
            {
                fakeUI.gameObject.SetActive(false);
                commonSelectUI.HideCommonSelectUI();

                // LunarInputManager.Instance.isStopInput = false;
            }));
        };

    }



    #region CommonUI

    public override bool IsCreateUIActivate()
    {
        return commonSelectUI.gameObject.activeSelf;
    }



    private void BindOpenCommonSelectUIEvent(PathNode obj)
    {
        //if (StageManager.Instance.RoundController.State != RoundState.TowerPlacement)
        //    return;

        if (commonSelectUI.gameObject.activeSelf == false)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(obj.position);

            var vaildUI_max = screenPosition.x + commonSelectUI.rectTransform.sizeDelta.x;

            DirectionType dirType = vaildUI_max > Screen.currentResolution.width ? DirectionType.left : DirectionType.right;

            //var targetuitype = ((int)selectuitype) + 1;

            //if (targetuitype >= System.Enum.GetValues(typeof(CommonSelectUIType)).Length)
            //    targetuitype = 0;
            //selectuitype = (CommonSelectUIType)targetuitype;

            //walkable empty wall
            //unitState empty tower hero
            // empty + empty = create wall / create tower   //create tower 함수는 타워만들고 오브젝트풀에 등록
            // empty + tower || hero = show tower/hero info 
            // wall + empty = create tower
            // wall + tower = show tower/hero info
            // wall + hero X

            bool isCanCreateTower = StageManager.Instance.PathController.isCanCreateWall(obj);

            switch (obj.walkable)
            {
                case TileWallState.empty:
                    switch (obj.unitState)
                    {
                        case TileUnitState.empty:
                            //벽 설치 유효성 체크 후 true 라면
                            if (isCanCreateTower)
                            {
                                commonSelectUI.ShowCommonSelectUI(screenPosition, CommonSelectUIType.two, dirType,
                                   (x) => CreateTowerClick(x, obj), (x) => CreateWallClick(x, obj), null, "Create Tower", "Create Wall");
                            }
                            else
                            {
                                commonSelectUI.ShowCommonSelectUI(screenPosition, CommonSelectUIType.one, dirType,
                                    (x) => CreateTowerClick(x, obj), null, null, "Create Tower");
                            }
                            break;
                        case TileUnitState.tower:
                            if (isCanCreateTower)
                            {
                                commonSelectUI.ShowCommonSelectUI(screenPosition, CommonSelectUIType.two, dirType,
                                (x) => ShowInfoClick(x, obj), (x) => CreateWallClick(x, obj), null, "Show Info", "Create Wall");
                            }
                            else
                            {
                                commonSelectUI.ShowCommonSelectUI(screenPosition, CommonSelectUIType.one, dirType,
                                    (x) => ShowInfoClick(x, obj), null, null, "Show Info");
                            }
                            break;
                        case TileUnitState.hero:
                            commonSelectUI.ShowCommonSelectUI(screenPosition, CommonSelectUIType.one, dirType,
                              (x) => ShowInfoClick(x, obj), null, null, "Show Info");
                            break;
                    }
                    break;
                case TileWallState.wall:
                    switch (obj.unitState)
                    {
                        case TileUnitState.empty:
                            commonSelectUI.ShowCommonSelectUI(screenPosition, CommonSelectUIType.two, dirType,
                           (x) => CreateTowerClick(x, obj), (x) => DestroyWall(x, obj), null, "Create Tower", "Destroy Wall");
                            break;
                        case TileUnitState.tower:
                            commonSelectUI.ShowCommonSelectUI(screenPosition, CommonSelectUIType.one, dirType,
                                   (x) => CreateTowerClick(x, obj), (x) => DestroyWall(x, obj), null, "Show Info", "Destroy Wall");
                            break;
                    }
                    break;
                case TileWallState.notower:
                    commonSelectUI.ShowCommonSelectUI(screenPosition, CommonSelectUIType.one, dirType,
                        (x) => ShowInfoClick(x, obj), null, null, "Show Info");
                    break;
                default:
                    break;
            }

            fakeUI.gameObject.SetActive(true);

            fakeUI.rectTransform.SetAsLastSibling();
            commonSelectUI.rectTransform.SetAsLastSibling();


            StageManager.Instance.PathController.selectPathNodeEventHandler_mouse_nodeChanged = BindCloseCommonSelectUIEvent;

            //LunarInputManager.Instance.isStopInput = true;
        }

    }

    private void DestroyWall(PointerEventData obj, PathNode node)
    {
        //재화 30% 돌려주기
        node.ChangeWalkable(TileWallState.empty);
        StageManager.Instance.PathController.ReFindPath();
        if (StageManager.Instance.isDevelopMode)
            node.material.color = node.origineColor;

        HideCommonSelectUI();
    }

    private void ShowInfoClick(PointerEventData obj, PathNode node)
    {


        HideCommonSelectUI();

    }

    private void CreateWallClick(PointerEventData obj, PathNode node)
    {
        node.ChangeWalkable(TileWallState.wall);
        StageManager.Instance.PathController.ReFindPath();
        if (StageManager.Instance.isDevelopMode)
            node.material.color = Color.blue;

        HideCommonSelectUI();

    }

    private void CreateTowerClick(PointerEventData obj, PathNode node)
    {
        //재화 유효성 체크

        if (node.unitState != TileUnitState.empty)
            return;

        var selectedKey = DataManager.Instance.GetRandomTowerKey();

        StageManager.Instance.ObjectPoolingController.GetTowerPool(selectedKey, node);

        HideCommonSelectUI();



    }

    void HideCommonSelectUI()
    {
        StartCoroutine(Utility.CoroutineHelper.DelayFunction_NextEndOfFrame(() =>
       {
           commonSelectUI.HideCommonSelectUI();
           fakeUI.gameObject.SetActive(false);

            //LunarInputManager.InstanceisStopInput = false;
        }));
    }


    private void BindCloseCommonSelectUIEvent(bool arg2, PathNode baseNode)
    {
        if (arg2 == false)
        {
            //Debug.Log("BindCloseCommonSelectUIEvent");
            //같은 노드가 아닐때 ui 끄기
            if (fakeUI.gameObject.activeSelf)
            {
                fakeUI.gameObject.SetActive(false);
                commonSelectUI.HideCommonSelectUI();
            }
            if (baseNode != null)
            {
                if (baseNode.unitState == TileUnitState.tower)
                {
                    //현재 노드상태가 타워라면
                    // 전체 노드들 조회해서 타워상태인 노드들과의 머지 상태 : 초록색 / 머지 불가 상태 (이동) : 빨간색 알파는 0.2
                    // 영웅상태 기본 빨강색 , 먹이는 로직 상태라면 회색
                    ShowAllNodeStateByBaseNode(baseNode);
                }
            }

            StageManager.Instance.PathController.selectPathNodeEventHandler_mouse_nodeChanged = null;
        }

    }


    #endregion

    #region NodeStateUIByBaseNode

    void ShowAllNodeStateByBaseNode(PathNode baseNode)
    {
        if (baseNode.unitState != TileUnitState.tower)
            return;

        var grid = StageManager.Instance.PathController.Grid;
        var maxRow = StageManager.Instance.PathController.MaxRow;
        var maxColumn = StageManager.Instance.PathController.MaxColumn;
        var width = StageManager.Instance.PathController.UI_Tile_Width;
        var height = StageManager.Instance.PathController.UI_Tile_Height;
        var baseTower = (Tower)StageManager.Instance.ObjectPoolingController.GetTargetTower(baseNode.row, baseNode.column);




        for (int i = 0; i < maxRow; i++)
        {
            for (int j = 0; j < maxColumn; j++)
            {
                var node = grid[i, j];
                if (baseNode == node)
                    continue;

                if (node.unitState == TileUnitState.empty)
                    continue;

                Vector3 screenPosition = Camera.main.WorldToScreenPoint(node.position);
                var image = GetComonUnitStateUI();
                image.rectTransform.sizeDelta = new Vector2(width, height);
                image.rectTransform.position = screenPosition;

                switch (node.unitState)
                {
                    case TileUnitState.empty:
                        break;
                    case TileUnitState.tower:
                        var nodeTower = (Tower)StageManager.Instance.ObjectPoolingController.GetTargetTower(node.row, node.column);
                        bool isCanFusion = StageManager.Instance.FusionController.IsCanFusion(baseTower, nodeTower) &&
                            StageManager.Instance.FusionController.isCanLevelUp(baseTower);
                        image.color = isCanFusion ? canfusionColor : cannotfusionColor;
                        break;
                    case TileUnitState.hero:
                        image.color = heroColor;
                        break;
                }

            }
        }


    }

    Image GetComonUnitStateUI()
    {
        if(commonUnitStateUI.Count <= 0)
        {
            GameObject imageObj = new GameObject("commonUnitStateUI");
            imageObj.transform.SetParent(commonUnitStateParent,false);
            Image image = imageObj.AddComponent<Image>();
            RectTransform rt = image.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0, 0); // 캔버스 중앙에 위치
            rt.sizeDelta = new Vector2(200, 200); // 크기 조절
            image.gameObject.SetActive(false);
            commonUnitStateUI.Enqueue(image);

        }

        var result = commonUnitStateUI.Dequeue();
        result.gameObject.SetActive(true);
        activated_commonUnitStateUI.Add(result);
        return result;
    }


    private void BindQueueNodeStateUIEvent(PathNode arg1, PathNode arg2)
    {
        if (activated_commonUnitStateUI.Count <= 0)
            return;

        for (int i = 0; i < activated_commonUnitStateUI.Count; i++)
        {
            commonUnitStateUI.Enqueue(activated_commonUnitStateUI[i]);
            activated_commonUnitStateUI[i].gameObject.SetActive(false);
        }

        activated_commonUnitStateUI.Clear();

    }


    #endregion

}
