using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUIWindowDtxz : HUIBase
{
    public GameObject _object;
    public GameObject _object1;
    public Text _Title;
    public List<Button> buttons = new List<Button>();
    public override void InitData(bool forceInit = false)
    {
        AddClickEvents();
        if (_object1.activeSelf == true)
        {
            _object1.SetActive(true);
            _object.SetActive(false);
        }
        else
        {
            _object1.SetActive(false);
            _object.SetActive(true);
        }
        string _Ftitle = Application.streamingAssetsPath + "/党建答题/自定义题库/名称.txt";
        WWW _ftitle = new WWW(_Ftitle);
        _Title.text = _ftitle.text;
    }
    void AddClickEvents()
    {
        int x = 0;
        foreach (Button item in buttons)
        {
            int y = x;//不能直接使用X,需要Y转一下
            item.onClick.AddListener(() => ClickEvent2(y));
            x++;
        }
    }
    void ClickEvent2(int a)
    {
        //Debug.Log(a);
        switch (buttons[a].name)
        {
            case "dzdg":
                OpenQuestion(HDataManager.EQuestionBank.DGST);
                break;
            case "dzdg1":
                OpenQuestion(HDataManager.EQuestionBank.DSST);
                break;
            case "dzdg2":
                OpenQuestion(HDataManager.EQuestionBank.SJDST);
                break;
            case "dzdg3":
                OpenQuestion(HDataManager.EQuestionBank.DYST);
                break;
            case "timuheji1":
                OpenQuestion(HDataManager.EQuestionBank.DSG);
                break;
            case "timuheji2":
                OpenQuestion(HDataManager.EQuestionBank.XJPZYJH);
                break;
            case "timuheji3":
                OpenQuestion(HDataManager.EQuestionBank.SJDZG);
                break;
            case "timuheji4":
                OpenQuestion(HDataManager.EQuestionBank.LZJYG);
                break;
            case "timuheji5":
                OpenQuestion(HDataManager.EQuestionBank.YBZN);
                break;
            case "timuheji6":
                OpenQuestion(HDataManager.EQuestionBank.KRZZ);
                break;
            case "timuheji7":
                OpenQuestion(HDataManager.EQuestionBank.GGKFG);
                break;
            case "timuheji8":
                OpenQuestion(HDataManager.EQuestionBank.XSD);
                break;
            case "timuheji9":
                OpenQuestion(HDataManager.EQuestionBank.JFZZ);
                break;
            case "timuheji10":
                OpenQuestion(HDataManager.EQuestionBank.DSST1);
                break;
            default:
                break;
        }
        
    }
    public void Exit()
    {
        HUITipManager.Instance.PlayExit();
        if (_object.activeSelf == false)
        {
            _object.SetActive(true);
            _object1.SetActive(false);
        }
        else
        {
            HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Djzs, finish: (selfWindow) =>
            {
                selfWindow.InitData();
            });
        }
    }
    public void OpenQuestion(HDataManager.EQuestionBank e)
    {
        HUITipManager.Instance.PlayQue();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Dyjy, finish: (selfWindow) =>
            {
                selfWindow.InitData();
                HUIWindowDyjy uiWindowDyjy = (HUIWindowDyjy)selfWindow;
                uiWindowDyjy.InitQuestionBack(e);
            });

    }
    public void Clicktiku()
    {
        HUITipManager.Instance.PlayQue();
        _object.SetActive(false);
        _object1.SetActive(true);
    }
}
