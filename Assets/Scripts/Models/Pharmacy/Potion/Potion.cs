using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EMaterial;
using ETag;
using UnityEngine.EventSystems;

public class Potion : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler,IPointerClickHandler
{
    string potion_name;
    [HideInInspector]public List<Efficacy> EfficacyList;
    [HideInInspector]public List<SideEffect> SideEffectList;
    string potion_description;

    Transform parent;
    Transform onDrag;

    GameObject potion;
    GameObject mini;

    Image current_image;

    public void Init(List<MaterialName> materialList,List<Efficacy> efficacyList, List<SideEffect> sideEffectList)
    {
        EfficacyList = new(efficacyList);
        SideEffectList = new(sideEffectList);
        var potionInfo = PotionConst.GetPotionName(materialList);
        if (potionInfo.IsNull())
        {
            potionInfo=PotionConst.GetPotionName(efficacyList);
        }
        potion_name = potionInfo.potionName;
        potion_description = potionInfo.potionDescription;
        parent=transform.parent;
        onDrag=GameObject.Find("OnDrag").transform;
        potion=transform.Find("potion").gameObject;
        mini=transform.Find("potion_mini").gameObject;
        mini.SetActive(false);
        current_image=Image.potion;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(onDrag,true);
        transform.position=eventData.position;
        ChangeImage(Image.potion);
        ShowUI();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 射线检测鼠标是否碰撞到碰撞题
        Collider2D collider = Physics2D.Raycast(eventData.position, Vector2.zero).collider;
        // 检测是否有碰撞题，有则是否为物品栏
        if (collider!=null)
        {
            if(collider.CompareTag("Customer"))
            {
                collider.transform.parent.GetComponent<Customer_Normal>().GivePotion(this);
                Destroy(gameObject);
                return;
            }

            if(collider.CompareTag("Inventory"))
            {
                if(collider.transform.childCount ==0)
                {
                    parent=collider.transform;
                    current_image=Image.mini;
                }
            }
            
        }
        transform.SetParent(parent,true);
        if(current_image==Image.mini)
        {
            ChangeImage(Image.mini);
        }
        transform.localPosition=Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position=eventData.position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShowUI();
    }

    void ShowUI(){
        EventManager.instance.SetInvokeParam("UI/ItemInfo/ChangeTitle",potion_name);
        EventManager.instance.Invoke("UI/ItemInfo/ChangeTitle");
        EventManager.instance.SetInvokeParam("UI/ItemInfo/ChangeDescription",potion_description);
        EventManager.instance.Invoke("UI/ItemInfo/ChangeDescription");
        string tag = "";
        foreach (Efficacy efficacy in EfficacyList)
        {
            tag += efficacy.ToString() + " ";
        }
        foreach (SideEffect sideEffect in SideEffectList)
        {
            tag += sideEffect.ToString() + " ";
        }
        EventManager.instance.SetInvokeParam("UI/ItemInfo/ChangeTag",tag);
        EventManager.instance.Invoke("UI/ItemInfo/ChangeTag");
    }

    void ChangeImage(Image image)
    {
        if(image==Image.potion){
            potion.SetActive(true);
            mini.SetActive(false);
        }else
        {
            potion.SetActive(false);
            mini.SetActive(true);
        }
    }

    enum Image
    {
        potion,
        mini,
    }
}
