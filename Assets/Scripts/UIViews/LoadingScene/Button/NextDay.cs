using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextDay : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.GetUIView<LoadingInit>("LoadingInit").LoadScene("PharmacyScene",()=>{
            UIManager.instance.DisableUIView("SettlePage");
        },()=>{
            GameController.StartGameAction.Invoke();
        });
    }
}
