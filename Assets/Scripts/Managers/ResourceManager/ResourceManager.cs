using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using EResource;
using ResourceModel;
using EPotion;
using ETag;
using ECustomer;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<string,GameObject> gameObject_dict;
    private Dictionary<string,Sprite> sprite_dict;
    private Dictionary<string,Customer_SO> customer_SO_dict;

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
        sprite_dict??=new();
        gameObject_dict??=new();

        foreach(var kv in ResourceConst.potion_sprite)
        {
            Addressables.LoadAssetAsync<Texture2D>(kv.Value).Completed += (handle) =>{
                var t2d=handle.Result;
                sprite_dict[kv.Key+"药"]=Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), Vector2.zero);
            };
            Addressables.LoadAssetAsync<Texture2D>(kv.Value+"_瓶塞").Completed += (handle) =>{
                var t2d = handle.Result;
                sprite_dict[kv.Key+"药_瓶塞"]=Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), Vector2.zero);
            };
        }

        foreach(var kv in ResourceConst.gameObjects)
        {
            Addressables.LoadAssetAsync<GameObject>(kv.Value).Completed += (handle) =>{
                var obj=handle.Result;
                gameObject_dict[kv.Key.ToString()]= obj;
            };
        }

        foreach(var kv in ResourceConst.customer_normal_gameobject)
        {
            Addressables.LoadAssetAsync<GameObject>(kv.Value).Completed += (handle) =>{
                var obj=handle.Result;
                gameObject_dict[kv.Key.ToString()]= obj;
            };
        }

        foreach(var kv in ResourceConst.customer_normal_gameobject)
        {
            Addressables.LoadAssetAsync<Customer_SO>(kv.Value).Completed += (handle) =>{
                var obj=handle.Result;
                customer_SO_dict[kv.Key.ToString()]= obj;
            };
        }
    }

    public GameObject GetGameObject(GameObjectName obj_name){
        if(gameObject_dict.TryGetValue(obj_name.ToString(),out GameObject obj)){
            return obj;
        }
        Debug.Log($"获取预制体失败: {obj_name}");
        return null;
    }

    public List<GameObject> GetNormalCustomerList_v2(){
        List<GameObject> list=new();
        foreach(var v in ResourceConst.customer_normal_gameobject.Values){
            if(gameObject_dict.TryGetValue(v,out GameObject obj)){
                list.Add(obj);
                list.Add(obj);
                continue;
            }
            Debug.Log($"获取预制体失败: {v}");
        }
        return list;
    }

    public Customer_SO GetCustomerSO(string cus_name){
        if(customer_SO_dict.TryGetValue(cus_name,out Customer_SO so)){
            return so;
        }
        Debug.Log($"获取SO文件失败: {cus_name}");
        return null;
    }

    public Sprite GetSprite(string url){
        if(sprite_dict.TryGetValue(url,out Sprite sprite)){
            return sprite;
        }
        return null;
    }

    public PotionSprite GetPotionSprite(PotionName url){
        return GetPotionSprite(url.ToString());
    }

    public PotionSprite GetPotionSprite(Efficacy url){
        return GetPotionSprite(url.ToString()+"药");
    }

    PotionSprite GetPotionSprite(string url){
        var Sprite_potion = GetSprite(url);
        if(Sprite_potion==null)
            return null;
        var Sprite_plug = GetSprite(url+"_瓶塞");
        if(Sprite_plug==null)
            return null;
        return new(Sprite_potion,Sprite_plug);
    }
}
