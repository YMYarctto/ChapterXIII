using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETag;
using EPotion;

public class Customer_Normal : Customer
{
    public override void GivePotion(Potion potion)
    {
        PotionName potionName = potionList[current_potion_index]; 
        Efficacy tag = (Efficacy)(int)potionName;
        float price = PotionConst.GetPotionPrice(potionName);
        if (!potion.EfficacyList.Contains(tag))
        {
            price = 0;
            dialog_str = CurrentCustomer.GetRandomDialog(CurrentCustomer.DialogFail);
        }
        if (potion.SideEffectList.Count > 0)
        {
            price -= price * potion.SideEffectList.Count / 3;
            dialog_str = CurrentCustomer.GetRandomDialog(CurrentCustomer.DialogFail);
        }
        current_price += price > 0 ? price : 0;
        order_obj[current_potion_index].SetActive(false);
        current_potion_index++;
        if (current_potion_index >= potionList.Count)
        {
            SettleMoney();
            return;
        }
        ChangeUI(current_potion_index);
    }

    public override void SettleMoney()
    {
        current_price*=customer_data.GetTipRate(current_waiting_time/customer_waiting_time);
        base.SettleMoney();
    }

}
