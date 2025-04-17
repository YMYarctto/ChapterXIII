using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MedicinalMaterial_SO", menuName = "Models/MedicinalMaterial_SO", order = 1)]
public class MedicinalMaterial_SO : ScriptableObject
{
    [Header("药材ID")]public uint ID;
    [Header("药材名称")]public string Name;
    [SerializeField][Header("药材功效")]public List<Efficacy> Efficacy;
    [SerializeField][Tooltip("不要选择'offest'开头的捏")][Header("药材副作用")]public List<SideEffect> SideEffect;
    [TextArea(1,5)][Header("药材描述")]public string Description;
}
