using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public OrderData_SO OrderData{get=>order_data;}
    public CustomerData_SO CustomerData{get=>customer_data;}
    public PotData_SO PotData{get=>pot_data;}
    public GameData_SO GameData{get=>game_data;}
    public SaveData_SO DefaultSaveData{get=>save_data_list[0];}

    [SerializeField]private OrderData_SO order_data;
    [SerializeField]private CustomerData_SO customer_data;
    [SerializeField]private PotData_SO pot_data;
    [SerializeField]private GameData_SO game_data;
    [SerializeField][Header("首个存档为默认存档")]private List<SaveData_SO> save_data_list;
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
        if(!DefaultSaveData.isInit)
        {
            save_data_list[0].LoadFromFile();
        }
        foreach(var save_data in save_data_list)
        {
            if(save_data.isInit)
            {
                save_data.LoadFromFile();
            }
        }
    }

    public void LoadSaveData(int index)
    {
        if(index>=save_data_list.Count||index<=0)
        {
            return;
        }
        save_data_list[0].LoadFromSO(save_data_list[index]);
    }
}