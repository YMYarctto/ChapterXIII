using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_ChangeSAN : MonoBehaviour,IPointerClickHandler
{
    public int add_SAN;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameController.AddSAN(add_SAN);
    }
}
