using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData_SO", menuName = "Data/Save/SaveData_SO")]
public class SaveData_SO : ScriptableObject
{
    public int SAN{get=>data.San;}
    public int Day{get=>data.Day;}
    public int Stage{get=>data.Stage;}
    public float TotalMoney{get=>data.Money;}
    public bool isInit{get=>File.Exists(GetPath());}
    public List<int> MaterialList{get=>new(data.MaterialList);}

    public string fileName;
    SaveDataModel data;

    public void SaveToFile()
    {
        data.SaveTime=System.DateTime.Now.ToString();
        var json = JsonUtility.ToJson(data);
        var path = GetPath();

        File.WriteAllText(path, json);
    }

    public void LoadFromFile()
    {
        var path = GetPath();
        if(!isInit)
        {
            Init();
            SaveToFile();
            return;
        }
        var json = File.ReadAllText(path);
        data = JsonUtility.FromJson<SaveDataModel>(json);
        Debug.Log($"成功读取到 {fileName}.sav");
    }

    public void DeleteFile()
    {
        var path = GetPath();
        if(!File.Exists(path))
        {
            return;
        }
        File.Delete(path);
        Init();
    }

    public void LoadFromSO(SaveData_SO save_data)
    {
        if(!save_data.isInit)
        {
            save_data.LoadFromFile();
        }
        data=save_data.data;
    }

    public void SetData(SaveDataModel _data)
    {
        data = _data;
    }

    public SaveDataModel GetData()
    {
        return data;
    }

    public void LoadMaterial()
    {
        data.MaterialList.Clear();
        var game_data = DataManager.instance.game_data;
        for(int i=0;i<=Stage;i++)
        {
            data.MaterialList.AddRange(game_data.GetMaterialList(i).ConvertAll(x=>(int)x));
        }
    }

    string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, fileName+".sav");
    }

    void Init()
    {
        data.Init();
        LoadMaterial();
    }
}
