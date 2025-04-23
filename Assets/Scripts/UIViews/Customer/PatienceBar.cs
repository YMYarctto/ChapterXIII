using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EColor;

public class PatienceBar : MonoBehaviour
{
    GameObject bottom;
    GameObject top;
    GameObject bar;
    RectTransform bar_rect;

    Image bottom_img;
    Image top_img;
    Image bar_img;

    float bar_height_max;
    float top_localPositionY;

    void Awake()
    {
        bottom=transform.Find("bottom").gameObject;
        top=transform.Find("top").gameObject;
        bar=transform.Find("bar").gameObject;
        bar_rect=bar.GetComponent<RectTransform>();
        bottom_img=bottom.GetComponent<Image>();
        top_img=top.GetComponent<Image>();
        bar_img=bar.GetComponent<Image>();
        bar_height_max=bar_rect.rect.height;
        top_localPositionY=top.transform.localPosition.y;
    }

    public void ChangeUI(float per)
    {
        bar_rect.sizeDelta=new Vector2(bar_rect.rect.width,bar_height_max*per);
        top.transform.localPosition=new Vector3(top.transform.localPosition.x,top_localPositionY-bar_height_max*(1-per),top.transform.localPosition.z);
        Color color=per>=0.7f?PatienceColor.Green:per>=0.3f?PatienceColor.Yellow:PatienceColor.Red;
        ChangeColor(color);
    }

    void ChangeColor(Color color)
    {
        bottom_img.color=color;
        top_img.color=color;
        bar_img.color=color;
    }

    enum ColorName
    {
        Green,
        Yello,
        Red,
    }
}
