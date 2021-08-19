using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIList : MonoBehaviour
{
    public int _maxNumberDataPerPage = 10;//一页多少数据
    private int _currentPage = 0;//当前页码
    private int mDataCount ;
    public UIInfiniteTable mInfiniteTable;

    private List<UIListItem> mListItem = new List<UIListItem>();
    void Start()
    {
        var datas = HLoginDataManager.Instance.GetCurrentPageDatas(_currentPage, _maxNumberDataPerPage);
        mInfiniteTable.onGetItemComponent = OnGetItemComponent;
        mInfiniteTable.onReposition = OnReposition;
        mDataCount = datas.Count;
        Refresh();
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
}
