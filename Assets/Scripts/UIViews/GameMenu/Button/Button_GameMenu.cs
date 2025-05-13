using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_GameMenu : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.EnableUIView("GameMenu");
        UIManager.instance.DisableUIView("MailMenu");
        UIManager.instance.EnableUIView("Button_MailMenu");
        AudioManager.instance.PauseAudio();
    }
}
