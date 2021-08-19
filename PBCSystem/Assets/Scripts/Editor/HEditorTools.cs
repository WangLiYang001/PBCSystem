using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using System.IO;
using System.Xml;
using UnityEngine.UI;

public class HEditorTools {

    class ObjectInfo
    {
        public string _packName;
        public object _packObj;
        public string _packPath;
        public string _prefabPath;
        public string _name;
    }

    [MenuItem("GameObject/Tools/去掉UI中的RayCastTarget", false,-1)]
    static public void CancelRayCastTarget()
    {
        GameObject[] objects = Selection.gameObjects; for (int index = 0; index < objects.Length; index++)
        {
            GameObject game = objects[index];
            if (game != null)
            {

                Graphic[] graphics = game.GetComponentsInChildren<Graphic>(true);
                if (graphics != null)
                {
                    for (int idx = 0; idx < graphics.Length; idx++)
                    {
                        graphics[idx].raycastTarget = false;
                    }
                }

            }
        }
    }
    [MenuItem("HTools/输出文件路径")]
    [MenuItem("Assets/输出文件路径")]
    static public void SelectOutputObjPath()
    {
        string outPath = "";
        foreach(Object obj in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            int index = path.IndexOf(".");
            path = path.Substring(0,index);
            path = path.TrimStart("Assets/Resources".ToCharArray());
         //   path = "paths.Add(\"" + path + "\"); ";
            outPath += path;
            outPath += "\n";
        }

        Debug.Log(outPath);
    }

    [MenuItem("HTools/输出文件裸路径")]
    [MenuItem("Assets/输出文件裸路径")]
    static public void SelectOutputObjLuoPath()
    {
        string outPath = "";
        foreach (Object obj in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            int index = path.IndexOf(".");
            path = path.Substring(0, index);
            path = path.TrimStart("Assets/Resources".ToCharArray());
            outPath += path;
            outPath += "\n";
        }

        Debug.Log(outPath);
    }

    static bool GetSelectionEffectObject(ref List<ObjectInfo> ltObjList, string P_START = "package_")
    {
        if (ltObjList == null)
            return false;

        UnityEngine.Object[] arrSelectionObject = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);
        foreach (UnityEngine.Object obj in arrSelectionObject)
        {
            if (obj.name.StartsWith(P_START))
            {
                ObjectInfo info = new ObjectInfo();
                info._packName = obj.name;
                info._packObj = obj;
                info._packPath = AssetDatabase.GetAssetPath(obj);
                info._prefabPath = info._packPath + "/Prefab/";
                if (!Directory.Exists(info._prefabPath))
                {
                    info._prefabPath = info._packPath + "/Prefabs/";
                    if (!Directory.Exists(info._prefabPath))
                    {
                        Debug.LogError(info._prefabPath + "not found!");
                        continue;
                    }
                }
                info._name = obj.name.Remove(0, P_START.Length);

                ltObjList.Add(info);

            }
        }

        return ltObjList.Count > 0;
    }
}
