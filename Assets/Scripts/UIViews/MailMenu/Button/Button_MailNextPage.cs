using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_MailNextPage : MonoBehaviour,IPointerClickHandler
{
    public int Direction;

    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.GetUIView<MailContent>("MailContent").NextPage(Direction);
    }
}
