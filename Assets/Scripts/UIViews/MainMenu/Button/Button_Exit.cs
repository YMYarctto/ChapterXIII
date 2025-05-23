using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class Button_Exit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
