using System.Collections.Generic;
using EResource;
using ECustomer;

public static class ResourceConst{
    public static Dictionary<GameObjectName,string> gameObjects = new(){
        {GameObjectName.Potion,"potion"},
        {GameObjectName.Tag,"tag"},
        {GameObjectName.Order,"order"}
    };

    public static Dictionary<CustomerName,string> customer_normal_gameobject=new(){
        {CustomerName.栗帽兔,"栗帽兔"},
        {CustomerName.精灵兔,"精灵兔"},
        {CustomerName.蘑菇,"蘑菇"},
        {CustomerName.黑猫,"黑猫"},
        {CustomerName.三花,"三花"},
        {CustomerName.狐狸,"狐狸"},
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
