using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_ReturnMainMenu : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.GetUIView<LoadingInit>("LoadingInit").LoadScene("MainScene",()=>{
            UIManager.instance.DisableUIView("SettlePage");
        },null);
    }
}
