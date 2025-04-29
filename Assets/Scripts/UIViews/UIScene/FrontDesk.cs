using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;

public class FrontDesk : UIView
{
    TMP_Text Calender;
    TMP_Text Sign;
    Transform clock_pointer;

    void Awake()
    {
        UIManager.instance.AddUIView("FrontDesk",this);
    }

    public override void Init()
    {
        Calender=transform.Find("Calender").GetComponentInChildren<TMP_Text>();
        Calender.text=DataManager.instance.DefaultSaveData.Day.ToString();
        Sign=transform.Find("Sign").GetComponent<TMP_Text>();
        Sign.text="营业";
        clock_pointer=transform.Find("Clock").Find("pointer").transform;
        clock_pointer.localRotation=Quaternion.Euler(0,0,0);
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("FrontDesk");
    }

    public void FinishToday()
    {
        Sign.text="打烊";
    }

    public void ChangeTimeUI(float per)
    {
        clock_pointer.localRotation=Quaternion.Euler(0,0,360*per);
    }

}
