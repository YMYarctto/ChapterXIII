using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public static int SAN{get=>san;}
    public static float Money{get=>money;}
    public static UnityAction StartGameAction;

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
    GameData_SO.time game_data_time;
    SaveData_SO save_data;
    FrontDesk frontDesk;
    static MoneyCounter moneyCounter;

    float total_time;
    float remain_time;

    void Awake()
    {
        StartCoroutine(Init());
    }

    void OnDisable() 
    {
        EventManager.instance?.RemoveListener("Game/SAN/OnChange");
    }

    IEnumerator Init()
    {
        AudioManager.instance.PlayMusic("Music/Game/Start").Next("Music/Game/Loop");
        money=0;
        CustomerTotal=new();
        CustomerNormalTotal=new();
        CustomerNormalRecepted=new();
        CustomerSpecialTotal=new();
        CustomerSpecialRecepted=new();
        CustomerLeave=new();
        CustomerRefused=new();
        save_data=DataManager.instance.DefaultSaveData;
        game_data=DataManager.instance.GameData;
        san=save_data.SAN;
        game_data_time=game_data.GetTime(save_data.Stage);
        total_time=remain_time=game_data_time.TotalTime;
        StartGameAction=()=>StartGame();
        PageConst.Init();
        yield return null;
        AddSAN(1);
        moneyCounter=UIManager.instance.GetUIView<MoneyCounter>("MoneyCounter");
        moneyCounter.ChangeUI(0);
        frontDesk=UIManager.instance.GetUIView<FrontDesk>("FrontDesk");
        if(save_data.Stage+1<game_data.DayToState.Count&&save_data.Stage>0)
        {
            Page.Add(2,game_data.DayToState.ConvertAll(v=>v.Day).Contains(save_data.Day));
            UIManager.instance.GetUIView<Page_SettleMoney>("Page_SettleMoney").SetData(game_data.DayToState[save_data.Stage+1].Day-1,(int)game_data.DayToState[save_data.Stage].Money);
        }
        Page.Add(0,save_data.Day==1);
        Page.Add(1,save_data.Day==1);
        UIManager.instance.GetUIView<MailContent>("MailContent").AddPageFinish();
        UIManager.instance.EnableUIView("Button_MailMenu");
    }

    public void StartGame()
    {
        StartCoroutine(ChangeTotalTime());
        StartCoroutine(NextCustomer(game_data_time.InitialWaitingTime));
    }

    IEnumerator ChangeTotalTime()
    {
        while (remain_time>0)
        {
            remain_time-=Time.fixedDeltaTime;
            frontDesk.ChangeTimeUI(remain_time/total_time);
            yield return new WaitForFixedUpdate();
        }
        frontDesk.ChangeTimeUI(0);
        frontDesk.FinishToday();
        StopAllCoroutines();
        StartCoroutine(WaitAllCustomerLeave());
    }

    IEnumerator WaitAllCustomerLeave()
    {
        yield return new WaitUntil(()=>CustomerTotal.value==CustomerLeave.value);
        StartCoroutine(TodayFinish());
    }

    IEnumerator NextCustomer(float time)
    {
        time-=SAN<6?7-SAN:0;
        time=time<0?0:time;
        yield return new WaitForSeconds(time);
        EventManager.instance.Invoke("Customer/Create");
        StartCoroutine(NextCustomer(game_data_time.CustomerRefreshTime));
    }

    IEnumerator TodayFinish()
    {
        SaveDataModel data=save_data.GetData();
        data.NextDay(money,san);
        var kv=game_data.GetStage(data.Day);
        data.Stage=kv.Key;
        data.Money-=kv.Value;
        save_data.SetData(data);
        save_data.LoadMaterial();
        yield return null;
        save_data.SaveToFile();
        AudioManager.instance.StopAudio();
        AudioManager.instance.ForcePlayMusic("Music/Game/Finish");
        yield return new WaitForSeconds(1);
        //TODO
        UIManager.instance.GetUIView<LoadingInit>("LoadingInit").UnloadScene("PharmacyScene",()=>{
            UIManager.instance.EnableUIView("SettlePage");
            UIManager.instance.GetUIView<SettlePage>("SettlePage").GetData(data.Day-1,(int)money,(int)data.Money,
            $"{CustomerNormalRecepted.value}/{CustomerNormalTotal.value}",
            $"{CustomerSpecialRecepted.value}/{CustomerSpecialTotal.value}");
            AudioManager.instance.PlayMusic("Music/SettlePage");
        });
    }

    public static void AddMoney(float m)
    {
        money+=m;
        moneyCounter.ChangeUISmooth((int)money);
    }

    public static void AddSAN(int s)
    {
        san=san+s>=0?san+s<=6?san+s:6:0;
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
