using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData_SO", menuName = "Data/Save/SaveData_SO")]
public class SaveData_SO : ScriptableObject
{
    public string fileName;
    SaveDataModel data;

    public void SaveByJson()
    {
        var json = JsonUtility.ToJson(data);
        var path = GetPath();

        File.WriteAllText(path, json);
    }

    public void LoadFromJson()
    {
        var path = GetPath();
        if(!File.Exists(path))
        {
            Init();
            SaveByJson();
            return;
        }
        var json = File.ReadAllText(path);
        data = JsonUtility.FromJson<SaveDataModel>(json);
        Debug.Log($"成功读取到 {fileName}.sav.");
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

    string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, fileName+".sav");
    }

    public List<int> GetMaterialList()
    {
        return new(data.MaterialList);
    }

    void Init()
    {
        data.Day=1;
        data.Money=0;
        data.San=6;
        data.MaterialList=new(){1,2,14,15};
    }
}
