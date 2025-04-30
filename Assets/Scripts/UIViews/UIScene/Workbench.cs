using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : UIView
{
    void Awake()
    {
        UIManager.instance.AddUIView("Workbench", this);
    }

    public override void Init()
    {
        Disable();
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("Workbench");
    }

    public override void Enable()
    {
        transform.localPosition = new(0, transform.localPosition.y, 0);
    }

    public override void Disable()
    {
        transform.localPosition = new(CanvasSetting.Width*2, transform.localPosition.y, 0);
    }
}
