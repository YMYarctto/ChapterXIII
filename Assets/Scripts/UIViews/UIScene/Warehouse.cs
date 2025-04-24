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

    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("Warehouse");
    }

    public override void Enable()
    {
        transform.position = new(Screen.width / 2, 0, 0);
    }

    public override void Disable()
    {
        transform.position = new(Screen.width * 5 / 2, 0, 0);
    }
    
}
