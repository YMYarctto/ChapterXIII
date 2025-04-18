using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETag;
using EPotion;

public class Customer_Normal : MonoBehaviour
{
    float current_price;

    public int Current_potion_index{
        get { return current_potion_index; }
        set { current_potion_index = value>=potionList.Count?potionList.Count-1:value; }
    }

    List<PotionName> potionList = new();
    int current_potion_index = 0;

    public void GetPrice(Potion potion){
        PotionName potionName = potionList[current_potion_index];
        Efficacy tag=(Efficacy)(int)potionName;
        float price=PotionConst.GetPotionPrice(potionName);
        if(!potion.EfficacyList.Contains(tag)){
            price=0;
            //TODO 根据配方复杂度增加价值
        }
        if(potion.SideEffectList.Count>0){
            price-=price*potion.SideEffectList.Count/3;
        }
        current_price+=price>0?price:0;
        current_potion_index++;
        if(current_potion_index>=potionList.Count){
            SettlePrice();
        }
    }

    void SettlePrice(){
        //TODO
    }
}
