using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Workbench : UIView
{
    void Awake()
    {
        UIManager.instance.AddUIView("Workbench",this);
    }

    public override void Init()
    {
        
    }

    public override void OnUnload()
    {
        UIManager.instance.RemoveUIView("Workbench");
    }

    public override void Enable()
    {
        transform.position=new(0,0,0);
    }

    public override void Disable()
    {
        transform.position=new(Screen.width,0,0);
    }
}
