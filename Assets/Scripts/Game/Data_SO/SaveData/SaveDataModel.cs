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
        Stage=0;
        Money=0;
        San=6;
        MaterialList=new();
        SaveTime=System.DateTime.Now.ToString();
    }

    public void NextDay(float Money_Add,int san)
    {
        Day++;
        Money+=Money_Add;
        San=san;
    }
}
