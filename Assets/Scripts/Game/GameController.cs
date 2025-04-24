using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static float money;

    GameData_SO game_data;
    TotalTimer totalTimer;

    float remain_time;

    void Awake()
    {
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
            EventManager.instance.Invoke("Customer/SettleMoney");
            //TODO
            Debug.Log("累计获得金币: "+money);
        }
    }

    IEnumerator NextCustomer(float time)
    {
        yield return new WaitForSeconds(time);
        EventManager.instance.Invoke("Customer/Create");
        StartCoroutine(NextCustomer(game_data.CustomerRefreshTime));
    }

    public static void AddMoney(float m){
        money+=m;
        UIManager.instance.GetUIView<MoneyCounter>("MoneyCounter").ChangeUI(money);
    }
}
