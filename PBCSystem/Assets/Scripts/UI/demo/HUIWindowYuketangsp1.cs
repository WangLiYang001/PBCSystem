using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUIWindowYuketangsp1 : HUIBase
{
    public Button _obj;

    public void ClickLoad()
    {
        HUITipManager.Instance.PlayQue();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Yunketangrsp1, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void ClickExit()
    {
        HUITipManager.Instance.PlayExit();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Yunketang, finish: (selfWindow) =>
        {
            selfWindow.InitData();

        });
    }
}
