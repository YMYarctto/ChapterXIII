using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "SettingData_SO", menuName = "Data/Save/SettingData_SO")]
public class SettingData_SO : ScriptableObject
{
    const string fileName = "Setting";
    public float MusicVolume{get=>data.MusicVolume;set=>data.MusicVolume=value;}
    public float AudioVolume{get=>data.AudioVolume;set=>data.AudioVolume=value;}
    SettingData data;

    public bool isInit{get=>File.Exists(GetPath());}

    struct SettingData
    {
        public float MusicVolume;
        public float AudioVolume;

        public void Init()
        {
            MusicVolume=1;
            AudioVolume=1;
        }
    }

    public void SaveToFile()
    {
        var json = JsonUtility.ToJson(data);
        var path = GetPath();

        File.WriteAllText(path, json);
    }

    public void LoadFromFile()
    {
        var path = GetPath();
        if(!isInit)
        {
            data.Init();
            SaveToFile();
            return;
        }
        var json = File.ReadAllText(path);
        data = JsonUtility.FromJson<SettingData>(json);
        Debug.Log($"成功读取到 {fileName}.sav");
    }

    string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, fileName+".sav");
    }
}
