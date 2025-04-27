using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ECustomer;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "CustomerData_SO", menuName = "Data/Customer/CustomerData_SO")]
public class CustomerData_SO : ScriptableObject
{
    public Dictionary<string,int> CustomerStageDict{get=>Customer_Stage.ToDictionary(item=>item.customerName.ToString(),item=>item.stage);}

    [Header("点(1/2/3)单的顾客等待时间(秒)")]public List<float> CustomerWaitingTime;
    [Header("未接单时，时间消耗倍率")][Range(0,1)]public float OrderingTimeScale;
    [Header("接单后，时间消耗倍率")]public float WaitingTimeScale;
    [Header("心情为(绿/黄/红)时的付款倍率加成")]public List<float> TipRate;
    [Header("每位顾客首次出现的阶段")]public List<CustomerStage> Customer_Stage;


    public float GetWaitingTime(int count){
        if(CustomerWaitingTime.Count==0||count<=0)
        {
            return 40f;
        }
        if(CustomerWaitingTime.Count<=count)
        {
            return CustomerWaitingTime[CustomerWaitingTime.Count-1];
        }
        return CustomerWaitingTime[count-1];
    }

    public float GetTipRate(float per){
        int index=per>=0.7f?0:per>=0.3f?1:2;
        if(TipRate.Count>index){
            return TipRate[index];
        }
        return index==0?1.4f:index==1?1.2f:1f;
    }

    [Serializable]
    public struct CustomerStage
    {
        public CustomerName customerName;
        public int stage;
    }
}
