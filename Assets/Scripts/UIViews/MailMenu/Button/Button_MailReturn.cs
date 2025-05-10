using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_MailReturn : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.DisableUIView("MailMenu");
        UIManager.instance.EnableUIView("Button_MailMenu");
    }
}
