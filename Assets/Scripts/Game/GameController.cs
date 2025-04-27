using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public static int SAN{get=>san;}

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
    TotalTimer totalTimer;

    float remain_time;

    void Awake()
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
        StartCoroutine(init());
    }

    IEnumerator init()
    {
        yield return new WaitForFixedUpdate();
        totalTimer = UIManager.instance.GetUIView<TotalTimer>("TotalTimer");
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
        yield return new WaitForSeconds(3);
        //TODO
        Debug.Log("累计获得金币: "+money);
        Debug.Log("服务的客人: "+CustomerNormalRecepted.value+"/"+CustomerNormalTotal.value);
        Debug.Log("拒绝的客人: "+CustomerRefused.value);
    }

    public static void AddMoney(float m)
    {
        money+=m;
        UIManager.instance.GetUIView<MoneyCounter>("MoneyCounter").ChangeUI(money);
    }

    public static void AddSAN(int s)
    {
        san=san+s>=0?san+s:0;
        //TODO UI
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
