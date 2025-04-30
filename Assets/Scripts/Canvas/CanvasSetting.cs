using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : MonoBehaviour
{
    public static float Width{get=>rect.sizeDelta.y * ratio_const;}
    private float width;
    private float height;
    private float width_height_ratio;
    private float current_ratio;
    private static float ratio_const;
    private RectTransform rect_left;
    private RectTransform rect_right;
    private RectTransform bg;
    private static RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        ratio_const = 1920f / 1080f;
        rect=GetComponent<RectTransform>();
        rect_left = transform.Find("black_block_left").GetComponent<RectTransform>();
        rect_right = transform.Find("black_block_right").GetComponent<RectTransform>();
        bg = transform.Find("bg").GetComponent<RectTransform>();
        width = Screen.width;
        height = Screen.height;//获取屏幕宽高信息
        width_height_ratio=width/height;
        if (width_height_ratio >= ratio_const)
            Width_Add();
    }

    // Update is called once per frame
    void Update()
    {
        width = Screen.width;
        height = Screen.height;//获取屏幕宽高信息
        current_ratio=width/height;
        if(current_ratio==width_height_ratio)
        {
            return;
        }
        width_height_ratio=current_ratio;
        if (width_height_ratio >= ratio_const)
        {
            Width_Add();
        }
        // if (width_height_ratio < ratio_const)
        // {
        //     Height_Add();
        // }
    }
    public void Width_Add()
    {
        bg.anchorMax = new Vector2(0.5f, 0.5f);
        bg.anchorMin = new Vector2(0.5f, 0.5f);
        bg.anchoredPosition = new Vector2(0, 0);
        bg.pivot = new Vector2(0.5f, 0.5f);
        bg.sizeDelta = new Vector2(rect.sizeDelta.y * ratio_const,rect.sizeDelta.y);

        rect_right.anchorMax = new Vector2(0, 0.5f);//锚点
        rect_right.anchorMin = new Vector2(0, 0.5f);//锚点
        rect_right.anchoredPosition = new Vector2(0, 0);//位置
        rect_right.pivot = new Vector2(0f, 0.5f);//轴心
        rect_right.sizeDelta = new Vector2(((width_height_ratio* rect.sizeDelta.y) - rect.sizeDelta.y * ratio_const) / 2, rect.sizeDelta.y);

        rect_left.anchorMax = new Vector2(1, 0.5f);
        rect_left.anchorMin = new Vector2(1, 0.5f);
        rect_left.anchoredPosition = new Vector2(0, 0);
        rect_left.pivot = new Vector2(1f, 0.5f);
        rect_left.sizeDelta = new Vector2(((width_height_ratio * rect.sizeDelta.y) - rect.sizeDelta.y * ratio_const) / 2, rect.sizeDelta.y);
    }
    // public void Height_Add()
    // {
    //     rect[2].anchorMax = new Vector2(0.5f, 1f);//锚点
    //     rect[2].anchorMin = new Vector2(0.5f, 1f);//锚点
    //     rect[2].anchoredPosition = new Vector2(0, 0);//位置
    //     rect[2].pivot = new Vector2(0.5f, 1f);//轴心
    //     rect[2].sizeDelta = new Vector2(rect[0].sizeDelta.x, ((rect[0].sizeDelta.x/ width_height_ratio) - rect[0].sizeDelta.x / ratio_const) / 2);

    //     rect[1].anchorMax = new Vector2(0.5f, 0);
    //     rect[1].anchorMin = new Vector2(0.5f, 0);
    //     rect[1].anchoredPosition = new Vector2(0, 0);
    //     rect[1].pivot = new Vector2(0.5f, 0);
    //     rect[1].sizeDelta = new Vector2(rect[0].sizeDelta.x, ((rect[0].sizeDelta.x / width_height_ratio) - rect[0].sizeDelta.x / ratio_const) / 2);
    // }
}