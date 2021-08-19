using System;
using Excel;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using OfficeOpenXml;

public class HUIWindowData : HUIBase
{
    public Text _name;
    public Text _tel;
    public Text _sex;
    public Text _id;
    public Text _userid;
    public Text _curtime;
    public GameObject _del;
    public GameObject _reSet;
    public Text _password;
    public InputField _repassword;
    public InputField _enrepassword;
    string _userid1;
    string password;
    public string message;
    private int _maxNumberDataPerPage=11;//一页多少数据
    private int _currentPage = 0;//当前页码
    private int mDataCount;
    string _path;
    public UIInfiniteTable mInfiniteTable;
    private List<HTextConfig1> mListItem = new List<HTextConfig1>();
    public override void InitData(bool forceInit = false)
    {
        var data = HLoginDataManager.Instance.GetCurrentPageDatas(_currentPage, _maxNumberDataPerPage);
        StartCoroutine(Wait(0.5f));
        Refresh1();
        mDataCount = data.Count;
        _maxNumberDataPerPage = mDataCount;
    }
    IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);
        ResfreshUI();
    }
    private void Refresh1()
    {
        var data = HLoginDataManager.Instance.GetCurrentPageDatas(_currentPage, _maxNumberDataPerPage);
        mInfiniteTable.onGetItemComponent = OnGetItemComponent;
        mInfiniteTable.onReposition = OnReposition;
        mDataCount = data.Count;
        Refresh();
    }
    private void ResfreshUI()
    {
        
        List<HLoginDataManager.HFunction> datas = HLoginDataManager.Instance.GetCurrentPageDatas(_currentPage, _maxNumberDataPerPage);
        for (int i = 0; i < datas.Count; i++)
        {
            HTextConfig1 dataUI = mListItem[i];
            dataUI.Init(this, datas[i]._userid);
        }
    }
    //获取列表 item身上的脚本
    private void OnGetItemComponent(GameObject obj)
    {
        mListItem.Add(obj.GetComponent<HTextConfig1>());
    }

    //刷新UI 回调 -- 用来更新数据
    private void OnReposition(GameObject go, int dataIndex, int childIndex)
    {
        mListItem[childIndex].Refresh(dataIndex);
    }

    private void Refresh()
    {
        //赋值数据总数
        mInfiniteTable.TableDataCount = mDataCount;
        //刷新UI
        mInfiniteTable.RefreshData();
    }
    public void ClickDlete(string userID)//删除键控制
    {
        _del.SetActive(true);
        DleteDatas(userID);
    }
    public void DleteDatas(string userID)
    {
        HLoginDataManager.HFunction info = HLoginDataManager.Instance.FindUserInfo(userID);

        _name.text = info._name;
        _tel.text = info._tel;
        _sex.text = info._sex;
        _id.text = info._id;
        _userid.text = info._userid;
        _curtime.text = info._curtime;
    }
    public void ClickSet(string userID)//删除键控制
    {
          ResetDatas(userID);
        _reSet.SetActive(true);
    }
    public override void Init(bool forceInit = false)//初始化UI
    {

    }
    public void Return()
    {
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Paiming, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void ClickEn()
    {
        bool b = HLoginDataManager.Instance.DelUserInfo(_userid.text);
        if (b == true)
        {
            HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Data, finish: (selfWindow) =>
            {
                selfWindow.InitData();
            });
            _del.SetActive(false);
        }
       
    }
    public void ClickExit()
    {
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Data, finish: (selfWindow) =>
        {
            selfWindow.InitData();
        });
        HUITipManager.Instance.PlayExit();
        _del.SetActive(false);
    }
    public void ResetDatas(string userID)
    {
        HLoginDataManager.HFunction info = HLoginDataManager.Instance.FindUserInfo(userID);
        _userid1 = info._userid;
        _password.text = info._password;
        
    }
    public void ClickExit1()
    {
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Data, finish: (selfWindow) =>
        {
            selfWindow.InitData();
        });
        _reSet.SetActive(false);
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
            bool b = HLoginDataManager.Instance.SetPassWord(_userid1, password, out message);
            HUITipManager.Instance.ShowTip(message);
            if (b == true)
            {
                HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Data, finish: (selfWindow) =>
                {
                    selfWindow.InitData();
                });
                _reSet.SetActive(false);
            }
        }
        else
        {
            HUITipManager.Instance.ShowTip("两次输入不一样");
        }
    }
    public void OutPutInfo()
    {
        List<HLoginDataManager.HFunction> datas = HLoginDataManager.Instance.GetCurrentPageDatas(_currentPage, _maxNumberDataPerPage);
            string _filePath = Application.streamingAssetsPath + "/人员信息/人员信息.xlsx";
            string _sheetName = "详情";
            FileInfo _excelName = new FileInfo(_filePath);
            //通过ExcelPackage打开文件
            if (_excelName.Exists)
            {    //删除旧文件，并创建一个新的 excel 文件。
                 _excelName.Delete();
                 _excelName = new FileInfo(_filePath);
            }
        using (ExcelPackage package = new ExcelPackage(_excelName))
            {
                //在 excel 空文件添加新 sheet，并设置名称。
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(_sheetName);
                worksheet.Cells[1, 1].Value = "姓名";
                worksheet.Cells[1, 2].Value = "性别";
                worksheet.Cells[1, 3].Value = "电话号码";

                worksheet.Cells[1, 4].Value = "身份证号";
                worksheet.Cells[1, 5].Value = "账号";
                worksheet.Cells[1, 6].Value = "分数";
                worksheet.Cells[1, 7].Value = "登陆时间";
            for (int i = 0; i < datas.Count; i++)
            {
                worksheet.Cells[i+2, 1].Value = datas[i]._name;
                worksheet.Cells[i + 2, 2].Value = datas[i]._sex;
                worksheet.Cells[i + 2, 3].Value = datas[i]._tel;
                worksheet.Cells[i + 2, 4].Value = datas[i]._id;
                worksheet.Cells[i + 2, 5].Value = datas[i]._userid;
                worksheet.Cells[i + 2, 6].Value = datas[i]._grade;
                worksheet.Cells[i + 2, 7].Value = datas[i]._curtime;
            }
                package.Save();
            }
    }
}



