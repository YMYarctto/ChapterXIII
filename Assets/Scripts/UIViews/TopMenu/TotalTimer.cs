using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalTimer : UIView
{
    TMP_Text TMP;

    public void ChangeTimeUI(int time)
    {
        TMP.text=time.ToString();
    }

    void Awake()
    {
        UIManager.instance.AddUIView("TotalTimer",this);
    }

    public override void Init()
    {
        TMP=GetComponentInChildren<TMP_Text>();
        if (TMP == null)
        {
            Debug.LogError("Button: 未找到 TMP_Text 组件");
        }
        TMP!.text=((int)DataManager.instance.GameData.TotalTime).ToString();
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("TotalTimer");
    }
}
