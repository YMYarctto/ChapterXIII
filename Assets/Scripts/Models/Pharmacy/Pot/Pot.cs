using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETag;
using EMaterial;
using EColor;

public class Pot : MonoBehaviour
{
    GameObject PotionPrefab;

    PotData_SO pot_data;

    List<MedicinalMaterial_SO> medicinalMaterialList = new List<MedicinalMaterial_SO>();
    List<Efficacy> efficacyList = new List<Efficacy>();
    List<SideEffect> sideEffectList = new List<SideEffect>();
    List<Efficacy> offseted_efficacieList = new List<Efficacy>();
    List<SideEffect> offseted_sideEffectList = new List<SideEffect>();

    Animator animator;

    Transform[] shelf_uiviews = new Transform[3];

    status current_status;
    float current_time;

    void Awake()
    {
        PotionPrefab = ResourceManager.instance.GetGameObject(EResource.GameObjectName.Potion);
        pot_data = DataManager.instance.PotData;
        current_status=status.HaveWater;

        animator = GetComponent<Animator>();
        string[] _uiviews = {
        "Shelf_item1",
        "Shelf_item2",
        "Shelf_item3",
        };
        for (int i = 0; i < _uiviews.Length; i++)
        {
            shelf_uiviews[i] = GameObject.Find(_uiviews[i]).transform;
        }
    }

    void OnEnable()
    {
        EventManager.instance.AddListener<MedicinalMaterial_SO>("Pot/Add", AddMadicinalMaterial);
        EventManager.instance.AddListener("Pot/Make", StarStir);
    }

    void OnDisable()
    {
        EventManager.instance?.RemoveListener<MedicinalMaterial_SO>("Pot/Add", AddMadicinalMaterial);
        EventManager.instance?.RemoveListener("Pot/Make", StarStir);
    }

    void FixedUpdate()
    {
        if(current_status==status.Stiring)
            if(current_time>0){
                current_time-=Time.fixedDeltaTime;
            }else{
                current_status=status.Finish;
            }
    }

    public void AddMadicinalMaterial(MedicinalMaterial_SO medicinalMaterial_SO)
    {
        if(current_status!=status.HaveWater){
            return;
        }
        if (medicinalMaterialList.Contains(medicinalMaterial_SO))
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
        animator.SetTrigger("Add");
        AddTag();
        ShowUI();
    }

    public void AddTag()
    {
        foreach (MedicinalMaterial_SO medicinalMaterial_SO in medicinalMaterialList)
        {
            foreach (Efficacy efficacy in medicinalMaterial_SO.Efficacy)
            {
                //转化成相应负面效果
                SideEffect sideEffect = (SideEffect)(int)efficacy;
                //如果效果已经被抵消，跳过
                if (offseted_efficacieList.Contains(efficacy))
                {
                    continue;
                }
                //如果存在相应负面效果，移除
                if (sideEffectList.Contains(sideEffect))
                {
                    sideEffectList.Remove(sideEffect);
                    offseted_efficacieList.Add(efficacy);
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
                if (offseted_sideEffectList.Contains(sideEffect))
                {
                    continue;
                }
                //如果存在相应正面效果，移除
                if (efficacyList.Contains(efficacy))
                {
                    efficacyList.Remove(efficacy);
                    offseted_efficacieList.Add(efficacy);
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
        if (!ShelfIsEmpty())
        {
            return;
        }
        List<MaterialName> materialList = new List<MaterialName>();
        foreach (MedicinalMaterial_SO medicinalMaterial_SO in medicinalMaterialList)
        {
            materialList.Add(medicinalMaterial_SO.Name);
        }
        InstantiatePotion(materialList);
        Clear();
        UIManager.instance.GetUIView<PotInfoUI>("PotInfoUI").ClearUI();
    }

    void InstantiatePotion(List<MaterialName> materialList)
    {
        foreach (var view in shelf_uiviews)
        {
            GameObject gameobj_potion = Instantiate(PotionPrefab, view);
            gameobj_potion.GetComponent<Potion>().Init(materialList, efficacyList, sideEffectList);
            gameobj_potion.transform.localPosition = Vector3.zero;
        }
    }

    public void StarStir(){
        if(medicinalMaterialList.Count==0||current_status!=status.HaveWater){
            return;
        }
        StartCoroutine(Stir());
    }

    IEnumerator Stir(){
        EventManager.instance.Invoke("Pot/Make/Start");
        animator.SetBool("isStir",true);
        current_status=status.Stiring;
        current_time=pot_data.GetStirTime(medicinalMaterialList.Count);
        yield return new WaitUntil(()=>current_status==status.Finish);
        animator.SetBool("isStir",false);
        CreatePotion();
        EventManager.instance.Invoke("Pot/Make/Finish");
        current_status=status.HaveWater;
    }

    bool ShelfIsEmpty()
    {
        int count = 0;
        foreach (var trans in shelf_uiviews)
        {
            count += trans.childCount;
        }
        return count == 0;
    }

    void Clear()
    {
        medicinalMaterialList.Clear();
        efficacyList.Clear();
        sideEffectList.Clear();
        offseted_efficacieList.Clear();
        offseted_sideEffectList.Clear();
    }

    void ShowUI()
    {
        var view = UIManager.instance.GetUIView<PotInfoUI>("PotInfoUI");
        view.ShowImage();
        view.RemoveAllTag();
        string title_str = "";
        foreach (MedicinalMaterial_SO medicinalMaterial_SO in medicinalMaterialList)
        {
            title_str += medicinalMaterial_SO.Name.ToString() + " ";
        }
        view.ChangeTitle(title_str);

        
        foreach (Efficacy efficacy in efficacyList)
        {
            view.AddTag(efficacy.ToString(),TagColor.Efficacy);
        }
        foreach (SideEffect sideEffect in sideEffectList)
        {
            view.AddTag(sideEffect.ToString(),TagColor.SideEffect);
        }
        
        foreach (Efficacy efficacy in offseted_efficacieList)
        {
            view.AddTag(efficacy.ToString(),TagColor.Efficacy,true);
        }
        foreach (SideEffect sideEffect in offseted_sideEffectList)
        {
            view.AddTag(sideEffect.ToString(),TagColor.SideEffect,true);
        }
    }

    enum status{
        NoWater,
        HaveWater,
        Stiring,
        Finish,
    }
}
