using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Utils;
public class HDataManager : HSingleTonMono<HDataManager>
{
    public enum EQuestionType
    {
        Single=1,
        multiple=2
    }

    public enum EQuestionBank    //题库
    {
        DSST = 1,
        DYST,
        DGST,
        SJDST,
        DSG,
        JFZZ,
        LZJYG,
        XJPZYJH,
        XSD,
        SJDZG,
        GGKFG,
        YBZN,
        KRZZ,
        DSST1
    }
    public int _dgrade;

    [System.Serializable]
    public class HQuestionInfo
    {
        public string _questionDescription;
        public string _chooseA;
        public string _A;
        public string _chooseB;
        public string _B;
        public string _chooseC;
        public string _C;
        public string _chooseD;
        public string _D;
        public string _answer;
        public EQuestionType _status = EQuestionType.Single;
        public HQuestionInfo()
        {
            _questionDescription ="";
            _chooseA="";
            _A = "";
            _chooseB = "";
            _B = "";
            _chooseC = "";
            _C = "";
            _chooseD = "";
            _D = "";
            _answer = "";
        }
    }

    [System.Serializable]
    public class HQuestionBank
    {
        public EQuestionBank _eQuestionBank = EQuestionBank.DGST;
        public string _strQuestContent = "";

        public HQuestionBank(EQuestionBank e,string strContent)
        {
            _eQuestionBank = e;
            _strQuestContent = strContent;
        }
    }

    public List<HQuestionBank> _hQuestionBanks = new List<HQuestionBank>(); //存储题库列表

    public HQuestionBank FindHQuestionBank(EQuestionBank e)
    {
        foreach (HQuestionBank item in _hQuestionBanks)
        {
            if (item._eQuestionBank == e)
                return item;
        }

        return null;
    }

    public void Init()
    {
        DontDestroyOnLoad(gameObject);     
    }


    HQuestionBank GetQuestionBank(EQuestionBank e)
    {
        //Debug.Log("读取题库文件" + e + "每次打开UI只调用一次");

        HQuestionBank item = FindHQuestionBank(e);

        if (item != null)
            return item;

        string path = "";
        switch(e)
        {
            case EQuestionBank.DGST:
                {
                    path = Application.streamingAssetsPath + "/党建答题/党规试题.txt";
                    break;
                }
            case EQuestionBank.DSST:
                {
                    path = Application.streamingAssetsPath + "/党建答题/党史试题.txt";
                    break;
                }
            case EQuestionBank.DYST:
                {
                    path = Application.streamingAssetsPath + "/党建答题/党员教育.txt";
                    break;
                }
            case EQuestionBank.SJDST:
                {
                    path = Application.streamingAssetsPath + "/党建答题/十九大试题.txt";
                    break;
                }
            case EQuestionBank.DSG:
                {
                    path = Application.streamingAssetsPath + "/党建答题/党史馆试题.txt";
                    break;
                }
            case EQuestionBank.XJPZYJH:
                {
                    path = Application.streamingAssetsPath + "/党建答题/习近平重要讲话.txt";
                    break;
                }
            case EQuestionBank.SJDZG:
                {
                    path = Application.streamingAssetsPath + "/党建答题/十九大展馆.txt";
                    break;
                }
            case EQuestionBank.LZJYG:
                {
                    path = Application.streamingAssetsPath + "/党建答题/廉政教育馆.txt";
                    break;
                }
            case EQuestionBank.YBZN:
                {
                    path = Application.streamingAssetsPath + "/党建答题/建党100周年展馆.txt";
                    break;
                }
            case EQuestionBank.KRZZ:
                {
                    path = Application.streamingAssetsPath + "/党建答题/抗日战争纪念馆.txt";
                    break;
                }
            case EQuestionBank.GGKFG:
                {
                    path = Application.streamingAssetsPath + "/党建答题/改革开放展馆.txt";
                    break;
                }
            case EQuestionBank.XSD:
                {
                    path = Application.streamingAssetsPath + "/党建答题/新时代成就展馆.txt";
                    break;
                }
            case EQuestionBank.JFZZ:
                {
                    path = Application.streamingAssetsPath + "/党建答题/解放战争纪念馆.txt";
                    break;
                }
            case EQuestionBank.DSST1:
                {
                    path = Application.streamingAssetsPath + "/党建答题/党史试题(1).txt";
                    break;
                }
        }
        string content;
        if (path== Application.streamingAssetsPath + "/党建答题/党史试题(1).txt")
        {
             content= File.ReadAllText(path);
        }
        else
        { 
         content = HAES.DecryptAESFileToString(path);//File.ReadAllText(path);
        }
        HQuestionBank itemQuestionBank = new HQuestionBank(e, content);
        _hQuestionBanks.Add(itemQuestionBank);
        return itemQuestionBank;
    }
    public List<HQuestionInfo> GetPageDatas(EQuestionBank e, int maxNumber)
    {
        List<HQuestionInfo> questlist = new List<HQuestionInfo>();
        //Debug.Log("解析题库文件" + maxNumber + "每次打开UI只调用一次");
        HQuestionBank questionBank =  GetQuestionBank(e);
        if (questionBank == null)
            return questlist;

        string[] question1 = questionBank._strQuestContent.Split("#"[0]);
        string str1 = question1[1];
        string[] str = str1.Split("&"[0]);
        List<string> list = new List<string>(str);
        for (int i = 0; i < list.Count; i++)
        {
                if (string.IsNullOrEmpty(list[i]) == true)
                    continue;
                string[] select1 = list[i].Split(new string[] { "\r\n" },System.StringSplitOptions.RemoveEmptyEntries);
                if (select1.Length <= 0)
                    continue;
                HQuestionInfo info = new HQuestionInfo();
                info._questionDescription = select1[0];
                if(select1.Length >= 3)
                {
                    info._chooseA = "A";
                    info._A = select1[1].Trim().TrimStart("A.".ToCharArray());
                }
                if (select1.Length >= 4)
                {
                    info._chooseB = "B";
                    info._B = select1[2].Trim().TrimStart("B.".ToCharArray()); ;
                }

                if(select1.Length >= 5)
                {
                    info._chooseC = "C";
                    info._C = select1[3].Trim().TrimStart("C.".ToCharArray()); ;
                }
                if(select1.Length >= 6)
                {
                    info._chooseD = "D";
                    info._D = select1[4].Trim().TrimStart("D.".ToCharArray()); ;
                }
                info._answer = select1[select1.Length-1];
                if (info._answer.Length > 1)
                    info._status = EQuestionType.multiple;
                else
                    info._status = EQuestionType.Single;
                questlist.Add(info);
        }
        if (questlist.Count <= 0)
        {
            Debug.LogError("解析题库列表为空");
            return questlist; }
        //如果有数据，那么当前页最小下标和最大下标

        List<HQuestionInfo> tempData = new List<HQuestionInfo>();
        if (maxNumber > questlist.Count)
        {
            for(int i=0; i< questlist.Count;i++)
            {
                tempData.Add(questlist[i]);
            }

            return tempData;
        }
        else
        {
            for(int i = 0; i<maxNumber;i++)
            {
                int index = Random.Range(0, questlist.Count - 1);
                tempData.Add(questlist[index]);
                questlist.RemoveAt(index);
            }
        }
        return tempData;
       
    }
}