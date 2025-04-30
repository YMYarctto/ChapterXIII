using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyCounter : UIView
{
    TMP_Text TMP;

    public void ChangeUI(float money)
    {
        TMP.text=money.ToString();
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
