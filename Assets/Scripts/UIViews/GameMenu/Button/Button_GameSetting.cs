using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_GameSetting : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.GetUIView<LoadingInit>("LoadingInit").ChangeUIView(()=>{
            UIManager.instance.EnableUIView("SettingMenu");
        });
    }
}
