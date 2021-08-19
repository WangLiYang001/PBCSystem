using Paroxe.PdfRenderer;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HLoadPDFMrg1 : MonoBehaviour
{
    PDFViewer _pdfViewer;
    public string _path;
    public string _foldName;
    public GameObject _back;
    void OnEnable()
    {
        _pdfViewer = GetComponent<PDFViewer>();
        if (_pdfViewer != null)
        {
            _pdfViewer.LoadDocumentFromStreamingAssets(_path, _foldName,"");
        }
    }
    public void Exit()
    {
        _back.gameObject.SetActive(false);
    }
}
