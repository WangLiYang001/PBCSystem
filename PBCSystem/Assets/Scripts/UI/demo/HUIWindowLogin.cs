using UnityEngine;
using UnityEngine.UI;


public class HUIWindowLogin : HUIBase
{ 
    public InputField _userIpf;
    public InputField _pwdIpf;
    public Toggle _toggle;
    string userid;
    string password;
    string message;
    public override void InitData(bool forceInit = false)
    {
        if (string.IsNullOrEmpty(HLoginDataManager.Instance.CurrUserInfo._userid)==false)
        {
            _userIpf.text = HLoginDataManager.Instance.CurrUserInfo._userid;
            _pwdIpf.text = HLoginDataManager.Instance.CurrUserInfo._password;
        }
        
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("name"))
        {
            _userIpf.text = PlayerPrefs.GetString("name");
        }

        if (PlayerPrefs.HasKey("password"))
        {

            _pwdIpf.text = PlayerPrefs.GetString("password");

        }
    }
    public void Login()
    {
        userid = _userIpf.text;
        password = _pwdIpf.text;
        HUITipManager.Instance.Play();
        HLoginDataManager.Instance.Login(userid, password, out message);
        HUITipManager.Instance.ShowTip(message);
    }
    public void Zhuce()
    {
        HUITipManager.Instance.PlayQue();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Zhuce, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void OnClick(bool isOn)
    {
        if (_toggle.isOn)
        {
            PlayerPrefs.SetString("name", _userIpf.text);

            PlayerPrefs.SetString("password", _pwdIpf.text);
        }
    }
    public void ClickExit()
    {
        HUITipManager.Instance.PlayExit();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Start, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
}
