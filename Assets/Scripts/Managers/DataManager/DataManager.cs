using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagerChange
{
    public OrderData_SO order_data{set=>DataManager.instance.order_data=value;}
    public CustomerData_SO customer_data{set=>DataManager.instance.customer_data=value;}
    public PotData_SO pot_data{set=>DataManager.instance.pot_data=value;}
    public GameData_SO game_data{set=>DataManager.instance.game_data=value;}
    public AudioData_SO audio_data{set=>DataManager.instance.audio_data=value;}
    public SettingData_SO setting_data{set=>DataManager.instance.setting_data=value;}
    public SaveData_SO save_data_list_Add{
        set=>DataManager.instance.save_data_list.Add(value);
        }
    public void Init()
    {
        DataManager.instance.Init();
    }
}

public class DataManager : MonoBehaviour
{
    public OrderData_SO OrderData{get=>order_data;}
    public CustomerData_SO CustomerData{get=>customer_data;}
    public PotData_SO PotData{get=>pot_data;}
    public GameData_SO GameData{get=>game_data;}
    public AudioData_SO AudioData{get=>audio_data;}
    public SettingData_SO SettingData{get=>setting_data;}
    public SaveData_SO DefaultSaveData{get=>save_data_list[0];}

    internal OrderData_SO order_data;
    internal CustomerData_SO customer_data;
    internal PotData_SO pot_data;
    internal GameData_SO game_data;
    internal AudioData_SO audio_data;
    internal SettingData_SO setting_data;
    internal List<SaveData_SO> save_data_list;

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
            }
            return _dataManager;
        }
    }
    void Awake()
    {
        save_data_list??=new();
    }
    internal void Init()
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