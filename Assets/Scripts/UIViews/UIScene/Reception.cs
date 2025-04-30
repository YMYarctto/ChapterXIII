using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reception : UIView
{
    void Awake()
    {
        UIManager.instance.AddUIView("Reception", this);
    }

    public override void Init()
    {
        Enable();
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("Reception");
    }

    public override void Enable()
    {
        transform.localPosition = new(0, transform.localPosition.y, 0);
    }

    public override void Disable()
    {
        transform.localPosition = new(-CanvasSetting.Width*2, transform.localPosition.y, 0);
    }
}
