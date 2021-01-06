using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ItemType
{
    Equipment,
    Food,
    Deafault

}
public class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite icon;
    public ItemType type;
    public bool isStackable;
    public string itemName = "New Item";
    [TextArea (15,20)]
    public string description;
}


[System.Serializable]
public class Item
{
    public string name;
    public int Id;
    public bool isStackable;
    public Item(ItemObject item)
    {
        name = item.name;
        Id = item.Id;
     }
}
