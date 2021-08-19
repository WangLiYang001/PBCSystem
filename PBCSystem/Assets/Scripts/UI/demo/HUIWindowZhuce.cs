using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class HUIWindowZhuce : HUIBase 
{
    public InputField _name;
    public Dropdown _sex;
    public InputField _tel;
    public InputField _id;
    public InputField _userid;
    public InputField _password;
    public InputField _newPassword;
    public string message;
    public override void InitData(bool forceInit = false)
    {
        int n = 0;
        Drop_select(n);
    }
    public void Drop_select(int n)
    {
        if (n == 0)
        {
            _sex.captionText.text = "";
        }
    }
    public void ClickExit()
    {
        _name.text = "";
        _tel.text = "";
        _id.text = "";
        _userid.text = "";
        _password.text = "";
        _newPassword.text = "";
        HUITipManager.Instance.PlayExit();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Login, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void ClickAdd()
    {
        AddDatas(); 
    }
    void AddDatas()
    {
        string name = _name.text;
        string tel = _tel.text;
        string id = _id.text;
        string sex = _sex.captionText.text;
        string userid = _userid.text;
        string password = _password.text;
        string newPassword = _newPassword.text;
        if (sex == "")
        {
            HUITipManager.Instance.ShowTip("请输入性别");
        }
        else
        {
            if (password.Equals(newPassword))
            {
                HLoginDataManager.Instance.Register(name, tel, id, sex, userid, password, out message);
                HUITipManager.Instance.ShowTip(message);
                if (message == "注册成功")
                {
                    _name.text = "";
                    _tel.text = "";
                    _id.text = "";
                    _userid.text = "";
                    _password.text = "";
                    _newPassword.text = "";
                    HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Login, finish: (selfWindow) => {
                        selfWindow.InitData();
                    });
                }
            }
            else
            {
                HUITipManager.Instance.ShowTip("两次密码输入不一致");
            }
        }
    }
}

