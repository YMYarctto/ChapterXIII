using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EMaterial;

[CreateAssetMenu(fileName = "GameData_SO", menuName = "Data/Game/GameData_SO")]
public class GameData_SO : ScriptableObject
{
    [Header("每阶段: 游戏时间(秒),初始等待时间(秒),客人刷新频率(秒)")]public List<time> TimeList;
    [Header("达到第( )天时进入下一阶,阶段目标( )钱")]public List<dayToState> DayToState;
    [Header("每阶段解锁的素材")]public List<MaterialList> MaterialLists;

    [Serializable]
    public struct dayToState
    {
        public int Day;
        public float Money;
    }

    [Serializable]
    public struct time
    {
        public float TotalTime;
        public float InitialWaitingTime;
        public float CustomerRefreshTime;
    }

    [Serializable]
    public struct MaterialList
    {
        public List<MaterialName> list;
    }

    public KeyValuePair<int,float> GetStage(int day)
    {
        for(int i=1;i<DayToState.Count;i++)
        {
            if(day==DayToState[i].Day)
            {
                return new(i,DayToState[i-1].Money);
            }
            if(day>=DayToState[i-1].Day&&day<DayToState[i].Day)
            {
                return new(i-1,0);
            }
        }
        return new(DayToState.Count-1,0);
    }

    public time GetTime(int stage)
    {
        if(stage<0&&stage>=TimeList.Count)
        {
            return TimeList[TimeList.Count-1];
        }
        return TimeList[stage];
    }

    public List<MaterialName> GetMaterialList(int stage)
    {
        if(stage<0&&stage>=MaterialLists.Count)
        {
            return MaterialLists[MaterialLists.Count-1].list;
        }
        return MaterialLists[stage].list;
    }
}
