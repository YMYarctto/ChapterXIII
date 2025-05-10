using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_MailMenu : UIView,IPointerClickHandler
{
    void Awake()
    {
        UIManager.instance.AddUIView("Button_MailMenu",this);
    }

    public override void Init()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.EnableUIView("MailMenu");
        Disable();
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("Button_MailMenu");
    }
}
