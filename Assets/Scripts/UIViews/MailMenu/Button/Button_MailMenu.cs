using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_MailMenu : UIView,IPointerClickHandler
{
    GameObject unread;

    void Awake()
    {
        UIManager.instance.AddUIView("Button_MailMenu",this);
    }

    public override void Init()
    {
        unread=transform.Find("UnreadMail").gameObject;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.EnableUIView("MailMenu");
        Time.timeScale = 0;
        Disable();
    }

    public override void Enable()
    {
        unread.SetActive(UIManager.instance.GetUIView<MailContent>("MailContent").HaveNewMail);
        base.Enable();
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("Button_MailMenu");
    }
}
