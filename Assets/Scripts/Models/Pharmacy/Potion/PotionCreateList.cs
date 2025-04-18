using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPotion;
using EMaterial;
using System.Linq;
using ETag;

public static class PotionCreateList
{
    private static Dictionary<List<MaterialName>,PotionName> potionDict;
    private static Dictionary<PotionName, string> potionDescriptionDict;
    private static Dictionary<Efficacy,string> efficacyDict;

    public static void Init(){
        potionDict= new Dictionary<List<MaterialName>, PotionName>
        {
            { new List<MaterialName> { MaterialName.绵眠叶,MaterialName.迷灵薄荷 }, PotionName.安眠药 },
            { new List<MaterialName> { MaterialName.凉影果,MaterialName.烈根生姜,MaterialName.迷灵薄荷 }, PotionName.清热药 },
            { new List<MaterialName> { MaterialName.石心花,MaterialName.虹须萝卜,MaterialName.迷灵薄荷 }, PotionName.止血药 },
            { new List<MaterialName> { MaterialName.凉影果,MaterialName.骨响草,MaterialName.烈根生姜,MaterialName.迷灵薄荷 }, PotionName.止痛药 },
            { new List<MaterialName> { MaterialName.凉影果,MaterialName.骨响草,MaterialName.灰胆根,MaterialName.烈根生姜,MaterialName.迷灵薄荷 }, PotionName.解毒药 },
            { new List<MaterialName> { MaterialName.石心花,MaterialName.月落藤,MaterialName.虹须萝卜,MaterialName.烈根生姜,MaterialName.迷灵薄荷 }, PotionName.安神药 },
            { new List<MaterialName> { MaterialName.银须菌,MaterialName.虹须萝卜 }, PotionName.抗菌药 },
        };

        potionDescriptionDict = new Dictionary<PotionName, string>
        {
            { PotionName.安眠药, "这是一瓶用于诱导睡眠的药剂，能有效缓解失眠，让使用者迅速进入深度睡眠状态。" },
            { PotionName.清热药, "这是一瓶用于缓解内火和燥热的药剂，适合在发热、上火时服用，具有良好的清热效果。" },
            { PotionName.止血药, "这是一瓶用于止血的基础药剂，适用于小型外伤或突发性出血，能够快速凝血并加快愈合。" },
            { PotionName.止痛药, "这是一瓶用于缓解身体疼痛的药剂，适合外伤或头痛等症状，短时间内可显著减轻疼痛感。" },
            { PotionName.解毒药, "这是一瓶用于清除体内毒素的药剂，适合因误食或中毒引起的不适，使用后可快速稳定症状。" },
            { PotionName.安神药, "这是一瓶用于镇定精神、缓解焦虑的药剂，适合神经紧张、易惊易怒等情况，服用后心绪将逐渐平稳。" },
            { PotionName.抗菌药, "这是一瓶用于抵抗感染的药剂，可对抗常见的细菌侵袭，适合在外伤或虚弱时期使用。" },
        };

        efficacyDict = new Dictionary<Efficacy, string>
        {
            { Efficacy.安眠, "睡眠障碍" },
            { Efficacy.清热, "各种热症" },
            { Efficacy.止血, "外伤出血、疾病出血" },
            { Efficacy.止痛, "外伤疼痛、神经疼痛" },
            { Efficacy.解毒, "误食中毒" },
            { Efficacy.安神, "神经紧张、易惊易怒" },
            { Efficacy.抗菌, "病菌感染" },
        };
    }

    public static PotionInfo GetPotionName(List<MaterialName> materialNames)
    {
        List<MaterialName> materialList = materialNames.OrderBy(x => (int)x).ToList();
        if (potionDict.TryGetValue(materialList, out PotionName potion))
        {
            return new PotionInfo(potion.ToString(), potionDescriptionDict[potion]);
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
        description = description.Substring(0, description.Length - 1);
        description += "效果的复合药剂，适用于";
        foreach(var efficacy in efficacyList)
        {
            if (efficacyDict.TryGetValue(efficacy, out string str))
            {
                description += $"{str}、";
            }
        }
        description = description.Substring(0, description.Length - 1);
        description += "等症状。请根据实际情况合理使用。";
        return PotionInfo.Null;
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
