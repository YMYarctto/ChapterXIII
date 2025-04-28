using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using EResource;
using ResourceModel;
using EPotion;
using ETag;
using ECustomer;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

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
            }
            return _resourceManager;
        }
    }

    void Awake()
    {
        sprite_dict??=new();
        gameObject_dict??=new();
        customer_SO_dict??=new();
        StartCoroutine(InitDataManager());
    }

    IEnumerator InitDataManager()
    {
        LoadingPackage pkg=new("正在获取资源。。。");
        DataManagerChange dataManager=new();
        Addressables.LoadAssetAsync<OrderData_SO>("OrderData").Completed += (handle) =>{
            var so=handle.Result;
            dataManager.order_data=so;
            pkg.AddProgress();
        };
        pkg.AddCount();
        Addressables.LoadAssetAsync<CustomerData_SO>("CustomerData").Completed += (handle) =>{
            var so=handle.Result;
            dataManager.customer_data=so;
            pkg.AddProgress();
        };
        pkg.AddCount();
        Addressables.LoadAssetAsync<PotData_SO>("PotData").Completed += (handle) =>{
            var so=handle.Result;
            dataManager.pot_data=so;
            pkg.AddProgress();
        };
        pkg.AddCount();
        Addressables.LoadAssetAsync<GameData_SO>("GameData").Completed += (handle) =>{
            var so=handle.Result;
            dataManager.game_data=so;
            pkg.AddProgress();
        };
        pkg.AddCount();
        foreach(var SO in ResourceConst.saveData_SO)
        {
            Addressables.LoadAssetAsync<SaveData_SO>(SO).Completed += (handle) =>{
                var so=handle.Result;
                dataManager.save_data_list_Add=so;
                pkg.AddProgress();
            };
            pkg.AddCount();
        }
        yield return new WaitUntil(()=>pkg.Finish());
        Debug.Log("游戏初始化成功");
        dataManager.Init();
        StartCoroutine(InitResource());
    }

    IEnumerator InitResource()
    {
        LoadingPackage pkg=new("正在加载资源。。。");
        foreach(var kv in ResourceConst.potion_sprite)
        {
            Addressables.LoadAssetAsync<Texture2D>(kv.Value).Completed += (handle) =>{
                var t2d=handle.Result;
                sprite_dict[kv.Key+"药"]=Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), Vector2.zero);
                pkg.AddProgress();
            };
            pkg.AddCount();
            Addressables.LoadAssetAsync<Texture2D>(kv.Value+"_瓶塞").Completed += (handle) =>{
                var t2d = handle.Result;
                sprite_dict[kv.Key+"药_瓶塞"]=Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), Vector2.zero);
                pkg.AddProgress();
            };
            pkg.AddCount();
        }

        foreach(var kv in ResourceConst.gameObjects)
        {
            Addressables.LoadAssetAsync<GameObject>(kv.Value).Completed += (handle) =>{
                var obj=handle.Result;
                gameObject_dict[kv.Key.ToString()]= obj;
                pkg.AddProgress();
            };
            pkg.AddCount();
        }

        foreach(var kv in ResourceConst.customer_normal_gameobject)
        {
            Addressables.LoadAssetAsync<GameObject>(kv.Value).Completed += (handle) =>{
                var obj=handle.Result;
                gameObject_dict[kv.Key.ToString()]= obj;
                pkg.AddProgress();
            };
            pkg.AddCount();
        }

        foreach(var kv in ResourceConst.customer_normal_gameobject)
        {
            Addressables.LoadAssetAsync<Customer_SO>(kv.Value).Completed += (handle) =>{
                var so=handle.Result;
                customer_SO_dict[kv.Key.ToString()]= so;
                pkg.AddProgress();
            };
            pkg.AddCount();
        }

        yield return new WaitUntil(()=>pkg.Finish());
        Debug.Log("加载资源成功");
        pkg.DisableUI();
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        var op=SceneManager.LoadSceneAsync("MainScene",LoadSceneMode.Additive);
        while (!op.isDone)//如果没有完成
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        UIManager.instance.DisableUIView("LoadingInit");
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
        var dict=DataManager.instance.CustomerData.CustomerStageDict;
        int current_stage=DataManager.instance.DefaultSaveData.Stage;
        foreach(var v in ResourceConst.customer_normal_gameobject.Values){
            if(gameObject_dict.TryGetValue(v,out GameObject obj)){
                if(dict.TryGetValue(obj.name,out int stage)&&stage<=current_stage)
                {
                    list.Add(obj);
                    list.Add(obj);
                }
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

    struct LoadingPackage
    {
        string Name;
        int count;
        int progress;
        TMP_Text loadingUI;

        public LoadingPackage(string name)
        {
            Name=name;
            count=0;
            progress=0;
            loadingUI=GameObject.Find("LoadingUI").GetComponentInChildren<TMP_Text>();
        }

        public void AddCount()
        {
            count++;
        }

        public void AddProgress()
        {
            progress++;
            loadingUI.text=$"{Name} ({progress}/{count})";
        }

        public bool Finish()
        {
            return count==progress;
        }

        public void DisableUI()
        {
            loadingUI.gameObject.SetActive(false);
        }
    }
}
