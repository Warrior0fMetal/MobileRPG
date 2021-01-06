using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Default Item", menuName = "Inventory/ItemObject/Default")]
public class DefaultObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Deafault;
    }
   
}
