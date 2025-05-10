using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml.Serialization;

public class MoneyCounter : UIView
{
    TMP_Text TMP;
    int currentMoney=0;
    int targetMoney=0;

    public void ChangeUISmooth(int money)
    {
        targetMoney=money;
        StartCoroutine(EChangeUISmooth());
    }

    public void ChangeUI(int money)
    {
        currentMoney=targetMoney=money;
        TMP.text=money.ToString();
    }

    IEnumerator EChangeUISmooth()
    {
        while (currentMoney < targetMoney)
        {
            currentMoney+=1;
            TMP.text=currentMoney.ToString();
            yield return new WaitForFixedUpdate();
        }
    }

    void Awake()
    {
        UIManager.instance.AddUIView("MoneyCounter",this);
    }

    public override void Init()
    {
        TMP=GetComponentInChildren<TMP_Text>();
        if (TMP == null)
        {
            Debug.LogError("Button: 未找到 TMP_Text 组件");
        }
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("MoneyCounter");
    }
}
