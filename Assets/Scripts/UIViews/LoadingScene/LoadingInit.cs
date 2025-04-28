using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadingInit : UIView
{
    bool startLoading;
    Image image;
    Color color;

    void Awake()
    {
        UIManager.instance.AddUIView("LoadingInit",this);
    }

    void FixedUpdate()
    {
        if(startLoading)
        {
            color.a-=0.6f*Time.fixedDeltaTime;
            image.color=color;
            if(color.a<=0)
                Destroy(gameObject);
        }
    }

    public override void Init()
    {
        image=GetComponent<Image>();
        color=image.color;
        startLoading=false;
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("LoadingInit");
    }

    public override void Disable()
    {
        startLoading=true;
    }
}
