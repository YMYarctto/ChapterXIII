using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPotion;

[CreateAssetMenu(fileName = "OrderData_SO", menuName = "Data/Customer/OrderData_SO")]
public class OrderData_SO : ScriptableObject
{
    [Header("顾客点单数量(1/2/3)的对应权值")]public List<int> OrderCount;

    [Header("顾客点单数量的随机范围")]public List<PotionName> PotionRange;

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
