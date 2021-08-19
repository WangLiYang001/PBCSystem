using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
public enum PreResourceType {
	PreGameObject = 1,
	PreAudioClip = 2,
}

/*
*  预加载资源工具类
 */
public class PreLoadResources : MonoBehaviour 
{
	public class ResPrefabItem
	{
		public string _path;
		public Object _prefab;
		public PreResourceType _type;
	}

	public class ResObjectItem
	{
		public string _path;
		public int _handle;
		public GameObject _object;
		public PreResourceType _type;
        public float _removeTime = 0;
	}

    string GetFilePath()
    {
        return Path.Combine(Application.streamingAssetsPath, "data/preloadObjs.data") ;
    }

    List<ResPrefabItem> _prefabList = new List<ResPrefabItem>();

	List<ResObjectItem> _objectList = new List<ResObjectItem>();
	//是否已加载完毕
    [HideInInspector]
	public bool _loadResFinish = false;
	public float _progress = 0.0f;

	private static PreLoadResources _instance = null;
	public static PreLoadResources Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType(typeof(PreLoadResources)) as PreLoadResources;
				
				if(_instance == null)
				{
					GameObject go = new GameObject("PreLoadResources");
					go.transform.position = new Vector3(-10000,0,-10000);
					_instance = go.AddComponent<PreLoadResources>();
				}
			}
			
			return _instance;
		}
	}

    void Start()
    {
        //if(Game.Instance.IsTestScene)
        //    Init();
    }

    public void Init()
    {
        Debug.Log("开始预加载资源");
        StartCoroutine(InitPreAll());

        InvokeRepeating("AutoUpdate", 0.5f, 0.5f);
    }

    void AutoUpdate()
    {
        foreach (ResObjectItem item in _objectList)
        {
            if (item._handle == -1 && item._object.activeSelf)
            {
                item._object.SetActive(false);
            }
        }
    }
    
    IEnumerator InitPreAll()
    {
        if (!File.Exists(GetFilePath()))
        {
            Debug.Log("预加载资源路径错误：" + GetFilePath());
            yield break;
        }
            

        string[] preObjectPahtList = File.ReadAllLines(GetFilePath());
        Debug.Log("预加载对象：" + preObjectPahtList.Length);

        _prefabList.Clear();

        yield return StartCoroutine(PreLoadPrefabs(preObjectPahtList));
        yield return StartCoroutine(PreLoadObject(preObjectPahtList));
        AutoUpdate();
        yield return new WaitForSeconds(1);
        _loadResFinish = true;
        Debug.Log("完成预加载资源 : " + preObjectPahtList.Length);
    }


    public static Object LoadAssetAtPath(string filePath)
    {
        Object obj = null;
        string file = filePath;
        if (filePath.StartsWith("/"))
            file = filePath.Substring(1);
        
        

        obj = Resources.Load(file);

        if (obj == null)
            Debug.LogError("路径:" + file + "未找到物体");

        return obj;
    }

    /*预加载对象
    */

    IEnumerator PreLoadObject(string[] preObjectPahtList)
    {
        int num = 0;
        //float t = Time.time;
        foreach (string id in preObjectPahtList)
        {
            num++;
            NewObject(id,false);
           // Debug.Log(id + "time = " + (Time.time - t));
           // t = Time.time;
            if(num > 5)
            {
                num = 1;
                yield return -1;
            }
        }
        yield break;
    }

	/*
	 * prefab 批量预加载
	 * */
	IEnumerator PreLoadPrefabs(string[] preObjectPahtList)
    {
        int num = 0;
        foreach (string path in preObjectPahtList)
		{
			if(FindPrefab(path) == null)
            {
                num++;
                PreLoadPrefab(path);
                if (num > 10)
                {
                    num = 0;
                    yield return -1;
                }
            }
				
		}

        yield break;
	}

    //预加载prefab
    void PreLoadPrefab(string path)
    {
        GameObject obj = (GameObject)LoadAssetAtPath(path);

        if (obj == null)
            return;

        ResPrefabItem item = new ResPrefabItem();
        item._path = path;
        item._prefab = obj;
        item._type = PreResourceType.PreGameObject;
        _prefabList.Add(item);
    }

    //查找对象prefab
    ResPrefabItem FindPrefab(string path)
    {
        foreach (ResPrefabItem item in _prefabList)
        {
            if (string.Compare(item._path, path) == 0)
                return item;
        }

        return null;
    }


    //句柄
    int _handleIndex = 100;
	int NewHandleIndex()
	{
		return _handleIndex++;
	}

    //获取对象
    public ResObjectItem GetObject(string path, Transform parent)
    {
        ResObjectItem item = GetObject(path);
        if (item == null)
            return null;

        item._object.transform.parent = parent;
        item._object.transform.localPosition = Vector3.zero;
        item._object.transform.localRotation = Quaternion.Euler(Vector3.zero);
        return item;
    }
    //获取对象
    public ResObjectItem GetObject(string path, Vector3 position, Vector3 rotation)
    {
        ResObjectItem item = GetObject(path);
        if (item == null)
            return null;

        item._object.transform.position = position;
        item._object.transform.rotation = Quaternion.Euler(rotation);
        return item;
    }

    //获取对象
    public ResObjectItem GetObject(string path, Vector3 position, Quaternion rotation)
    {
        ResObjectItem item = GetObject(path);
        if (item == null)
        {
            return null;
        }
            

        item._object.transform.position = position;
        item._object.transform.rotation = rotation;
        return item;
    }


    //获取对象
    public ResObjectItem GetObject(string path)
	{
		path = path.Trim ();
		ResObjectItem objItem = FindIdleObject (path);

		if(objItem == null)
			objItem = NewObject(path);

		if(objItem == null)
        {
            Debug.Log("没有找到物体：" + path);
            return null;
        }
			

		objItem._handle = NewHandleIndex ();
        objItem._object.SetActive(true);
        objItem._object.transform.parent = null;
		return objItem;
	}

	//移出对象
	public void RemoveObject(ResObjectItem item)
	{
		item._handle = -1;
		item._object.transform.parent = gameObject.transform;
		item._object.transform.localPosition = Vector3.zero;
        item._removeTime = Time.time;
        item._object.SetActive(false);

    }

    

	//移出对象
	public void RemoveObject(int handle )
	{
        if (this == null) return;
		ResObjectItem item = FindObject (handle);

		if(item != null)
		{
			item._handle = -1;
			item._object.transform.parent = gameObject.transform;
			item._object.transform.localPosition = Vector3.zero;
            item._removeTime = Time.time;
            item._object.SetActive(false);
        }
	}

	//根据句柄查找对象
	ResObjectItem FindObject(int handle)
	{
		foreach(ResObjectItem item in _objectList)
		{
			if(item._handle == handle)
				return item;
		}

		return null;
	}

	//查找空闲对象
	ResObjectItem FindIdleObject(string path)
	{
		foreach(ResObjectItem item in _objectList)
		{
			if(string.Equals(item._path,path) && item._handle < 0 && (Time.time - item._removeTime) > 0.1f)
				return item;
		}

		return null;
	}

	//创建一个对象
	ResObjectItem NewObject(string path , bool isAutoActive = true)
	{
		ResPrefabItem pItem = FindPrefab (path);
		if(pItem == null)
			return null;

		ResObjectItem item = new ResObjectItem ();
		item._handle = -1;
		item._path = path;
        item._removeTime = 0;
		item._object = (GameObject)GameObject.Instantiate (pItem._prefab);
        if (item._object == null)
            Debug.LogError("加载对象失败 :" + path);
        if(isAutoActive)
            item._object.SetActive(false);
		item._object.transform.parent = gameObject.transform;
		item._object.transform.localPosition = Vector3.zero;
		_objectList.Add (item);

		return item;
	}

	public void ReleaseAll() {
		foreach(ResObjectItem item in _objectList) {
			Destroy(item._object);
		}
		_objectList.Clear();

		_prefabList.Clear();
	}
}
