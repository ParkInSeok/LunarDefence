using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager_StageMode : UIManager
{

    public List<CanvasScaler> canvasScalers = new List<CanvasScaler>();

    RectTransform backCanvasRect;

    [SerializeField] StageBackUI backUI;
    [SerializeField] CommonSelectUI commonSelectUI;
    [SerializeField] UIEventTrigger fakeUI;

    // [SerializeField] CommonSelectUIType selectuitype;



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

        StageManager.Instance.PathController.selectPathNodeEventHandler += BindOpenCommonSelectUIEvent;
        fakeUI.onPointerDownEventHandler = (x) =>
        {
            UtilityManager.Instance.DelayFunction_EndOfFrame(() =>
            {
                fakeUI.gameObject.SetActive(false);
                commonSelectUI.HideCommonSelectUI();

                LunarInputManager.Instance.isStopInput = false;
            });
        };

    }

    private void BindOpenCommonSelectUIEvent(PathNode obj)
    {
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

            switch (obj.walkable)
            {
                case TileWallState.empty:
                    switch (obj.unitState)
                    {
                        case TileUnitState.empty:
                            commonSelectUI.ShowCommonSelectUI(screenPosition, CommonSelectUIType.two, dirType,
                               (x) => CreateTowerClick(x, obj), (x) => CreateWallClick(x, obj), null, "Create Tower", "Create Wall");
                            break;
                        case TileUnitState.tower:
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
                            commonSelectUI.ShowCommonSelectUI(screenPosition, CommonSelectUIType.one, dirType,
                           (x) => CreateTowerClick(x, obj), null, null, "Create Tower");
                            break;
                        case TileUnitState.tower:
                            commonSelectUI.ShowCommonSelectUI(screenPosition, CommonSelectUIType.one, dirType,
                                   (x) => CreateTowerClick(x, obj), null, null, "Show Info");
                            break;
                    }
                    break;
                default:
                    break;
            }

            fakeUI.gameObject.SetActive(true);

            fakeUI.rectTransform.SetAsLastSibling();
            commonSelectUI.rectTransform.SetAsLastSibling();

            LunarInputManager.Instance.isStopInput = true;
        }

    }

    private void ShowInfoClick(PointerEventData obj, PathNode node)
    {



        commonSelectUI.HideCommonSelectUI();

    }

    private void CreateWallClick(PointerEventData obj, PathNode node)
    {



        commonSelectUI.HideCommonSelectUI();

    }

    private void CreateTowerClick(PointerEventData obj, PathNode node)
    {
        //재화 유효성 체크

        if (node.unitState != TileUnitState.empty)
            return;

        var selectedKey = DataManager.Instance.GetRandomTowerKey();

        StageManager.Instance.ObjectPoolingController.GetTowerPool(selectedKey, node);

        UtilityManager.Instance.DelayFunction_NextEndOfFrame(() =>
        {
            commonSelectUI.HideCommonSelectUI();
            fakeUI.gameObject.SetActive(false);

            LunarInputManager.Instance.isStopInput = false;
        });



    }
}
