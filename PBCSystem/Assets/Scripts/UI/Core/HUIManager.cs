using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HUIManager : HSingleTonMono<HUIManager>
{
    private Dictionary<string, HUIBase> _uiAll = new Dictionary<string, HUIBase>();
    private HUIBase _currentWindow;
    private Stack<HUIBase> _uiStacks = new Stack<HUIBase>();
    private bool _init = false;
    [SerializeField]
    private GameObject _windowRoot;
    private GameObject _top;
    private GameObject _bottom;
    public bool LoadFinished { get; private set; }

    public void Init()
    {
        _init = true;
        _windowRoot = FindObjectOfType<Canvas>().rootCanvas.gameObject;
        _top = GameObject.Find("top");
        _bottom = GameObject.Find("bottom");
        LoadFinished = false;
      //  StartCoroutine(InitPreAll());
    }
    public void HideTop()
    {
        if (_top != null)
            _top.SetActive(false);
    }
    public void ShowTop()
    {
        if (_top != null)
            _top.SetActive(true);
    }
    public void OpenUI(string uiPath,bool hideCurrent = true,Action<HUIBase> finish = null)
    {
        if (!_init)
            Init();
        
        HUIBase uIBase = null;
        if (_uiAll.ContainsKey(uiPath))
        {
            uIBase = _uiAll[uiPath]; 
        }
        else
        {
            uIBase = NewUI(uiPath);
            if (uIBase)
                _uiAll.Add(uiPath, uIBase);
            //Debug.Log("NewUI ：uiPath：" + uiPath);
        }
        if (uIBase!=null)
        {
            
            ShowUI(uIBase);
            if (_currentWindow != null && _currentWindow!= uIBase)
            {
                _uiStacks.Push(_currentWindow);
                if (hideCurrent)
                    HideUI(_currentWindow);
            }
            //该变当前前置窗口
            _currentWindow = uIBase;
        }
        
        PrintUIPath(uiPath, uIBase._log);
        if (finish != null)
            finish(uIBase);
    }


    public void Back(string uiPath)
    {
        if (!_uiAll.ContainsKey(uiPath))
        {
            return;
        }
        _uiAll[uiPath].gameObject.SetActive(false);
        if (_uiStacks.Count > 0)
        {
            HUIBase uIBase = _uiStacks.Pop();
            if (uIBase != null)
            {
                ShowUI(uIBase);
                //该变当前前置窗口
                _currentWindow = uIBase;
                //Debug.Log("Back");
                PrintUIPath(uIBase._windowPath, uIBase._log);
            }
            
        }
       
    }



    void PrintUIPath(string uiPath,bool log)
    {
        if (log)
            Debug.Log(uiPath);
    }


    HUIBase NewUI(string uiPath)
    {
       // Debug.Log(uiPath);
        GameObject go = Instantiate(Resources.Load(uiPath), _windowRoot.transform) as GameObject;
        HUIBase uIBase = go.GetComponent<HUIBase>();
        go.name = uiPath;
        uIBase._windowPath = uiPath;
        return uIBase;
    }




    void ShowUI(HUIBase uIBase)
    {
        uIBase.gameObject.SetActive(true);
        uIBase.BringToFront();
        if (_top!=null)
            _top.SetActive(uIBase._hasTop);
        if (_bottom != null)
            _bottom.SetActive(uIBase._hasBottom);

        if (_uiStacks.Count > 0)
        {
            HUIBase uIBaseInStackTop = _uiStacks.Peek();
            if (uIBaseInStackTop == uIBase)
                _uiStacks.Pop();
        }
    }

    void HideUI(HUIBase uIBase)
    {
        uIBase.gameObject.SetActive(false);
        
    }


    string GetFilePath()
    {
        
        return "data/preloadObjs";
    }

    IEnumerator InitPreAll()
    {
        if (string.IsNullOrEmpty(GetFilePath()))
        {
            Debug.Log("预加载资源路径错误：" + GetFilePath());
            yield break;
        }
        TextAsset textAsset = Resources.Load(GetFilePath()) as TextAsset;
        if (textAsset == null)
        {
            Debug.Log("预加载资源路径错误：" + GetFilePath());
            yield break;
        }
        
        string[] preObjectPahtList = textAsset.text.Trim().Split('\n');
        Debug.Log("预加载对象：" + preObjectPahtList.Length);

        _uiAll.Clear();
        
        yield return StartCoroutine(PreLoadObject(preObjectPahtList));

        yield return new WaitForSeconds(1);
        LoadFinished = true;
        Debug.Log("完成UI资源加载");
    }

    /*预加载对象
    */

    IEnumerator PreLoadObject(string[] preObjectPahtList)
    {
        foreach (string uiPath in preObjectPahtList)
        {
            HUIBase uIBase = NewUI(uiPath.Trim());
            if (uIBase != null)
            {
                _uiAll.Add(uiPath, uIBase);
                uIBase.gameObject.SetActive(false);
            }
            yield return -1;
        }
        yield break;
    }


    public void ResetUIState(string uiPath)
    {
        if (!_uiAll.ContainsKey(uiPath))
        {
            return;
        }
        _uiAll[uiPath].ResetUIState();
    }
    void OnButtonCLick()
    {
        HUIManager.Instance.OpenUI("UI/Window/UIWindow_Login");

    }
}
