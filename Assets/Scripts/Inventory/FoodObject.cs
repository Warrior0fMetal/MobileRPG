using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Item", menuName = "Inventory/ItemObject/Food")]
public class FoodObject : ItemObject
{
    public int restoreHungerValue;
    public void Awake()
    {
        type = ItemType.Food;
    }

}
