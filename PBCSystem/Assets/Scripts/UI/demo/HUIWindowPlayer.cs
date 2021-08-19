using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUIWindowPlayer : HUIBase
{
    public GameObject _object1;
    public override void InitData(bool forceInit = false)
    {
        _object1.gameObject.SetActive(false);
    }
    public void LoadMovie()
    {
        _object1.gameObject.SetActive(true);
    }
    public void Return()
    {
        HUITipManager.Instance.PlayExit();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Huodong, finish: (selfWindow) =>
        {
            selfWindow.InitData();
        });
    }
}
