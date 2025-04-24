using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData_SO", menuName = "Data/Game/GameData_SO")]
public class GameData_SO : ScriptableObject
{
    [Header("游戏时间(秒)")]public float TotalTime;
    [Header("初始等待时间(秒)")]public float InitialWaitingTime;
    [Header("客人刷新频率(秒)")]public float CustomerRefreshTime;
}
