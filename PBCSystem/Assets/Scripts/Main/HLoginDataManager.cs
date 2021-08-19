using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;
using System.Text.RegularExpressions;

public class HLoginDataManager : HSingleTonMono<HLoginDataManager>
{
    string _userInfoPath;
    HFunction _currUserInfo = null;
    public HFunction CurrUserInfo
    {
        get { return _currUserInfo; }
    }
    HFunction info;
    //电信手机号码正则  
    //中国电信133.153.177.180.181.189      
    private static string dianxin = @"^1(3[3]|5[3]|7[7]|8[019])\d{8}$";
    Regex dianxinReg = new Regex(dianxin);
    //联通手机号正则 
    //中国联 通130.131.132.155.156.185.186 .145.176       
    private static string liantong = @"^1(3[0-2]|4[5]|7[6]|5[56]|8[56])\d{8}$";
    Regex liantongReg = new Regex(liantong);
    //移动手机号正则 
    //中国移动 134.135.136.137.138.139.150.151.152.157.158.159.182.183.184.187.188 ,147(数据卡)
    private static string yidong = @"^1(3[4-9]|4[7]|5[012789]|8[3478])\d{8}$";
    Regex yidongReg = new Regex(yidong);
    [System.Serializable]
    public class HFunction//数据中的一个小单位
    {
        public string _name;
        public string _sex;
        public string _tel;
        public string _id;
        public string _curtime;
        public string _userid;
        public string _password;
        public int _grade;
    }

    string _adminUserID;
    public string AdminUserID
    {
        get { return _adminUserID; }
    }
    string _adminPassWord;
    public string AdminPassWord
    {
        get { return _adminPassWord; }
    }

    bool _isAdmin = false;
    public bool IsAdmin
    {
        get { return _isAdmin; }
    }

    public List<HFunction> _userInfoList = new List<HFunction>();

    public void Init()
    {
        DontDestroyOnLoad(gameObject);
        _userInfoPath = Application.persistentDataPath + "/userinfo/";
        LoadUserInfos();
        LoadAdminInfo();
    }

