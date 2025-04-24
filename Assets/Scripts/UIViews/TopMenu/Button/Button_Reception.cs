using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Reception : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    TMP_Text text;

    void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
        if (text == null)
        {
            Debug.LogError("Button: 未找到 TMP_Text 组件");
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ColorUtility.TryParseHtmlString("#F01d48", out Color color);
        text.color = color;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.black;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.DisableUIView("Workbench");
        UIManager.instance.EnableUIView("Reception");
        UIManager.instance.DisableUIView("Warehouse");
    }
}
