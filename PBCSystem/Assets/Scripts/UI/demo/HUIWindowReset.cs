using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUIWindowReset : HUIBase 
{
    public Text _password;
    public InputField _repassword;
    public InputField _enrepassword;
    string _userid;
    string password;
    public string message;
    public override void InitData(bool forceInit = false)
    {
        _repassword.text = "";
        _enrepassword.text = "";
    }
    public void ResetDatas(string userID)
    {
        HLoginDataManager.HFunction info = HLoginDataManager.Instance.FindUserInfo(userID);
        _userid = info._userid;
        _password.text = info._password;
     }
    public void ClickExit()
    {
        HUITipManager.Instance.PlayExit();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Data, finish: (selfWindow) =>
        {
            selfWindow.InitData();
        });
    }
    public void ResetData()
    {
        if (_password.text.Equals(_repassword.text))
        {
            HUITipManager.Instance.ShowTip("新密码与旧密码重复");
        }
        if (_enrepassword.text.Equals(_repassword.text))
        {
            password = _repassword.text;
            bool b = HLoginDataManager.Instance.SetPassWord(_userid, password, out message);
            HUITipManager.Instance.ShowTip(message);
            if (b==true)
            {
                HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Data, finish: (selfWindow) =>
                {
                    selfWindow.InitData();
                });
            }
        }
        else
        {
            HUITipManager.Instance.ShowTip("两次输入不一样");
        }     
    }
}
