using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailMenu : UIView
{
    void Awake()
    {
        UIManager.instance.AddUIView("MailMenu",this);
    }

    public override void Init()
    {
        Disable();
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("MailMenu");
    }
}
