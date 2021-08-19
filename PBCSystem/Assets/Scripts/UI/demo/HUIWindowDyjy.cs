using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class HUIWindowDyjy : HUIBase
{
    public Text _tihao;
    public Text question;
    public Text _answer;
    public Text _tips;
    public Text _daojishi;
    public GameObject _background;
    public Text _grades;
    public Text _txtTime;
    public Text _pingjia;
    public Color _defaultColor;
    public Color _rightColor;
    public Button _btn4;
    public GameObject title;
    public GameObject title1;
    public GameObject title2;
    public GameObject title3;
    int Count_Down = 60;
    int _count_down = 0;
    int _grade = 0;
    int _yesNumber = 0;

    public HTextConfig _textConfig;//表格列表 
    private int _currentPage = 0;//当前页码
    int MaxNumber;//最大题数 
    List<HDataManager.HQuestionInfo> _questionList = new List<HDataManager.HQuestionInfo>();

    public List<HUIQuestionInfo> _questionInfoList = new List<HUIQuestionInfo>();
    public override void InitData(bool forceInit = false)
    {
        _background.gameObject.SetActive(true);
        _answer.gameObject.SetActive(false);
        _tips.gameObject.SetActive(false);
        string path = Application.streamingAssetsPath + "/党建答题/最大题数.txt";
        string content = File.ReadAllText(path);
        MaxNumber = int.Parse(content);
        _questionList.Clear();
        _yesNumber = 0;
        _currentPage = 0;
        _grade = 0;
        _count_down = Count_Down;
        _tihao.text = "第" + (_currentPage+1).ToString() + "/" + MaxNumber.ToString() + "题";
        _daojishi.text = "" + _count_down;
        title.gameObject.SetActive(false);
        title1.gameObject.SetActive(false);
        title2.gameObject.SetActive(false);
        title3.gameObject.SetActive(false);
        CancelInvoke();
        InvokeRepeating("Time_count", 0f, 1F);
    }

    public void InitQuestionBack(HDataManager.EQuestionBank e)
    {
        if (e == HDataManager.EQuestionBank.DGST)
        {
            title.gameObject.SetActive(true);
        }
        else if (e == HDataManager.EQuestionBank.DSST)
        {
            title1.gameObject.SetActive(true);
        }
        else if (e == HDataManager.EQuestionBank.DYST)
        {
            title2.gameObject.SetActive(true);
        }
        else if (e == HDataManager.EQuestionBank.SJDST)
        {
            title3.gameObject.SetActive(true);
        }
        else
        {
            title2.gameObject.SetActive(true);
        }
        _questionList = HDataManager.Instance.GetPageDatas(e, MaxNumber);
        //Debug.Log(_questionList.Count);
        ResfreshUI(); 
    }

    void ResetAllButUI()
    {
        _answer.gameObject.SetActive(false);
        foreach(HUIQuestionInfo butInfo in _questionInfoList)
        {
            butInfo.NoSelectUI();
        }
    }

    HUIQuestionInfo GetQuestionInfo(GameObject obj)
    {
        HUIQuestionInfo uiQInfo = obj.GetComponent<HUIQuestionInfo>();
        foreach (HUIQuestionInfo butInfo in _questionInfoList)
        {
            if (uiQInfo == butInfo)
                return butInfo;
        }
        return null;
    }
    public void OnBtnClick(GameObject obj)
    {
        HDataManager.HQuestionInfo qInfo = _questionList[_currentPage];
        HUIQuestionInfo uiQInfo = obj.GetComponent<HUIQuestionInfo>();
        if (qInfo._status == HDataManager.EQuestionType.Single)
        {
            if(uiQInfo.BSelect)
            {
                uiQInfo.NoSelectUI();
            }
            else
            {
                ResetAllButUI();
                uiQInfo.SelectUI();
            }
        }
        else
        {
            if (uiQInfo.BSelect)
            {
                uiQInfo.NoSelectUI();
            }
            else
            {
                uiQInfo.SelectUI();
            }
        }
    }

    public void OnBtnClickOK()
    {
        HDataManager.HQuestionInfo qInfo = _questionList[_currentPage];
        _answer.gameObject.SetActive(true);
        if (qInfo._status == HDataManager.EQuestionType.Single)
        {
            HUIQuestionInfo selectButInfo = null;
            foreach (HUIQuestionInfo butInfo in _questionInfoList)
            {
                if(butInfo.BSelect)
                {
                    selectButInfo = butInfo;
                    break;
                }
            }
            if(selectButInfo == null)
            {
                HUITipManager.Instance.ShowImageNO();
            }
            else
            {
                if(qInfo._answer.Equals(selectButInfo.ID) == true)
                {
                    HUITipManager.Instance.ShowImageOK();
                    _yesNumber++;
                }
                else
                {
                    HUITipManager.Instance.ShowImageNO();
                }
            }
        }
        else
        {
            List<string>  selectList = new List<string>();
            foreach (HUIQuestionInfo butInfo in _questionInfoList)
            {
                if (butInfo.BSelect)
                {
                    selectList.Add(butInfo.ID);
                }
            }

            if (qInfo._answer.Length != selectList.Count)
            {
                HUITipManager.Instance.ShowImageNO();
            }
            else
            {
                bool isOK = true;
                foreach(string selcetID in selectList)
                {
                    if(!qInfo._answer.Contains(selcetID))
                    {
                        isOK = false;
                        break;
                    }
                }

                if(isOK)
                {
                    HUITipManager.Instance.ShowImageOK();
                    _yesNumber++;
                }
                else
                {
                    HUITipManager.Instance.ShowImageNO();
                }
            }
        }
        CancelInvoke();
        _tips.gameObject.SetActive(true);
        StartCoroutine(Wait(2f));
        _currentPage++;
        if (_currentPage >= MaxNumber)
        {
            string path = Application.streamingAssetsPath + "/积分系统/积分系统得分.txt";
            string content = File.ReadAllText(path);
            string[] flies = content.Split('.');
            string str = flies[0];
            string[] str1 = str.Split(':');
            string str2 = str1[1];
            int n = int.Parse(str2);
            _currentPage = 0;
            _grades.text = "您的得分为"+(_yesNumber * n).ToString();
            _grade = _yesNumber * n;
            CancelInvoke();
            GetGrade();
            StartCoroutine(Wait1(2f));             
        }
    }
    IEnumerator Wait1(float t)
    {
        yield return new WaitForSeconds(t);
        _background.gameObject.SetActive(false);
    }

    private void ResfreshUI()
    {
        if (_currentPage >= MaxNumber)
            return;       
        HDataManager.HQuestionInfo qInfo = _questionList[_currentPage];
        question.text = _questionList[_currentPage]._questionDescription;
        _answer.text = _questionList[_currentPage]._answer;
        if (string.IsNullOrEmpty(qInfo._A))
        {
            _questionInfoList[0].gameObject.SetActive(false);
        }
        else
        {
            _questionInfoList[0].gameObject.SetActive(true);
            _questionInfoList[0].Init(this, qInfo._chooseA, qInfo._A, qInfo._chooseA);
        }
        if (string.IsNullOrEmpty(qInfo._B))
        {
            _questionInfoList[1].gameObject.SetActive(false);
        }
        else
        {
            _questionInfoList[1].gameObject.SetActive(true);
            _questionInfoList[1].Init(this, qInfo._chooseB, qInfo._B, qInfo._chooseB);
        }
        if (string.IsNullOrEmpty(qInfo._C))
        {
            _questionInfoList[2].gameObject.SetActive(false);
        }
        else
        {
            _questionInfoList[2].gameObject.SetActive(true);
            _questionInfoList[2].Init(this, qInfo._chooseC, qInfo._C, qInfo._chooseC);
        }
        if (string.IsNullOrEmpty(qInfo._D))
        {
            _questionInfoList[3].gameObject.SetActive(false);
        }
        else
        {
            _questionInfoList[3].gameObject.SetActive(true);
            _questionInfoList[3].Init(this, qInfo._chooseD, qInfo._D, qInfo._chooseD);
        }

    }
    void Time_count()
    {
        if (_count_down > 0)
        {
            _count_down--;
            _daojishi.text = "" + _count_down;

        }
        else
        {
            CancelInvoke();
            OnBtnClickOK();
        }

    }

    public void Return()
    {
        CancelInvoke();
        HUITipManager.Instance.PlayExit();
        _background.gameObject.SetActive(true);
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Dtxz, finish: (selfWindow) =>
        {
            selfWindow.InitData();
        });
    }
    IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);
        _tips.gameObject.SetActive(false);
        _answer.gameObject.SetActive(false);
        _tihao.text = "第" + (_currentPage+1).ToString() + "/" + MaxNumber.ToString() + "题";
        if (_count_down > 0)
        {
            _count_down = Count_Down;
            InvokeRepeating("Time_count", 0.0f, 1.0F);
        }
        else if (_count_down <= 0)
        {
            _count_down = Count_Down;
            InvokeRepeating("Time_count", 0.0f, 1.0F);
        }

        ResfreshUI();
    }
    public void GetGrade()
    {
       
        DateTime NowTime = DateTime.Now.ToLocalTime();
        _txtTime.text = NowTime.ToString("yyyy/MM/dd -- HH:mm:ss");
        bool isAdd = true;
        CancelInvoke();
        HLoginDataManager.Instance.SetGrade(_grade,isAdd);        
            if (_grade < 60)
            {
                _pingjia.text = "成绩不理想，继续努力！";
            }
            else
            {
                _pingjia.text = "成绩合格,继续加油！";
            }
        StartCoroutine(Wait2(2f));
    }
    IEnumerator Wait2(float t)
    {
        yield return new WaitForSeconds(t);
        _background.gameObject.SetActive(false);
        StopAllCoroutines();
    }
}

