using System.Collections.Generic;
using EResource;

public static class ResourceConst{
    public static Dictionary<GameObjectName,string> gameObjects = new(){
        {GameObjectName.Customer,"customer"},
        {GameObjectName.Potion,"potion"},
        {GameObjectName.Tag,"tag"},
        {GameObjectName.Order,"order"}
    };

    public static Dictionary<string,string> potion_sprite = new(){
        {"安眠","安眠药"},
        {"安神","安神药"},
        {"止痛","止痛药"},
        {"止血","止血药"},
        {"清热","清热药"},
        {"抗菌","抗菌药"},
        {"解毒","解毒药"},
    };
}
