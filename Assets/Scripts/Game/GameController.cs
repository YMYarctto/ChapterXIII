using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public static int SAN{get=>san;}
    public static float Money{get=>money;}

    static float money;
    static int san;
    public static Int_OnlyAdd CustomerTotal;
    public static Int_OnlyAdd CustomerNormalTotal;
    public static Int_OnlyAdd CustomerNormalRecepted;
    public static Int_OnlyAdd CustomerSpecialTotal;
    public static Int_OnlyAdd CustomerSpecialRecepted;
    public static Int_OnlyAdd CustomerRefused;
    public static Int_OnlyAdd CustomerLeave;

    GameData_SO game_data;
    SaveData_SO save_data;
    TotalTimer totalTimer;

    float remain_time;

    void Awake()
    {
        StartCoroutine(Init());
    }

    void OnEnable()
    {
        EventManager.instance.AddListener("Scene/PharmacyScene/Load/Finish",StartGame);
    }

    void OnDisable()
    {
        EventManager.instance?.RemoveListener("Scene/PharmacyScene/Load/Finish",StartGame);
    }

    IEnumerator Init()
    {
        CustomerTotal=new();
        CustomerNormalTotal=new();
        CustomerNormalRecepted=new();
        CustomerSpecialTotal=new();
        CustomerSpecialRecepted=new();
        CustomerLeave=new();
        CustomerRefused=new();
        game_data=DataManager.instance.GameData;
        remain_time=game_data.TotalTime;
        save_data=DataManager.instance.DefaultSaveData;
        yield return null;
        totalTimer = UIManager.instance.GetUIView<TotalTimer>("TotalTimer");
    }

    public void StartGame()
    {
        StartCoroutine(ChangeTotalTime());
        StartCoroutine(NextCustomer(game_data.InitialWaitingTime));
    }

    IEnumerator ChangeTotalTime()
    {
        yield return new WaitForSeconds(1);
        remain_time-=1;
        if(remain_time>0)
        {
            totalTimer.ChangeTimeUI((int)remain_time);
            StartCoroutine(ChangeTotalTime());
        }else{
            totalTimer.ChangeTimeUI(0);
            StopAllCoroutines();
            StartCoroutine(WaitAllCustomerLeave());
        }
    }

    IEnumerator WaitAllCustomerLeave()
    {
        yield return new WaitUntil(()=>CustomerTotal.value==CustomerLeave.value);
        StartCoroutine(TodayFinish());
    }

    IEnumerator NextCustomer(float time)
    {
        yield return new WaitForSeconds(time);
        EventManager.instance.Invoke("Customer/Create");
        StartCoroutine(NextCustomer(game_data.CustomerRefreshTime));
    }

    IEnumerator TodayFinish()
    {
        SaveDataModel data=save_data.GetData();
        data.NextDay(money,san);
        save_data.SetData(data);
        yield return null;
        save_data.SaveToFile();
        yield return new WaitForSeconds(1);
        //TODO
        UIManager.instance.GetUIView<LoadingInit>("LoadingInit").UnloadScene("PharmacyScene",()=>{
            //EventManager.instance.Invoke("Scene/PharmacyScene/Unload/Finish");
            UIManager.instance.EnableUIView("SettlePage");
            UIManager.instance.GetUIView<SettlePage>("SettlePage").GetData(data.Day-1,money,data.Money,
            $"{CustomerNormalRecepted.value}/{CustomerNormalTotal.value}",
            $"{CustomerSpecialRecepted.value}/{CustomerSpecialTotal.value}");
        });
    }

    public static void AddMoney(float m)
    {
        money+=m;
        UIManager.instance.GetUIView<MoneyCounter>("MoneyCounter").ChangeUI(money);
    }

    public static void AddSAN(int s)
    {
        san=san+s>=0?san+s:0;
        EventManager.instance.Invoke("Game/SAN/OnChange");
    }

    public class Int_OnlyAdd
    {
        public int value{get=>_value;}
        int _value;
        public Int_OnlyAdd()
        {
            _value=0;
        }
        public void Add()
        {
            _value++;
        }
    }
}
