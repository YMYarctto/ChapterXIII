using System.Collections.Generic;
using EResource;

public static class ResourceConst{
    public static Dictionary<GameObjectName,string> gameObjects = new(){
        {GameObjectName.Customer,"Assets/Perfebs/Customer/customer.prefab"},
        {GameObjectName.Potion,"Assets/Perfebs/Potion/potion.prefab"}
    };

    public static Dictionary<SpriteName,string> sprites = new(){
        {SpriteName.安眠药,""},
    };
}
