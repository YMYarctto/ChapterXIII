using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    List<MedicinalMaterial_SO> medicinalMaterialList = new List<MedicinalMaterial_SO>();
    List<Efficacy> efficacieList = new List<Efficacy>();
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
                SideEffect sideEffect = (SideEffect)(int)efficacy;
                if(offested_efficacieList.Contains(efficacy))
                {
                    continue;
                }
                if(sideEffectList.Contains(sideEffect))
                {
                    sideEffectList.Remove(sideEffect);
                    offested_efficacieList.Add(efficacy);
                    offseted_sideEffectList.Add(sideEffect);
                    continue;
                }
                if (!efficacieList.Contains(efficacy))
                {
                    efficacieList.Add(efficacy);
                }
            }
            foreach (SideEffect sideEffect in medicinalMaterial_SO.SideEffect)
            {
                Efficacy efficacy = (Efficacy)(int)sideEffect;
                if(offseted_sideEffectList.Contains(sideEffect))
                {
                    continue;
                }
                if(efficacieList.Contains(efficacy))
                {
                    efficacieList.Remove(efficacy);
                    offested_efficacieList.Add(efficacy);
                    offseted_sideEffectList.Add(sideEffect);
                    continue;
                }
                if (!sideEffectList.Contains(sideEffect))
                {
                    sideEffectList.Add(sideEffect);
                }
            }
        }
    }

    void DebugLog()
    {
        string str=$" 药材个数: {medicinalMaterialList.Count}\n 功效:";
        foreach(Efficacy efficacy in efficacieList)
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
