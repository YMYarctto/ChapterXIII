using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPotion;
using EMaterial;
using System.Linq;

[CreateAssetMenu(fileName = "OrderData_SO", menuName = "Data/Customer/OrderData_SO")]
public class OrderData_SO : ScriptableObject
{
    public List<PotionName> PotionRange{
        get{
            if(potion_range.Count==0)
            {
                LoadData(); 
            }
            return potion_range;
        }
    }

    [Header("顾客点单数量(1/2/3)的对应权值")]public List<int> OrderCount;

    List<PotionName> potion_range;

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

    public void LoadData()
    {
        potion_range=new();
        List<int> list=DataManager.instance.DefaultSaveData.MaterialList;
        //将list中所有 int 对应转换为枚举 MaterialName
        List<MaterialName> material_list= list.ConvertAll(x=>(MaterialName)x);
        foreach(var List_PotionName in PotionConst.PotionDict)
        {
            //判断 列表 中的所有元素是否都在 material_list 中
            if(List_PotionName.Key.All(x=>material_list.Contains(x)))
            {
                potion_range.Add(List_PotionName.Value);
            }
        }
    }
}
