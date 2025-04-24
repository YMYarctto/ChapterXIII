using System.Collections.Generic;
using EResource;

public static class ResourceConst{
    public static Dictionary<GameObjectName,string> gameObjects = new(){
        {GameObjectName.Potion,"potion"},
        {GameObjectName.Tag,"tag"},
        {GameObjectName.Order,"order"}
    };

    public static Dictionary<NormalCustomerName,string> customer_normal_gameobject=new(){
        {NormalCustomerName.兔子,"兔子"},
        {NormalCustomerName.垂耳兔,"垂耳兔"},
        {NormalCustomerName.蘑菇,"蘑菇"},
        {NormalCustomerName.黑猫,"黑猫"},
        {NormalCustomerName.三花,"三花"},
        {NormalCustomerName.狐狸,"狐狸"},
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
