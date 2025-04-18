using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETag;
using EPotion;

public class Customer_Normal : MonoBehaviour
{
    public int Current_potion_index{
        get { return current_potion_index; }
        set { current_potion_index = value>=potionList.Count?potionList.Count-1:value; }
    }

    List<PotionName> potionList = new();
    int current_potion_index = 0;

    public void GetPrice(Potion potion){
        Efficacy tag=(Efficacy)(int)potionList[current_potion_index];
        //int price;
        if(potion.EfficacyList.Contains(tag)){
            //TODO
        }
        else{
            //TODO
        }
        if(potion.SideEffectList.Count>0){
            //TODO
        }
        current_potion_index++;
        if(current_potion_index>=potionList.Count){
            SettlePrice();
        }
    }

    void SettlePrice(){
        //TODO
    }
}