    public bool Register(string name, string tel, string id, string sex, string userid, string password, out string message)
    {

        if (string.IsNullOrEmpty(name))
        {
            message = "用户名为空";
            return false;
        }

        if (name.Equals(_adminUserID) == true)
        {
            message = "账号重复，请更换账号(1)";
            return false;
        }

        if (FindUserInfo(userid) != null)
        {
            message = "账号重复，请更换账号(2)";
            return false;
        }
        if (userid.Length < 3)
        {
            message = "账号必须3位以上";
            return false;
        }
        if (IsHasCHZN(password) ==true)
        {
            message = "密码设置错误，不能包含中文";
            return false;
        }
        if (password.Length < 6)
        {
            message = "密码必须6位以上";
            return false;
        }
        if (IsHasCHZN(tel) == true)
        {
            message = "电话号设置，不能包含非数字字符";
            return false;
        }
        if (tel.Trim() != null)
        {
            if (dianxinReg.IsMatch(tel) || liantongReg.IsMatch(tel) || yidongReg.IsMatch(tel))
            {

            }
            else
            {
                message = "电话号码不合法";
                return false;
            }
        }
        if (tel.Length != 11)
        {
                message = "电话号码验证不通过，原因：位数不够！";
                return false;
        }
      
        if (IsHasCHZN(id) == false)
        {
            if (id.Length == 18)
            {
                //将所输入的身份证号码进行拆分，拆分为17位，最后一位留着待用
                int Num0 = int.Parse(id.Substring(0, 1)) * 7;
                int Num1 = int.Parse(id.Substring(1, 1)) * 9;
                int Num2 = int.Parse(id.Substring(2, 1)) * 10;
                int Num3 = int.Parse(id.Substring(3, 1)) * 5;
                int Num4 = int.Parse(id.Substring(4, 1)) * 8;
                int Num5 = int.Parse(id.Substring(5, 1)) * 4;
                int Num6 = int.Parse(id.Substring(6, 1)) * 2;
                int Num7 = int.Parse(id.Substring(7, 1)) * 1;
                int Num8 = int.Parse(id.Substring(8, 1)) * 6;
                int Num9 = int.Parse(id.Substring(9, 1)) * 3;
                int Num10 = int.Parse(id.Substring(10, 1)) * 7;
                int Num11 = int.Parse(id.Substring(11, 1)) * 9;
                int Num12 = int.Parse(id.Substring(12, 1)) * 10;
                int Num13 = int.Parse(id.Substring(13, 1)) * 5;
                int Num14 = int.Parse(id.Substring(14, 1)) * 8;
                int Num15 = int.Parse(id.Substring(15, 1)) * 4;
                int Num16 = int.Parse(id.Substring(16, 1)) * 2;
                int allNum = Num0 + Num1 + Num2 + Num3 + Num4 + Num5 + Num6 + Num7 + Num8 + Num9 + Num10 + Num11 + Num12 + Num13 + Num14 + Num15 + Num16;
                int Remainder = allNum % 11;
                //如果最后一位数不是X的时候将最后一位数转换为int
                string LastNum= id.Substring(17, 1);
                if (Remainder == 0)
                {
                    if (int.Parse(LastNum) == 1)
                    {
                      message= "恭喜验证通过";
                    }
                    else
                    {
                        message = "您的身份证号不合法";
                        return false;
                    }
                }
                if (Remainder == 1)
                {
                    if (int.Parse(LastNum) == 0)
                    {
                        message = "恭喜验证通过";
                    }
                    else
                    {
                        message = "您的身份证号不合法";
                        return false;
                    }
                }
                if (Remainder == 2)
                {
                    if (LastNum != "x" && LastNum != "X")
                    {
                        message = "您的身份证号不合法";
                        return false;
                    }
                    else
                    {
                        message = "恭喜验证通过";
                    }
                }
                if (Remainder == 3)
                {
                    if (int.Parse(LastNum) == 9)
                    {
                        message = "恭喜验证通过";
                    }
                    else
                    {
                        message = "您的身份证号不合法";
                        return false;
                    }
                }
                if (Remainder == 4)
                {
                    if (int.Parse(LastNum) == 8)
                    {
                        message = "恭喜验证通过";
                    }
                    else
                    {
                        message = "您的身份证号不合法";
                        return false;
                    }
                }
                if (Remainder == 5)
                {
                    if (int.Parse(LastNum) == 7)
                    {
                        message = "恭喜验证通过";
                    }
                    else
                    {
                        message = "您的身份证号不合法";
                        return false;
                    }
                }
                if (Remainder == 6)
                {
                    if (int.Parse(LastNum) == 6)
                    {
                       message="恭喜验证通过";
                    }
                    else
                    {
                        message = "您的身份证号不合法";
                        return false;
                    }
                }
                if (Remainder == 7)
                {
                    if (int.Parse(LastNum) == 5)
                    {
                        message = "恭喜验证通过";
                    }
                    else
                    {
                       message="您的身份证号不合法";
                        return false;
                    }
                }
                if (Remainder == 8)
                {
                    if (int.Parse(LastNum) == 4)
                    {
                        message = "恭喜验证通过";
                    }
                    else
                    {
                        message = "您的身份证号不合法";
                        return false;
                    }
                }
                if (Remainder == 9)
                {
                    if (int.Parse(LastNum) == 3)
                    {
                        message = "恭喜验证通过";
                    }
                    else
                    {
                        message = "您的身份证号不合法";
                        return false;
                    }
                }
                if (Remainder == 10)
                {
                    if (int.Parse(LastNum) == 2)
                    {
                        message = "恭喜验证通过";
                    }
                    else
                    {
                        message = "您的身份证号不合法";
                        return false;
                    }
                }

            }
            else
            {
                message = "身份证验证不通过，原因：身份证位数不够！";
                return false;
            }
        }
        else
        {
            message = "身份证添加失败，请输入正确的格式";
            return false;

        }
        if (!CheckStringValid(name))
        {
            message = "请输入姓名";
            return false;
        }

        else if (!CheckStringValid(password))
        {
            message = "请输入密码";
            return false;
        }
        else if (!CheckStringValid(userid))
        {
            message = "请输入账号";
            return false;
        }
        DateTime dt = DateTime.Now;
        string Dt = dt.ToString();
        HFunction function = new HFunction();
        function._curtime = Dt;
        function._name = name;
        function._sex = sex;
        function._id = id;
        function._tel = tel;
        function._userid = userid;
        function._password = password;
        _currUserInfo=function;
        _userInfoList.Add(function);
        SaveUserInfos(userid);
        message = "注册成功";
        return true;
    }
    public static bool IsHasCHZN(string str)
    {

        Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
        Match m = RegCHZN.Match(str);
        return m.Success;
    }
    public bool Login(string userID,string password,out string message)
    {
        if (!CheckStringValid(userID))
        {
            message="用户名不能为空";
            return false;
        }
        if (!CheckStringValid(password))
        {
            message="密码不能为空";
            return false;
        }
        if (userID.Equals(_adminUserID) == true)
        {
            if (_adminPassWord.Equals(password) == true)
            {
                _isAdmin = true;
                message = "管理员登陆成功";
                HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Main, finish: (selfWindow) => {
                    selfWindow.InitData();
                });
                return true;
            }
            else
            {
                message = "管理员密码错误";
                return false;
            }

        }
        info = FindUserInfo(userID);
        if(info == null)
        {
            message = "账号不存在";
            return false;
        }

