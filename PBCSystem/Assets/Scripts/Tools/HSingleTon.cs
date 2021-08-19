using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSingleTon<T> where T : class, new() { 

    private static T _instance;
    private static readonly object _locker = new object();

    public static T Instance
    {
        get
        {
            if (_instance == null)//节省性能
            {
                lock (_locker)//加锁创建
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                }
            }
            return _instance;
        }
    }
}
