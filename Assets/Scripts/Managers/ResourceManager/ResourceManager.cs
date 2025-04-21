using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using EResource;
using ResourceModel;

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

    void Awake()
    {
        _=instance;
    }

    void Init()
    {
        if(sprite_dict==null)
            sprite_dict=new();

        foreach(var kv in ResourceConst.potion_sprite){
            Addressables.LoadAssetAsync<Texture2D>(kv.Value).Completed += (handle) =>{
                var t2d=handle.Result;
                sprite_dict[kv.Key]=Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), Vector2.zero);
            };
            Addressables.LoadAssetAsync<Texture2D>(kv.Value+"_瓶塞").Completed += (handle) =>{
                var t2d = handle.Result;
                sprite_dict[kv.Key+"_瓶塞"]=Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), Vector2.zero);
            };
        }
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
        if(sprite_dict.TryGetValue(url,out Sprite sprite)){
            return sprite;
        }
        return null;
    }

    public PotionSprite GetPotionSprite(string url){
        var Sprite_potion = GetSprite(url);
        if(Sprite_potion==null)
            return null;
        var Sprite_plug = GetSprite(url+"_瓶塞");
        if(Sprite_plug==null)
            return null;
        return new(Sprite_potion,Sprite_plug);
    }
}
