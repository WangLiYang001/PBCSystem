using SuperDog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    public int _featureID;
    [SerializeField]
    private Text _guid;
    public string _contentName = "VR党建展馆";
    public static bool _vertificationSuccess = false;
    IEnumerator Start()
    {
#if UNITY_EDITOR
        if (!_vertificationSuccess)
        {
            _guid.transform.parent.gameObject.SetActive(true);
            _guid.text = "验证成功";
            _vertificationSuccess = true;
            yield return new WaitForSeconds(1f);
        }
        _guid.transform.parent.gameObject.SetActive(false);
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Start, finish: (window) =>
        {

        });
#else
        //走加密狗鉴权
        if (!_vertificationSuccess)
        {
            DogStatus status = HSuperDog.Instance.CheckFeatureID(_featureID);
            if (status == DogStatus.StatusOk)
            {
                _guid.transform.parent.gameObject.SetActive(true);
                _guid.text = "验证成功";
                _vertificationSuccess = true;
                yield return new WaitForSeconds(1f);
            }
            else
            { 
                _guid.transform.parent.gameObject.SetActive(true);
                _guid.text = "加密狗验证失败";
                yield break;
            }
        }
        _guid.transform.parent.gameObject.SetActive(false);
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Start, finish: (window) =>
        {

        });
#endif


        yield break;
    }
}


