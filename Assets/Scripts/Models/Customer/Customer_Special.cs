using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETag;
using EPotion;
using System.Linq;

public class Customer_Special : Customer
{
    public override void Order_Recept()
    {
        GameController.AddSAN(-1);
        base.Order_Recept();
    }

    public override void Order_Refuse()
    {
        GameController.AddSAN(1);
        base.Order_Refuse();
    }

    protected override void CreateOrderList(int order_count){
        System.Random ran = new();
        var list = order_data.PotionRange.Intersect(CurrentCustomer.PotionRequest).ToList();
        potionList.Add(list[ran.Next(list.Count)]);
        CreateOrder(potionList[0]);
    }

    public override void GivePotion(Potion potion)
    {
        PotionName potionName = potionList[current_potion_index]; 
        int potion_int = (int)potionName;
        SideEffect tag = (SideEffect)(potion_int>100?potion_int-100:potion_int);
        float price = PotionConst.GetPotionPrice(potionName);
        if (potion.SideEffectList.Count>1||!potion.SideEffectList.Contains(tag))
        {
            price = 0;
        }
        current_price += price > 0 ? price : 0;
        SettleMoney();
    }

    public override void SettleMoney()
    {
        current_price*=customer_data.GetTipRate(current_waiting_time/customer_waiting_time);
        base.SettleMoney();
    }

}
