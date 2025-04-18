using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MedicinalMaterial : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("")]public MedicinalMaterial_SO medicinalMaterial_SO;
    Transform parent;

    void Awake() {
        parent = GameObject.Find("OnDrag").transform;
    }

    GameObject thisGameObject;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        thisGameObject = Instantiate(gameObject, parent);
        thisGameObject.GetComponent<MedicinalMaterial>().medicinalMaterial_SO = medicinalMaterial_SO;
        thisGameObject.transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        thisGameObject.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(thisGameObject);
        // 射线检测鼠标是否碰撞到碰撞题
        RaycastHit2D inner = Physics2D.Raycast(eventData.position, Vector2.zero);
        // 检测是否有碰撞题，有则是否为锅
        if (inner.collider!=null&&inner.collider.CompareTag("Pot"))
        {
            EventManager.instance.SetInvokeParam("Pot/Add",medicinalMaterial_SO);
            EventManager.instance.Invoke("Pot/Add");
        }

    }
}
