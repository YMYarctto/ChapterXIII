using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Page_SettleMoney : UIView
{
    TMP_Text day;
    TMP_Text money;

    void Awake()
    {
        UIManager.instance.AddUIView("Page_SettleMoney", this);
    }

    public override void Init()
    {
        day=transform.Find("day").GetComponent<TMP_Text>();
        money=transform.Find("money").GetComponent<TMP_Text>();
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("Page_SettleMoney");
    }

    public void SetData(int day,int money)
    {
        this.day.text = day.ToString();
        this.money.text = money.ToString();
    }
}
