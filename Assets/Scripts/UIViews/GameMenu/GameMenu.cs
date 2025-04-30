using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : UIView
{
    void Awake()
    {
        UIManager.instance.AddUIView("GameMenu",this);
    }

    public override void Init()
    {
        gameObject.SetActive(false);
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("GameMenu");
    }

    public override void Enable()
    {
        Time.timeScale=0;
        base.Enable();
    }

    public override void Disable()
    {
        Time.timeScale=1;
        base.Disable();
    }
}
