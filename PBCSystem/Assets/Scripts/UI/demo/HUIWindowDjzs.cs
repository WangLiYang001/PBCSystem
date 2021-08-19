using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUIWindowDjzs : HUIBase 
{
    public void Return()
    {
        HUITipManager.Instance.PlayExit();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Main, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void Enter()
    {
        HUITipManager.Instance.PlayQue();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Dtxz, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
}
