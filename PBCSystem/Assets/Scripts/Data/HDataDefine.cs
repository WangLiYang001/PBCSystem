using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DataDefine
{

    [System.Serializable]
    public class HCustomersInfomation
    {
        public List<HCustomersDataItem> _list = new List<HCustomersDataItem>();
    }

    [System.Serializable]
    public class HCustomersDataItem
    {

        public string _dateTime;
        public string _key;
        public string _tel;
        public string _company;
    }

    [System.Serializable]
    public class HItem
    {

        public string _dateTime;
        public string _key;

    }
    

}
