using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerData_SO", menuName = "Data/CustomerData_SO", order = 2)]
public class CustomerData_SO : ScriptableObject
{
    [Header("顾客等待时间(秒)")]public float CustomerWaitingTime;
    [Header("未接单时，时间消耗倍率")][Range(0,1)]public float OrderingTimeScale;
    [Header("接单后，时间消耗倍率")]public float WaitingTimeScale;
}
