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
        if(DataManager.instance.IsNewGame)
        {
            text.text = "开始游戏";
        }else
        {
            text.text = "读取存档";
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
        if(DataManager.instance.IsNewGame)
        {
            DataManager.instance.DefaultSaveData.LoadFromFile();
            UIManager.instance.GetUIView<LoadingInit>("LoadingInit").ChangeScene("PharmacyScene","MainScene",()=>{
                GameController.StartGameAction.Invoke();
            });
        }else
        {
            UIManager.instance.EnableUIView("LoadMenu");
        }
    }
}
