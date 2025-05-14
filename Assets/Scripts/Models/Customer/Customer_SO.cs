using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPotion;
using ECustomer;

[CreateAssetMenu(fileName = "Customer_SO", menuName = "Models/Customer_SO")]
public class Customer_SO : ScriptableObject
{
    [Header("ID")] public string ID;
    [Header("种类")] public CustomerClassification Classification;
    [Header("名称")] public CustomerName Name;
    [SerializeField][Header("可能点单的药水")] public List<PotionName> PotionRequest;
    [SerializeField][Header("点单对话")] public List<string> DialogOrder;
    [SerializeField][Header("完成订单对话")] public List<string> DialogSuccess;
    [SerializeField][Header("未完成订单对话")] public List<string> DialogFail;
    [SerializeField][Header("拒绝订单对话")] public List<string> DialogRefuse;

    public string GetRandomDialog(List<string> strs)
    {
        System.Random ran=new();
        return strs[ran.Next(strs.Count)];
    }
}
