using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public Order_SO Order{get=>_order;}

    [SerializeField]private Order_SO _order;
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