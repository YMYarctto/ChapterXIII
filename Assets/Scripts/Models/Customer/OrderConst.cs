using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OrderConst
{
    public static List<List<Vector3>> PositonConst = new(){
        new(){new(-20,20,0)},
        new(){new(-20,60,0),new(-20,-40,0)},
        new(){new(-20,110,0),new(-20,10,0),new(-20,70,0)},
    };

    public static Dictionary<PositionScale,PositionModel> PositonStruct = new(){
        {PositionScale.Focus,new(new(1,1,1),36)},
        {PositionScale.Below,new(new(075f,0.75f,0.75f),24)},
    };

    public struct PositionModel{
        public Vector3 Scale;
        public int FontSize;

        public PositionModel(Vector3 scale,int fontSize){
            Scale = scale;
            FontSize=fontSize;
        }
    }

    public enum PositionScale{
        Focus,
        Below,
    }
}
