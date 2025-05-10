using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AshBin : MonoBehaviour
{
    GameObject open;
    GameObject close;

    void Awake()
    {
        open=transform.Find("open").gameObject;
        close=transform.Find("close").gameObject;
        SetActive(open,false);
    }

    public void Open()
    {
        SetActive(open,true);
        SetActive(close,false);
    }

    public void Close()
    {
        SetActive(open,false);
        SetActive(close,true);
    }

    public void SetActive(GameObject obj,bool isActive)
    {
        if(obj.TryGetComponent<Image>(out var image))
        {
            float alpha=isActive ? 1 : 0;
            image.color=new Color(image.color.r,image.color.g,image.color.b,alpha);
        }
    }
}
