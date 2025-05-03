using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotBar : UIView
{
    GameObject gameObject_this;
    Transform bar;
    Transform head;
    RectTransform mid;

    float max_length;

    void Awake()
    {
        UIManager.instance.AddUIView("PotBar",this);
    }

    public override void Init()
    {
        gameObject_this=gameObject;
        bar=transform.Find("bar");
        max_length=bar.GetComponent<RectTransform>().rect.width;
        head=bar.Find("head");
        mid=bar.Find("mid").GetComponent<RectTransform>();
        mid.sizeDelta=new Vector2(0,mid.rect.height);
        Disable();
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("PotBar");
    }

    public void ChangeBar(float per)
    {
        mid.sizeDelta=new Vector2(per*max_length,mid.rect.height);
        head.localPosition=new Vector3(per*max_length,0,0);
    }
}
