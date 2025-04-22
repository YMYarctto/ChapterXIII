using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPotion;

[CreateAssetMenu(fileName = "Order_SO", menuName = "Data/Order_SO", order = 2)]
public class Order_SO : ScriptableObject
{
    public List<int> OrderCount;

    public List<PotionName> PotionRange;

    public int RandomOrderCount(){
        System.Random ran = new();
        int per=0;
        foreach(var i in OrderCount)
        {
            per+=i;
        }
        var ran_per =ran.Next(per);
        per=0;
        for(int i=0;i<OrderCount.Count;i++)
        {
            per+=OrderCount[i];
            if(ran_per<per)return i+1;
        }
        return -1;
    }
}
