using System.Collections.Generic;
using UnityEngine;

public struct SaveDataModel
{
    public int Day;
    public int Stage;
    public float Money;
    public int San;

    public string SaveTime;

    public List<int> MaterialList;

    public void Init()
    {
        Day=1;
        Stage=1;
        Money=0;
        San=6;
        MaterialList=new(){1,3,14,15};
        SaveTime=System.DateTime.Now.ToString();
    }
}
