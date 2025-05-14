using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_MailReturn : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = 1;
        UIManager.instance.DisableUIView("MailMenu");
        UIManager.instance.EnableUIView("Button_MailMenu");
    }
}
