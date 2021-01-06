using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public InventoryObject inventory;
    public GroundItem grItem;
  
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp()
    {
        
        inventory.AddItem(new Item(grItem.item), grItem.count, grItem.item.isStackable);
        UnityEngine.Debug.Log("Picking Up item" + transform.name);
        Destroy(grItem.gameObject);

    }

}