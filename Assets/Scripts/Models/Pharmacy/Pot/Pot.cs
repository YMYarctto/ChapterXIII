using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETag;
using EMaterial;

public class Pot : MonoBehaviour
{
    public GameObject PotionPrefab;

    List<MedicinalMaterial_SO> medicinalMaterialList = new List<MedicinalMaterial_SO>();
    List<Efficacy> efficacyList = new List<Efficacy>();
    List<SideEffect> sideEffectList = new List<SideEffect>();
    List<Efficacy> offested_efficacieList = new List<Efficacy>();
    List<SideEffect> offseted_sideEffectList = new List<SideEffect>();

    void OnEnable()
    {
        EventManager.instance.AddListener<MedicinalMaterial_SO>("Pot/Add", AddMadicinalMaterial);
    }

    void OnDisable()
    {
        EventManager.instance.RemoveListener<MedicinalMaterial_SO>("Pot/Add", AddMadicinalMaterial);
    }

    public void AddMadicinalMaterial(MedicinalMaterial_SO medicinalMaterial_SO)
    {
        if(medicinalMaterialList.Contains(medicinalMaterial_SO))
        {
            Debug.Log("药材已存在");
            return;
        }
        if (medicinalMaterialList.Count > 5)
        {
            Debug.Log("药锅已满");
            return;
        }
        medicinalMaterialList.Add(medicinalMaterial_SO);
        AddTag();
        DebugLog();
    }

    public void AddTag(){
        foreach (MedicinalMaterial_SO medicinalMaterial_SO in medicinalMaterialList)
        {
            foreach (Efficacy efficacy in medicinalMaterial_SO.Efficacy)
            {
                //转化成相应负面效果
                SideEffect sideEffect = (SideEffect)(int)efficacy;
                //如果效果已经被抵消，跳过
                if(offested_efficacieList.Contains(efficacy))
                {
                    continue;
                }
                //如果存在相应负面效果，移除
                if(sideEffectList.Contains(sideEffect))
                {
                    sideEffectList.Remove(sideEffect);
                    offested_efficacieList.Add(efficacy);
                    offseted_sideEffectList.Add(sideEffect);
                    continue;
                }
                //如果效果不存在，添加
                if (!efficacyList.Contains(efficacy))
                {
                    efficacyList.Add(efficacy);
                }
            }
            foreach (SideEffect sideEffect in medicinalMaterial_SO.SideEffect)
            {
                //转化成相应正面效果
                Efficacy efficacy = (Efficacy)(int)sideEffect;
                //如果效果已经被抵消，跳过
                if(offseted_sideEffectList.Contains(sideEffect))
                {
                    continue;
                }
                //如果存在相应正面效果，移除
                if(efficacyList.Contains(efficacy))
                {
                    efficacyList.Remove(efficacy);
                    offested_efficacieList.Add(efficacy);
                    offseted_sideEffectList.Add(sideEffect);
                    continue;
                }
                //如果效果不存在，添加
                if (!sideEffectList.Contains(sideEffect))
                {
                    sideEffectList.Add(sideEffect);
                }
            }
        }
    }

    /// <summary>
    /// 生成药水
    /// </summary>
    public void CreatePotion()
    {
        List<MaterialName> materialList = new List<MaterialName>();
        foreach (MedicinalMaterial_SO medicinalMaterial_SO in medicinalMaterialList)
        {
            materialList.Add(medicinalMaterial_SO.Name);
        }
        Clear();
        InstantiatePotion(materialList, efficacyList, sideEffectList);
    }

    void InstantiatePotion(List<MaterialName> materialList, List<Efficacy> efficacyList, List<SideEffect> sideEffectList)
    {
        string[] _uiviews = {
        "Shelf_item1",
        "Shelf_item2",
        "Shelf_item3",
        };
        foreach(var str in _uiviews)
        {
            GameObject gameobj_potion = Instantiate(PotionPrefab, GameObject.Find(str).transform);
            gameobj_potion.GetComponent<Potion>().Init(materialList, efficacyList, sideEffectList);
            gameobj_potion.transform.localPosition = Vector3.zero;
        }
    }

    void Clear()
    {
        medicinalMaterialList.Clear();
        efficacyList.Clear();
        sideEffectList.Clear();
        offested_efficacieList.Clear();
        offseted_sideEffectList.Clear();
    }

    void DebugLog()
    {
        string str=$" 药材个数: {medicinalMaterialList.Count}\n 功效:";
        foreach(Efficacy efficacy in efficacyList)
        {
            str += $" {efficacy},";
        }
        str += "\n 副作用:";
        foreach(SideEffect sideEffect in sideEffectList)
        {
            str += $" {sideEffect},";
        }
        str += "\n 被抵消的功效:";
        foreach(Efficacy efficacy in offested_efficacieList)
        {
            str += $" {efficacy},";
        }
        str += "\n 被抵消的副作用:";
        foreach(SideEffect sideEffect in offseted_sideEffectList)
        {
            str += $" {sideEffect},";
        }
        Debug.Log(str);
    }
}
