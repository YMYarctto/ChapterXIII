using System.Collections.Generic;

public static class ResourceConst{
    public static Dictionary<GameObjects,string> gameObjects = new(){
        {GameObjects.Customer,"Assets/Perfebs/Customer/customer.prefab"},
        {GameObjects.Potion,"Assets/Perfebs/Potion/potion_empty.prefab"}
    };
}

public enum GameObjects{
    Potion,
    Customer,
}
