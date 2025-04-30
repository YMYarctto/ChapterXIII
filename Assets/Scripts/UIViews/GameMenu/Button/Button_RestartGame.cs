using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_RestartGame : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        StopAllCoroutines();
        Time.timeScale=1;
        UIManager.instance.GetUIView<LoadingInit>("LoadingInit").ChangeScene("PharmacyScene","PharmacyScene",()=>{
            GameController.StartGameAction.Invoke();
        });
    }
}
