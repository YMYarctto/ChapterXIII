using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_LastPage : UIView, IPointerClickHandler
{
    void Awake()
    {
        UIManager.instance.AddUIView("Button_LastPage",this);
    }

    public override void Init()
    {
        Disable();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PageConst.ChangePage(-1);
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("Button_LastPage");
    }
}
