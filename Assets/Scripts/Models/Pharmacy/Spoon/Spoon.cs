using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spoon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform parent;
    Transform drag_transform;

    string spoon="汤匙";
    string description="用于搅拌炼药锅的药材，放入锅中后需要花费一些时间搅拌";

    void Awake() {
        parent = GameObject.Find("Spoon_init_transform").transform;
        drag_transform = GameObject.Find("OnDrag").transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(drag_transform,true);
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parent,true);
        transform.localPosition = Vector3.zero;
        // 射线检测鼠标是否碰撞到碰撞题
        RaycastHit2D inner = Physics2D.Raycast(eventData.position, Vector2.zero);
        // 检测是否有碰撞题，有则是否为锅
        if (inner.collider!=null&&inner.collider.CompareTag("Pot"))
        {
            EventManager.instance.Invoke("Pot/Make");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    void ShowUI(){
        EventManager.instance.SetInvokeParam("UI/ItemInfo/ChangeTitle",spoon);
        EventManager.instance.Invoke("UI/ItemInfo/ChangeTitle");
        EventManager.instance.SetInvokeParam("UI/ItemInfo/ChangeDescription",description);
        EventManager.instance.Invoke("UI/ItemInfo/ChangeDescription");
    }
}
