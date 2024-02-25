using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIManager : Singleton<UIManager>
{
    // 각 씬마다 상속받은 uimanager가 존재하며 dont destroy false setting



    protected override void Awake()
    {
        dontDestroy = false;
        base.Awake();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Init()
    {


    }

    protected abstract void BindEvents();
 




}
