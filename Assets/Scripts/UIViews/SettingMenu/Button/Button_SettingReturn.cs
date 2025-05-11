using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_SettingReturn : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        DataManager.instance.setting_data.SaveToFile();
        UIManager.instance.GetUIView<LoadingInit>("LoadingInit").ChangeUIView(() =>
        {
            UIManager.instance.DisableUIView("SettingMenu");
        });
    }
}
