using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPotion;
using EMaterial;
using System.Linq;
using ETag;

public static class PotionConst
{
    public static Dictionary<List<MaterialName>,PotionName> PotionDict{get=>potionDict;}
    public static Dictionary<MaterialName,List<PotionName>> PotionDict_v2{get=>potionDict_v2;}
    private static Dictionary<List<MaterialName>,PotionName> potionDict = new()
    {
        { new List<MaterialName> { MaterialName.绵眠叶,MaterialName.迷灵薄荷 }, PotionName.安眠药 },
        { new List<MaterialName> { MaterialName.凉影果,MaterialName.烈根生姜,MaterialName.迷灵薄荷 }, PotionName.清热药 },
        { new List<MaterialName> { MaterialName.石心花,MaterialName.虹须萝卜,MaterialName.迷灵薄荷 }, PotionName.止血药 },
        { new List<MaterialName> { MaterialName.凉影果,MaterialName.骨响草,MaterialName.烈根生姜,MaterialName.迷灵薄荷 }, PotionName.止痛药 },
        { new List<MaterialName> { MaterialName.凉影果,MaterialName.骨响草,MaterialName.灰胆根,MaterialName.烈根生姜,MaterialName.迷灵薄荷 }, PotionName.解毒药 },
        { new List<MaterialName> { MaterialName.石心花,MaterialName.月落藤,MaterialName.虹须萝卜,MaterialName.烈根生姜,MaterialName.迷灵薄荷 }, PotionName.安神药 },
        { new List<MaterialName> { MaterialName.银须菌,MaterialName.虹须萝卜 }, PotionName.抗菌药 },
        { new List<MaterialName> { MaterialName.诡枯脂,MaterialName.烈根生姜,MaterialName.迷灵薄荷 }, PotionName.毒药 },
        { new List<MaterialName> { MaterialName.绵眠叶,MaterialName.骨响草,MaterialName.梦壳核,MaterialName.迷灵薄荷 }, PotionName.致幻药 },
        { new List<MaterialName> { MaterialName.绵眠叶,MaterialName.石心花,MaterialName.煌光粉,MaterialName.虹须萝卜,MaterialName.迷灵薄荷 }, PotionName.快乐药 },
    };
    private static Dictionary<MaterialName,List<PotionName>> potionDict_v2 = new()
    {
        {MaterialName.迷灵薄荷,new(){PotionName.提神药}},
        {MaterialName.烈根生姜,new(){PotionName.止咳药}},
    };
    private static Dictionary<PotionName, string> potionDescriptionDict = new()
    {
        { PotionName.安眠药, "这是一瓶用于诱导睡眠的药剂，能有效缓解失眠，让使用者迅速进入深度睡眠状态。" },
        { PotionName.清热药, "这是一瓶用于缓解内火和燥热的药剂，适合在发热、上火时服用，具有良好的清热效果。" },
        { PotionName.止血药, "这是一瓶用于止血的基础药剂，适用于小型外伤或突发性出血，能够快速凝血并加快愈合。" },
        { PotionName.止痛药, "这是一瓶用于缓解身体疼痛的药剂，适合外伤或头痛等症状，短时间内可显著减轻疼痛感。" },
        { PotionName.解毒药, "这是一瓶用于清除体内毒素的药剂，适合因误食或中毒引起的不适，使用后可快速稳定症状。" },
        { PotionName.安神药, "这是一瓶用于镇定精神、缓解焦虑的药剂，适合神经紧张、易惊易怒等情况，服用后心绪将逐渐平稳。" },
        { PotionName.抗菌药, "这是一瓶用于抵抗感染的药剂，可对抗常见的细菌侵袭，适合在外伤或虚弱时期使用。" },
        { PotionName.毒药,"这瓶药剂未列入官方认证目录，标签模糊，成分未知。曾有客人私下表示它“令人安静”。监管部不建议处理此类药剂，除非你确定目标不会投诉。"},
        { PotionName.致幻药,"这瓶药剂会扰乱使用者的五感，引发视觉、听觉甚至时间感知的异常。有客人称它“能看到过去的人”，也有人因此失踪。监管部曾多次勒令销毁此类药剂，切勿陈列于货架。"},
        { PotionName.快乐药,"这瓶药剂会使人进入极度愉悦与满足的状态，有时伴随无差别的爱意和短暂健忘。它是某些“特殊客人”的最爱。请牢记：快乐不等于安全。"},
    };
    private static Dictionary<PotionName,int> priceDict = new()//暂定
    {
        { PotionName.提神药, 20},
        { PotionName.止咳药, 25},
        { PotionName.安眠药, 50 },
        { PotionName.清热药, 60 },
        { PotionName.止血药, 70 },
        { PotionName.止痛药, 80 },
        { PotionName.解毒药, 90 },
        { PotionName.安神药, 100 },
        { PotionName.抗菌药, 110 },
        { PotionName.毒药, 450 },
        { PotionName.致幻药, 600 },
        { PotionName.快乐药, 800 },
    };
    private static Dictionary<SideEffect,PotionName> specialPotionDict=new()
    {
        {SideEffect.中毒,PotionName.毒药},
        {SideEffect.欣快,PotionName.快乐药},
        {SideEffect.致幻,PotionName.致幻药},
    };

    public static PotionInfo GetPotionName(List<MaterialName> materialNames)
    {
        List<MaterialName> materialList = materialNames.OrderBy(x => (int)x).ToList();
        foreach(var kv in potionDict){
            if(kv.Key.SequenceEqual(materialList)){
                return new PotionInfo(kv.Value.ToString(), potionDescriptionDict[kv.Value]);
            }
        }
        return PotionInfo.Null;
    }

    public static PotionInfo GetPotionName(List<SideEffect> side_effect)
    {
        string name;
        string description;
        if(side_effect.Count==1)
        {
            if(specialPotionDict.TryGetValue(side_effect[0],out PotionName potionName))
            {
                name=potionName.ToString();
                description=potionDescriptionDict[potionName];
                return new(name,description);
            }
        }
        return PotionInfo.Null;
    }

    public static PotionInfo GetPotionName(List<Efficacy> efficacies)
    {
        List<Efficacy> efficacyList = efficacies.OrderBy(x => (int)x).Take(3).ToList();
        string name="";
        string description="这是一瓶同时拥有";
        foreach(var efficacy in efficacyList)
        {
            name+= $"{efficacy}";
            description += $"{efficacy}、";
        }
        description = description.Substring(0, description.Length - "、".Length);
        description += "效果的复合药剂。请根据实际情况合理使用。";
        name+="药";
        return new PotionInfo(name, description);
    }

    public static int GetPotionPrice(PotionName potionName)
    {
        if (priceDict.TryGetValue(potionName, out int price))
        {
            return price;
        }
        return 0;
    }

    public struct PotionInfo
    {
        public string potionName;
        public string potionDescription;

        public PotionInfo(string potionName, string potionDescription)
        {
            this.potionName = potionName;
            this.potionDescription = potionDescription;
        }

        public bool IsNull()
        {
            return potionName == "未知药水";
        }

        public static PotionInfo Null{get=> new PotionInfo("未知药水", "未知");}
    }
}
