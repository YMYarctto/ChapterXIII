using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoadMenu : UIView
{
    public static EnableType enableType{get=>_enableType;}
    static EnableType _enableType;

    void Awake()
    {
        UIManager.instance.AddUIView("LoadMenu", this);
    }

    public override void Init()
    {
        transform.localScale = new Vector3(0, 1, 1);
        transform.localPosition = new Vector3(0,Screen.height,0);
        _enableType= EnableType.Load;
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("LoadMenu");
    }

    public void Enable(EnableType type)
    {
        _enableType = type;
        Enable();
    }

    public override void Enable()
    {
        foreach(Transform child in transform)
        {
            if(child.TryGetComponent(out LoadDataInfo data))
            {
                data.Init();
            }
        }
        StartCoroutine(EEnable());
    }
    public void ForceDisable()
    {
        transform.localScale = new Vector3(0, 1, 1);
    }
    public override void Disable()
    {
        StartCoroutine(EDisable());
    }
    IEnumerator EEnable()
    {
        transform.localPosition = new Vector3(0,0,0);
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
        transform.localPosition = new Vector3(0,Screen.height,0);
    }

}

public enum EnableType
{
    Load,
    Save,
}