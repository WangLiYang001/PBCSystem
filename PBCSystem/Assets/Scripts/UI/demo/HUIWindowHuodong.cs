using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class HUIWindowHuodong : HUIBase 
{
    public GameObject _object;
    public GameObject GetObject;
    public Text _txtCurrentTime;
    public Text _txtCurrentType;
    public GameObject Object;
    public GameObject Object1;
    public GameObject Object2;
    public Text _userName;
    public Text _photoNum;
    public GameObject _photoAl;
    private string filePath;
    public GameObject _content;
    List<Texture2D> allTex2d = new List<Texture2D>();
    List<GameObject> team = new List<GameObject>();
    public override void InitData(bool forceInit = false)
    {
        GetFiles();
        _photoAl.SetActive(false);
        Object.gameObject.SetActive(false);
        if (HLoginDataManager.Instance.IsAdmin)
            _userName.text = "管理员：" + HLoginDataManager.Instance.AdminUserID;
        else
            _userName.text = HLoginDataManager.Instance.CurrUserInfo._name;
    }
    public void GetFiles()
    {     
        string path = Application.streamingAssetsPath + "/党员活动/活动相册";
        string[] dirs = Directory.GetDirectories(@path, "*");
        for (int i = 0; i < dirs.Length; i++)
        {
            _photoNum.text = dirs.Length.ToString()+"个相册";
            filePath = dirs[i];
            string fileName = Path.GetFileName(dirs[i]);
            GameObject go = Instantiate(_photoAl);
            go.name = fileName;
            go.GetComponent<RectTransform>().SetParent(_photoAl.transform.parent);
            if (i > 6)
            {
                go.GetComponent<RectTransform>().localPosition = new Vector3(250 * (i - 7), -250, 0);
            }
            go.GetComponent<RectTransform>().localPosition = new Vector3(250 * i,0,0);        
            go.GetComponent<RectTransform>().localScale = Vector3.one;
            team.Add(go);
        }
    }
    void Update()
    {
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
    public void Exit()
    {
        HUITipManager.Instance.PlayExit();
        if (Object.gameObject.activeSelf == true)
        {
            Object1.gameObject.SetActive(true);
            Object.gameObject.SetActive(false);
            GetObject.gameObject.SetActive(true);
        }
        else
        {         
            HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Main, finish: (selfWindow) =>
            {
                selfWindow.InitData();
            });
        }
        for (int i = 0; i < _content.transform.childCount; i++)
        {
            Destroy(_content.transform.GetChild(i).gameObject);
        }
        GetObject.gameObject.SetActive(true);
    }
    public void Click()
    {
        _object.gameObject.SetActive(false);
    }

    public void LoadPhoto()
    {
        Object1.gameObject.SetActive(false);
        Object.gameObject.SetActive(true);
    }
    public void LoadMovies()
    {
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Player, finish: (selfWindow) =>
        {
            selfWindow.InitData();
        });
    }
}
