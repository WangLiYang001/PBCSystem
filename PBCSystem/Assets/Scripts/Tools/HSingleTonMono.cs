using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSingleTonMono<T> :MonoBehaviour where T:MonoBehaviour {

    private static T _instance;
	public static T Instance {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
            }
            if (_instance == null)
            {
                GameObject go = new GameObject(typeof(T).ToString());
                _instance = go.AddComponent<T>();
            }
            return _instance;
        }
    }






}
