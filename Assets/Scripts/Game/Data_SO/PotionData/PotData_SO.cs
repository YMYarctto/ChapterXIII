using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PotData_SO", menuName = "Data/Potion/PotData_SO")]
public class PotData_SO : ScriptableObject
{
    [Header("搅拌(1/2/3/4/5)个素材所用时间(秒)")]public List<float> StirTime;

    public float GetStirTime(int count){
        if(StirTime.Count==0||count<=0)
        {
            return 0;
        }
        if(StirTime.Count<=count)
        {
            return StirTime[StirTime.Count-1];
        }
        return StirTime[count-1];
    }
}
