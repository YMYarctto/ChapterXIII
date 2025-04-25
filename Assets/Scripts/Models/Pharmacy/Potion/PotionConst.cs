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
    private static Dictionary<List<MaterialName>,PotionName> potionDict = new()
    {
        { new List<MaterialName> { MaterialName.绵眠叶,MaterialName.迷灵薄荷 }, PotionName.安眠药 },
        { new List<MaterialName> { MaterialName.凉影果,MaterialName.烈根生姜,MaterialName.迷灵薄荷 }, PotionName.清热药 },
        { new List<MaterialName> { MaterialName.石心花,MaterialName.虹须萝卜,MaterialName.迷灵薄荷 }, PotionName.止血药 },
        { new List<MaterialName> { MaterialName.凉影果,MaterialName.骨响草,MaterialName.烈根生姜,MaterialName.迷灵薄荷 }, PotionName.止痛药 },
        { new List<MaterialName> { MaterialName.凉影果,MaterialName.骨响草,MaterialName.灰胆根,MaterialName.烈根生姜,MaterialName.迷灵薄荷 }, PotionName.解毒药 },
        { new List<MaterialName> { MaterialName.石心花,MaterialName.月落藤,MaterialName.虹须萝卜,MaterialName.烈根生姜,MaterialName.迷灵薄荷 }, PotionName.安神药 },
        { new List<MaterialName> { MaterialName.银须菌,MaterialName.虹须萝卜 }, PotionName.抗菌药 },
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
