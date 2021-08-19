using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUIWindowDemo : HUIBase
{
    //UI
    public override void Init(bool forceInit = false)
    {
        base.Init(forceInit);
    }
    //数据
    public override void InitData(bool forceInit = false)
    {
        base.InitData(forceInit);
    }


    public void ButttonTest()
    {
    }
    public void OnButtonCLick()
    {
        HUIManager.Instance.OpenUI("UI/Window/UIWindow_Login");

    }
   

}
