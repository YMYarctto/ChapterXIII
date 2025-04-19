using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EMaterial;
using ETag;
using UnityEngine.EventSystems;

public class Potion : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    string potion_name;
    [HideInInspector]public List<Efficacy> EfficacyList = new List<Efficacy>();
    [HideInInspector]public List<SideEffect> SideEffectList = new List<SideEffect>();
    string potion_description;

    public void Init(List<MaterialName> materialList,List<Efficacy> efficacyList, List<SideEffect> sideEffectList)
    {
        EfficacyList = efficacyList;
        SideEffectList = sideEffectList;
        var potionInfo = PotionConst.GetPotionName(materialList);
        if (potionInfo.IsNull())
        {
            potionInfo=PotionConst.GetPotionName(efficacyList);
        }
        potion_name = potionInfo.potionName;
        potion_description = potionInfo.potionDescription;
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(GameObject.Find("OnDrag").transform,true);
        Debug.Log("开始拖动药水");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //TODO
        Debug.Log("结束拖动药水");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //TODO
        Debug.Log("正在拖动药水");
    }

    
}
