using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EMaterial;
using ETag;

public class Potion : MonoBehaviour
{
    string potion_name;
    public List<Efficacy> EfficacyList = new List<Efficacy>();
    public List<SideEffect> SideEffectList = new List<SideEffect>();
    string potion_description;

    public static Potion CreatePotion(List<MaterialName> materialList,List<Efficacy> efficacyList, List<SideEffect> sideEffectList)
    {
        Potion potion = new();
        potion.EfficacyList = efficacyList;
        potion.SideEffectList = sideEffectList;
        var potionInfo = PotionConst.GetPotionName(materialList);
        if (potionInfo.IsNull())
        {
            potionInfo=PotionConst.GetPotionName(efficacyList);
        }
        potion.potion_name = potionInfo.potionName;
        potion.potion_description = potionInfo.potionDescription;
        
        return potion;
    }
}
