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
        {CustomerName.蘑菇兔,"蘑菇兔"},
        {CustomerName.黑猫,"黑猫"},
        {CustomerName.游侠花猫,"游侠花猫"},
        {CustomerName.笑面冰狐,"笑面冰狐"},
        {CustomerName.橘帽猪仔,"橘帽猪仔"},
        {CustomerName.睡帽猪仔,"睡帽猪仔"},
        {CustomerName.贺帽猪仔,"贺帽猪仔"},
    };

    public static Dictionary<CustomerName,string> customer_special_gameobject=new(){
        {CustomerName.特殊客人A,"特殊客人A"},
        {CustomerName.特殊客人B,"特殊客人B"},
        {CustomerName.特殊客人C,"特殊客人C"},
    };

    public static Dictionary<string,string> potion_sprite = new(){
        {"安眠","安眠药"},
        {"安神","安神药"},
        {"止痛","止痛药"},
        {"止血","止血药"},
        {"清热","清热药"},
        {"抗菌","抗菌药"},
        {"解毒","解毒药"},
        {"中毒","毒药"},
        {"致幻","致幻药"},
        {"欣快","快乐药"},
    };

    public static List<string> saveData_SO=new()
    {
        "data_0",
        "data_1",
        "data_2",
    };
}
