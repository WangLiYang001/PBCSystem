using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paroxe.PdfRenderer;
using System.IO;
using Utils;
public class HLoadPDFMrg : MonoBehaviour
{
    PDFViewer _pdfViewer;
    public string _path;
    public GameObject _back;
    void OnEnable()
    {
        _pdfViewer = GetComponent<PDFViewer>();
        if(_pdfViewer != null)
        {
            string path = Application.streamingAssetsPath + Path.DirectorySeparatorChar + _path;
            _pdfViewer.LoadDocumentWithBuffer(HAES.DecryptAESFileToBytes(path),"");
        }
    }
    public void Exit()
    {
        _back.gameObject.SetActive(false);
        GetGrade();
    }
    public void GetGrade()
    {
            if (_pdfViewer.GetGrade == true)
            {
                bool isAdd = true;
                string path = Application.streamingAssetsPath + "/积分系统/积分系统得分.txt";
                string content = File.ReadAllText(path);
                string[] flies = content.Split('.');
                string str = flies[2];
                string[] str1 = str.Split(':');
                string str2 = str1[1];
                int _grade = int.Parse(str2);
                HLoginDataManager.Instance.SetGrade(_grade, isAdd);
            }
        
    }
}
