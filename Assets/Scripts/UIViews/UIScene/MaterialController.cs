using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 需要挂载在MedicinalMaterial物体的父物体上
/// </summary>
public class MaterialController : MonoBehaviour
{
    SaveData_SO save_data;

    void Awake()
    {
        save_data=DataManager.instance.DefaultSaveData;
        List<int> list=save_data.MaterialList;
        foreach(Transform trans in transform)
        {
            int id = (int)trans.gameObject.GetComponent<MedicinalMaterial>().medicinalMaterial_SO.ID;
            if(!list.Contains(id)){
                trans.gameObject.SetActive(false);
            }
        }
    }
}
