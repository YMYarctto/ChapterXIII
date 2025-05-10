using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettlePage : UIView
{
    TMP_Text Day;
    TMP_Text CurrentMoney;
    TMP_Text TotalMoney;
    TMP_Text NormalCustomer;
    TMP_Text SpecialCustomer;
    Transform transform_this;

    void Awake()
    {
        UIManager.instance.AddUIView("SettlePage",this);
    }

    public override void Init()
    {
        transform_this=transform;
        Day=transform_this.Find("Day").GetComponent<TMP_Text>();
        CurrentMoney=transform_this.Find("CurrentMoney").GetComponent<TMP_Text>();
        TotalMoney=transform_this.Find("TotalMoney").GetComponent<TMP_Text>();
        NormalCustomer=transform_this.Find("NormalCustomer").GetComponent<TMP_Text>();
        SpecialCustomer=transform_this.Find("SpecialCustomer").GetComponent<TMP_Text>();
        Disable();
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("SettlePage");
    }

    public void GetData(int day,int money,int total_moeny,string normal_customer,string special_customer)
    {
        Day.text=day.ToString();
        CurrentMoney.text=money.ToString();
        TotalMoney.text=total_moeny.ToString();
        NormalCustomer.text=normal_customer.ToString();
        SpecialCustomer.text=special_customer.ToString();
    }

}
