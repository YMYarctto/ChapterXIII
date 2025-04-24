using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public OrderData_SO OrderData{get=>order_data;}
    public CustomerData_SO CustomerData{get=>customer_data;}

    [SerializeField]private OrderData_SO order_data;
    [SerializeField]private CustomerData_SO customer_data;
    private static DataManager _dataManager;
    public static DataManager instance
    {
        get
        {
            if (!_dataManager)
            {
                _dataManager = FindObjectOfType(typeof(DataManager)) as DataManager;
                if (!_dataManager)
                    return null;
                _dataManager.Init();
            }
            return _dataManager;
        }
    }
    void Init()
    {
        
    }
}