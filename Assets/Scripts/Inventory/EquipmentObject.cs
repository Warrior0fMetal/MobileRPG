using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/ItemObject/Equipment")]
public class EquipmentObject : ItemObject
{
    public float attackBonus;
    public float defenceBonus;
    public void Awake()
    {
        type = ItemType.Equipment;
    }

}
