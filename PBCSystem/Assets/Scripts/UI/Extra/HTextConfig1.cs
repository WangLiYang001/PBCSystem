using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HTextConfig1 : MonoBehaviour
{
    public Text _tx_name;
    public Text _sex;
    public Text _tel;
    public Text _id;
    public Text _curtime;
    public Text _userid;
    public Text _password;
    public GameObject _delBtn;
    public string _mId;
    public GameObject _setBtn;
    public int _maxNumberDataPerPage = 10;//一页多少数据
    private int _currentPage = 0;//当前页码
    private HUIWindowData _uiWindowsData;
    private string _userID;
    public void Init(HUIWindowData uiWindowData,string userID)
    {
        _userID = userID;
        _uiWindowsData = uiWindowData;
    }
    public void Refresh(int index)
    {
        var datas = HLoginDataManager.Instance.GetCurrentPageDatas(_currentPage, _maxNumberDataPerPage);
        _mId = (index + 1).ToString();
        if (datas.Count >= index)
        {
            _userid.text = datas[index]._userid;
            _tx_name.text = datas[index]._name;
            _sex.text = datas[index]._sex;
            _tel.text = datas[index]._tel;
            _id.text = datas[index]._id;
            _curtime.text = datas[index]._curtime;
            _password.text = datas[index]._password;
        }
    }

    public void OnBtn(GameObject obj)
    {
        if(obj == _delBtn)
        {
            _uiWindowsData.ClickDlete(_userID);
        }
        else if(obj == _setBtn)
        {
            _uiWindowsData.ClickSet(_userID);

        }
    }



}
