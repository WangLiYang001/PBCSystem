using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUIWindowPaiming : HUIBase
{
    private int _maxNumberDataPerPage=11;//一页多少数据
    private int _currentPage = 0;//当前页码
    public GameObject _obj;
    private int mDataCount;
    public UIInfiniteTable mInfiniteTable;
    private List<UIListItem> mListItem = new List<UIListItem>();

    public override void InitData(bool forceInit = false)
    {
        var datas = HLoginDataManager.Instance.GetCurrentPageDatas(_currentPage, _maxNumberDataPerPage);
        ResfreshUI();
        Refresh();
        mDataCount = datas.Count;
        _maxNumberDataPerPage = mDataCount;//一页多少数据
    }
    private void ResfreshUI()
    {
        var datas = HLoginDataManager .Instance.GetCurrentPageDatas(_currentPage, _maxNumberDataPerPage);
        bool b = HLoginDataManager.Instance.IsAdmin;
        mInfiniteTable.onGetItemComponent = OnGetItemComponent;
        mInfiniteTable.onReposition = OnReposition;
        mDataCount = datas.Count;
        Refresh();
        if (b == false)
        {
            _obj.SetActive(false);
        }
        else
        {
            _obj.SetActive(true);
        }    
    }
    //获取列表 item身上的脚本
    private void OnGetItemComponent(GameObject obj)
    {
        mListItem.Add(obj.GetComponent<UIListItem>());
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
    public override void Init(bool forceInit = false)//初始化UI
    {

    }
    public void Return()
    {
        HUITipManager.Instance.PlayExit();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Main, finish: (selfWindow) =>
        {
            selfWindow.InitData();
        });
    }
    public void Data()
    {
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Data, finish: (selfWindow) =>
        {
            selfWindow.InitData();
        });
    }
}
