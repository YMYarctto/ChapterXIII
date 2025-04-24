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

    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("Reception");
    }

    public override void Enable()
    {
        transform.position = new(Screen.width / 2, 0, 0);
    }

    public override void Disable()
    {
        transform.position = new(-Screen.width / 2, 0, 0);
    }
}
