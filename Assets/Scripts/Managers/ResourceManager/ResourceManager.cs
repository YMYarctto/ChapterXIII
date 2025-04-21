using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using EResource;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<string,Sprite> sprite_dict;

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

    public GameObject GetGameObject(GameObjectName obj){
        return GetGameObject(ResourceConst.gameObjects[obj]);
    }

    public Sprite GetSprite(string url){
        Texture2D t2d=null;
        Addressables.LoadAssetAsync<Texture2D>(url).Completed += (handle) =>{t2d = handle.Result;};
        return Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), Vector2.zero);
    }

    public Sprite GetSprite(SpriteName sp){
        return GetSprite(ResourceConst.sprites[sp]);
    }
}
