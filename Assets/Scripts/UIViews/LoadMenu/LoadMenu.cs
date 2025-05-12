using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMenu : UIView
{
    void Awake()
    {
        UIManager.instance.AddUIView("LoadMenu", this);
    }

    public override void Init()
    {
        transform.localScale = new Vector3(0, 1, 1);
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("LoadMenu");
    }

    public override void Enable()
    {
        StartCoroutine(EEnable());
    }
    public override void Disable()
    {
        StartCoroutine(EDisable());
    }
    IEnumerator EEnable()
    {
        while(transform.localScale.x<1)
        {
            transform.localScale+=new Vector3(10,0,0)*Time.unscaledDeltaTime;
            yield return null;
        }
        transform.localScale=new Vector3(1,1,1);
    }
    IEnumerator EDisable()
    {
        while(transform.localScale.x>0)
        {
            transform.localScale-=new Vector3(10,0,0)*Time.unscaledDeltaTime;
            yield return null;
        }
        transform.localScale=new Vector3(0,1,1);
    }
}
