using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : UIView
{
    void Awake()
    {
        UIManager.instance.AddUIView("Warehouse", this);
    }

    public override void Init()
    {
        Disable();
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("Warehouse");
    }

    public override void Enable()
    {
        transform.localPosition = new(0,transform.localPosition.y , 0);
    }

    public override void Disable()
    {
        transform.localPosition = new(CanvasSetting.Width * 2, transform.localPosition.y, 0);
    }
    
}
