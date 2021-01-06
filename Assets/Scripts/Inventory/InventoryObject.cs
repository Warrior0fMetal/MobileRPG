using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySpace")]
public class InventoryObject : ScriptableObject
{
    
    public ItemDatabaseObject database;
    public string savePath;
    public Inventory Container;



    public void AddItem(Item _item, int _amount, bool _isstackable)
    {

        

            for (int i = 0; i < Container.Items.Length; i++)
            {
                
                if (Container.Items[i].item.Id == _item.Id && Container.Items[i].isStackable)

                {

                    Container.Items[i].AddAmount(_amount);
                    UnityEngine.Debug.Log("Increase Stack Amount");
                    return;

                }

            }
            UnityEngine.Debug.Log("NOT Stackable");
        SetFirstEmptySlot(_item,_amount,_isstackable);

        

    }

    public InventorySlot SetFirstEmptySlot(Item _item, int _amount, bool _isstackable)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(_item.Id, _item, _amount,_isstackable);
                return Container.Items[i];
            }
        }
        //Setup functionality if ur inventory is full
        return null;

    }  

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item,item2.amount,item2.isStackable);
        item2.UpdateSlot(item1.ID, item1.item, item1.amount, item1.isStackable);
        item1.UpdateSlot(temp.ID, temp.item, temp.amount, temp.isStackable);
    }

    [ContextMenu ("Save")]
    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();

    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < Container.Items.Length; i++)
            {
                Container.Items[i].UpdateSlot(newContainer.Items[i].ID, newContainer.Items[i].item, newContainer.Items[i].amount, newContainer.Items[i].isStackable);
            }
            stream.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {

        Container = new Inventory();
    }

  }

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[25];

}

[System.Serializable]
public class InventorySlot 
{
    public int ID = -1;
    public Item item;
    public int amount;
    public bool isStackable;


    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
        isStackable = false;

    }

    public InventorySlot(int _id, Item _item, int _amount, bool _isstackable)
    {
        ID = _id;
        item = _item;
        amount = _amount;
        isStackable = _isstackable;

    }

    public void UpdateSlot (int _id, Item _item, int _amount, bool _isstackable)
    {
        ID = _id;
        item = _item;
        amount = _amount;
        isStackable = _isstackable;

    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}