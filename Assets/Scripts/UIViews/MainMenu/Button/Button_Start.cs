using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class Button_Start : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        DataManager.instance.LoadSaveData(0);
        SceneManager.LoadSceneAsync("PharmacyScene", LoadSceneMode.Additive);
    }
}
