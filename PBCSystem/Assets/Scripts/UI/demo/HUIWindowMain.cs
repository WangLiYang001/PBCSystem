using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;
using System.Net.NetworkInformation;

public class HUIWindowMain : HUIBase
{
    public Text title;
    public Text _freeTitle;
    public Text _Title;
    public Text _19Title;
    public Text _fcTitle;
    public Text _txtCurrentTime;
    public Text _txtCurrentType;
    public Text _userName;
    public Button _bianji;
    public InputField _input;
    public Button _sure;
    Texture2D tempImage;
    RawImage image;
    string path;
    int i;
    public List<Button> buttons = new List<Button>();
    void LoadImage()
    {
        for (int i=0; i<buttons.Count;i++)
        {            
            image = buttons[i].GetComponent<RawImage>();
            string name= buttons[i].name.ToString();
            string texturePath = Application.streamingAssetsPath + "/主界面/主界面图片/"+name+".png";
            string filePath = "file://" + texturePath;
            WWW www = new WWW(filePath);
            if (www.error != null)
            {
                Debug.LogError(filePath + www.error);
                image.gameObject.SetActive(false);
            }
            else
            {
                image.gameObject.SetActive(true);
                tempImage = www.texture;
                image.texture = tempImage;
            }
        }
    }
    private void PingNetAddress()
    {
        try
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            PingReply pr = ping.Send("www.baidu.com", 3000);
            if (pr.Status == IPStatus.Success)
            {
                i = 0;
            }
            else
            {
                i = 1;
            }
        }
        catch (Exception e)
        {
            i = 2;
        }
    }
    public override void InitData(bool forceInit = false)
    {
        LoadImage();
        path = Application.streamingAssetsPath + "/主界面/主界面主题.txt";
        WWW www = new WWW(path);
        title.text = www.text;
        _bianji.gameObject.SetActive(false);
        _input.gameObject.SetActive(false);
        _sure.gameObject.SetActive(false);
        if (HLoginDataManager.Instance.IsAdmin)
        {
            _userName.text = "管理员：" + HLoginDataManager.Instance.AdminUserID;
            _bianji.gameObject.SetActive(true);
        }
        else
            _userName.text = HLoginDataManager.Instance.CurrUserInfo._name;
        string _titlepath = Application.streamingAssetsPath + "/主界面/企业展馆.txt";
        WWW ww = new WWW(_titlepath);
        _freeTitle.text = ww.text;
        string _title = Application.streamingAssetsPath + "/主界面/企业介绍.txt";
        WWW w = new WWW(_title);
        _Title.text = w.text;
        string _19datitle = Application.streamingAssetsPath + "/主界面/19大问答.txt";
        WWW _datitle = new WWW(_19datitle);
        _19Title.text = _datitle.text;
        string _Ftitle = Application.streamingAssetsPath + "/主界面/党员风采.txt";
        WWW _ftitle = new WWW(_Ftitle);
        _fcTitle.text = _ftitle.text;
    }
   
    public void Click()
    {
        _input.gameObject.SetActive(true);
        _sure.gameObject.SetActive(true);
    }
    public void ClickSure()
    {
        if (string.IsNullOrEmpty(_input.text) == false)
        {
            title.text = _input.text;
            StreamWriter sw;
            FileInfo fi = new FileInfo(path);
            sw = fi.CreateText();
            sw.WriteLine(_input.text);
            sw.Close();
            sw.Dispose();
        }
        _input.gameObject.SetActive(false);
        _sure.gameObject.SetActive(false);
    }
    public void Zuzhi()
    {
        HUITipManager.Instance.Play();
        PingNetAddress();
        if (i==2)
        {
            HUITipManager.Instance.ShowTip("请检查网络设置");
        }
        else
        {
            HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Zuzhi, finish: (selfWindow) =>
            {
                selfWindow.InitData();

            });
        }
    }
    public void Sixiang()
    {
        HUITipManager.Instance.Play();
        PingNetAddress();
        if (i == 2)
        {
            HUITipManager.Instance.ShowTip("请检查网络设置");
        }
        else
        {
            HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Sixiang, finish: (selfWindow) =>
            {
                selfWindow.InitData();
            });
        }
    }
    public void Nineteenda()
    {
        HUITipManager.Instance.Play();
        PingNetAddress();
        if (i == 2)
        {
            HUITipManager.Instance.ShowTip("请检查网络设置");
        }
        else
        {
            HUIManager.Instance.OpenUI(HUIWindowDefine.Window_19da, finish: (selfWindow) =>
            {
                selfWindow.InitData();
            });
        }
    }
    public void Ketan()
    {
        HUITipManager.Instance.Play();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Yunketang, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    void Update()
    {
        //获取系统当前时间
        DateTime NowTime = DateTime.Now.ToLocalTime();
        _txtCurrentTime.text = NowTime.ToString("yyyy/MM/dd          HH:mm:ss");
        if (NowTime.Hour > 0 && NowTime.Hour < 12)
        {
            _txtCurrentType.text = "上午";
        }
        else
        {
            _txtCurrentType.text = "下午";
        }
    }
    public void Djzs()
    {
        HUITipManager.Instance.Play();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Djzs, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void Return()
    {
        HUITipManager.Instance.PlayExit();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Login, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void Danghui()
    {
        HUITipManager.Instance.Play();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Danghui, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void Huodong()
    {
        HUITipManager.Instance.Play();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Huodong, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void Shizheng()
    {
        HUITipManager.Instance.Play();
        PingNetAddress();
        if (i == 2)
        {
            HUITipManager.Instance.ShowTip("请检查网络设置");
        }
        else
        {
            HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Shizheng, finish: (selfWindow) =>
            {
                selfWindow.InitData();
            });
        }
    }
    public void Paiming()
    {
        HUITipManager.Instance.Play();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Paiming, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void Shapan()
    {
        HUITipManager.Instance.Play();
        //System.Diagnostics.Process.Start(Application.dataPath + "/Content/最新播控平台/播控平台.exe");
        System.Diagnostics.Process.Start(Application.dataPath + "/Content/VR党建展馆/VR党建展馆.exe");
    }
    public void Wel()
    {
        HUITipManager.Instance.Play();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Wel, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void Zuzhigz()
    {
        HUITipManager.Instance.Play();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Zuzhigz, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void Gsjs()
    {
        HUITipManager.Instance.Play();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Gsjs, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void Xxzl()
    {
        HUITipManager.Instance.Play();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Xxzl, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void xuanshi()
    {
        HUITipManager.Instance.Play();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_xuanshi, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void ShapanFree()
    {
        string strpath = Application.streamingAssetsPath + "/" + "企业展馆位置.txt";
        HUITipManager.Instance.Play();
        string path= File.ReadAllText(strpath);
        System.Diagnostics.Process.Start(path);
    }
}
