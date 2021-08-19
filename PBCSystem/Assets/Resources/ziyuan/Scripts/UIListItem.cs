using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIListItem : MonoBehaviour
{
    public int _maxNumberDataPerPage = 10;//一页多少数据
    private int _currentPage = 0;//当前页码
    public Text _mId;
    public Text _userId;
    public Text _grades;
    public List<HFunction> _hFunctions = new List<HFunction>();
    [System.Serializable]
    public class HFunction
    {
        public string _userId;
        public int _grades;
    }

    //更新数据
    public void Refresh(int index)
    {
        var datas = HLoginDataManager.Instance.GetCurrentPageDatas(_currentPage, _maxNumberDataPerPage);
        _mId.text = (index + 1).ToString();
        if (datas.Count >= index)
        {
            for (int i = 0; i < datas.Count; i++)
            {
                _hFunctions[i]._userId = datas[i]._userid;
               
                _hFunctions[i]._grades = datas[i]._grade;
                _hFunctions.Sort(new ComparePersonByGrade());  //根据成绩大小对集合中所有元素进行一次排序
            }         
            for (int i = 0; i < datas.Count; i++)
            {
                _userId.text = _hFunctions[index]._userId;
                _grades.text = _hFunctions[index]._grades.ToString();
            }
            
        }
    }

    public class ComparePersonByGrade : IComparer<HFunction>
    {
        public int Compare(HFunction x, HFunction y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            if (x._grades < y._grades) return 1;
            if (x._grades > y._grades) return -1;
            return 0;
        }
    }
   
}
