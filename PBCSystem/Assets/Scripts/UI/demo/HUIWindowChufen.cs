using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUIWindowChufen : HUIBase 
{
    public void Exit()
    {
        HUITipManager.Instance.PlayExit();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Danghui, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
}
