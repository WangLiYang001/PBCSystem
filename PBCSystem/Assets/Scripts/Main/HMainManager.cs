using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMainManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        HLoginDataManager.Instance.Init();
        HDataManager.Instance.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