        if(info._password.Equals(password) == false)
        {
            message = "密码错误";
            return false;
        }
        _isAdmin = false;
        _currUserInfo = info;
        _currUserInfo._curtime = DateTime.Now.ToString();
        SaveUserInfos(info._userid);
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Main, finish: (selfWindow) => {
            selfWindow.InitData();
        });
        message = "登陆成功";
        return true;
    }

    public bool SetPassWord(string userid, string newPassWord, out string massage)
    {
        if (_isAdmin == false)
        {
            massage = ("您没有管理员权限");
            return false;
        }
       info = FindUserInfo(userid);
        if (info == null)
        {
            massage = ("用户不存在");
            return false;
        }
        info._password = newPassWord;
        SaveUserInfos(userid);
        massage = ("密码修改成功");
        return true;
    }
    public bool DelUserInfo(string userID)
    {
        if (_isAdmin == false)
        {
            return false;
        }
        info = FindUserInfo(userID);
        if (info == null)
            return false;

        string path = _userInfoPath+ info._userid + ".json";
        Debug.Log(path);
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        _userInfoList.Remove(info);
        return true;
    }

    public void SetGrade(int grade, bool isAdd)
    {
        if(_isAdmin == false)
        {
            info = _currUserInfo;
            info._userid = _currUserInfo._userid;
            if (info != null)
            {
                if (isAdd)
                    info._grade += grade;
                else
                    info._grade = grade;
                
                SaveUserInfos(info._userid);
            }
        }
        
       
    }

    public HFunction FindUserInfo(string userID)
    {
        foreach (HFunction item in _userInfoList)
        {
            if (item._userid.Equals(userID))
                return item;
        }

        return null;
    }
    public void SaveUserInfos(string userID)
    {
        //Debug.Log("保存" + userID + "用户信息");
        if (Directory.Exists(_userInfoPath) == false)
        {
            //Debug.Log("创建文件夹成功:" + _userInfoPath);
            Directory.CreateDirectory(_userInfoPath);
        }
        if(info != null)
        {
            string str = JsonUtility.ToJson(info);

            string _jsonname = info._userid + ".json";
            string filePath = _userInfoPath + _jsonname;
            Debug.Log(_userInfoPath);
            //Debug.Log(filePath);
            FileInfo file = new FileInfo(filePath);
            StreamWriter sw = file.CreateText();
            sw.WriteLine(str);
            sw.Close();
            sw.Dispose();
        }
    }

    public void LoadAdminInfo()
    {
        _adminPassWord = "";
        _adminUserID = "";
        string fullPath = Application.streamingAssetsPath + "/admin.txt";

        int i = 0;
        foreach(string str in File.ReadLines(fullPath))
        {
            i++;
            if (i == 2)
                _adminPassWord = str;
            if (i == 1)
                _adminUserID = str;
        }
    }

    public void LoadUserInfos()
    {
        _userInfoList.Clear();
        if (Directory.Exists(_userInfoPath) == true)
        {
            DirectoryInfo direction = new DirectoryInfo(_userInfoPath);
            FileInfo[] files = direction.GetFiles("*.json", SearchOption.AllDirectories);

            for (int i = 0; i < files.Length; i++)
            {
                string Path = _userInfoPath + files[i].Name;
                string str = File.ReadAllText(Path);
                if(string.IsNullOrEmpty(str) == false)
                {
                    info = JsonUtility.FromJson<HFunction>(str);
                    _userInfoList.Add(info);
                }
                
            }
        }
    }
    public List<HFunction> GetCurrentPageDatas(int currentPage, int maxNumberDataPerPage)
    {
        LoadUserInfos();
        if (_userInfoList.Count == 0) return _userInfoList;
        //如果有数据，那么当前页最小下标和最大下标
        int min = currentPage * maxNumberDataPerPage;
        int max = min + maxNumberDataPerPage - 1;
        List<HFunction> tempData = new List<HFunction>();
        //有几种情况取不到
        if (min >= _userInfoList.Count)
        {
            return tempData;
        }
        else
        {
            max = ((max >= _userInfoList.Count) ? (_userInfoList.Count - 1) : max);

            tempData = _userInfoList.GetRange(min, max - min + 1);
        }
        return tempData;
    }

    bool CheckStringValid(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            return false;
        }
        return true;
    }
}




