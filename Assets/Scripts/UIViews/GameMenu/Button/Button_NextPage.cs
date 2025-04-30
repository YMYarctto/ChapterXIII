using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_NextPage : UIView, IPointerClickHandler
{
    void Awake()
    {
        UIManager.instance.AddUIView("Button_NextPage",this);
    }

    public override void Init()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PageConst.ChangePage(1);
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("Button_NextPage");
    }
}
