using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultUIView : UIView
{
    public string UIViewIndex;

    void Awake()
    {
        UIViewIndex ??= gameObject.name;
        UIManager.instance.AddUIView(UIViewIndex,this);
    }

    public override void Init()
    {
        StartCoroutine(EInit());
    }

    IEnumerator EInit()
    {
        yield return null;
        Disable();
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView(UIViewIndex);
    }
}
