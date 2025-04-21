using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ETag;
using EColor;

public class MedicinalMaterial : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("")] public MedicinalMaterial_SO medicinalMaterial_SO;
    Transform parent;

    void Awake()
    {
        parent = GameObject.Find("OnDrag").transform;
    }

    GameObject thisGameObject;

    public void OnBeginDrag(PointerEventData eventData)
    {
        thisGameObject = Instantiate(gameObject, parent);
        thisGameObject.GetComponent<MedicinalMaterial>().medicinalMaterial_SO = medicinalMaterial_SO;
        thisGameObject.transform.position = eventData.position;

        ShowUI();
    }

    public void OnDrag(PointerEventData eventData)
    {
        thisGameObject.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(thisGameObject);
        // 射线检测鼠标是否碰撞到碰撞题
        Collider2D collider = Physics2D.Raycast(eventData.position, Vector2.zero).collider;
        // 检测是否有碰撞题，有则是否为锅
        if (collider != null && collider.CompareTag("Pot"))
        {
            EventManager.instance.SetInvokeParam("Pot/Add", medicinalMaterial_SO);
            EventManager.instance.Invoke("Pot/Add");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShowUI();
    }

    void ShowUI()
    {
        var view = UIManager.instance.GetUIView<ItemInfoUI>("ItemInfoUI");
        view.RemoveAllTag();
        view.ChangeTitle(medicinalMaterial_SO.Name.ToString());
        view.ChangeDescription(medicinalMaterial_SO.Description);

        foreach (Efficacy efficacy in medicinalMaterial_SO.Efficacy)
        {
            view.AddTag(efficacy.ToString(), TagColor.Efficacy);
        }
        foreach (SideEffect sideEffect in medicinalMaterial_SO.SideEffect)
        {
            view.AddTag(sideEffect.ToString(), TagColor.SideEffect);
        }
    }
}
