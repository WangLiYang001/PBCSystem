using UnityEngine;

public class HUIWindowStart : HUIBase 
{
    public void Click()
    {
        HUITipManager.Instance.Play();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Login, finish: (window) =>//当程序开始启动时加载登录界面
        {

        });

    }
    public void Exit()
    {
        HUITipManager.Instance.PlayExit();
        Application.Quit();
    }
   
}
