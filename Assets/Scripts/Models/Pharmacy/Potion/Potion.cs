using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETag;

public class Potion : MonoBehaviour
{
    string potion_name;
    List<Efficacy> efficacyList = new List<Efficacy>();
    List<SideEffect> sideEffectList = new List<SideEffect>();
    string potion_description;
    int potion_price;

    public static Potion CreatePotion(List<Efficacy> efficacyList, List<SideEffect> sideEffectList)
    {
        Potion potion = new();
        potion.efficacyList = efficacyList;
        potion.sideEffectList = sideEffectList;
        
        return potion;
    }
}
