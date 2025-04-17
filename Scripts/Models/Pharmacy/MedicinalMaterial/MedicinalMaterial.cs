using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MedicinalMaterial : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("")]public MedicinalMaterial_SO medicinalMaterial_SO;
    Transform parent;

    void Awake() {
        parent = GameObject.Find("Top_MousePointer").transform;
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
        // Vector2 Position = Camera.main.ScreenToWorldPoint(eventData.position);
        RaycastHit2D inner = Physics2D.Raycast(eventData.position, Vector2.zero);
        // 不为null，则认为有物体撞到
        if (inner.collider!=null&&inner.collider.CompareTag("Pot"))
        {
            Debug.Log("药材放入药锅");
            // EventManager.instance.SetInvokeParam("Pot/Add",medicinalMaterial_SO);
            // EventManager.instance.Invoke("Pot/Add");
        }

    }
}
