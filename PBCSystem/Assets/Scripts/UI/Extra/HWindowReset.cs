using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HWindowReset : MonoBehaviour
{
    int j = 0;
    public List<GameObject> _lists = new List<GameObject>();
    public List<GameObject> _list1 = new List<GameObject>();
    public void Additemy()
    {
       for(int i = 0; i < _lists.Count; i++)
       {

       }
        if (j >= 0)
        {
            if (j < 4)
            {
                _lists[j].gameObject.SetActive(true);
            }
        }
    }
}
