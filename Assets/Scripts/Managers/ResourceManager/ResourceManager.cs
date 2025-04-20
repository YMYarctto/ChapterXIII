using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager _resourceManager;
    public static ResourceManager instance
    {
        get
        {
            if (!_resourceManager)
            {
                _resourceManager = FindObjectOfType(typeof(ResourceManager)) as ResourceManager;
                if (!_resourceManager)
                    return null;
                _resourceManager.Init();
            }
            return _resourceManager;
        }
    }

    void Init()
    {
        
    }

    public GameObject GetGameObject(string url){
        GameObject prefabObj=null;
        Addressables.LoadAssetAsync<GameObject>(url).Completed += (handle) =>{prefabObj = handle.Result;};
        return prefabObj;
    }
}
