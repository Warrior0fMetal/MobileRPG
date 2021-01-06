using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InventoryDisplay : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();
    public InventoryObject inventory;
    public GameObject invContainerPrefab;


    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int Y_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMNS;
    


    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();


    void Start()
    {
        CreateSlots();
    }

    
    void Update()
    {
        UpdateSlots();
    }

    public void UpdateSlots() {

        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetComponent<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].icon;
                _slot.Key.transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
                
            }
            else
            {

            }
        }
    
    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);



    }
   

    public void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(invContainerPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnTriggerEnter(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnTriggerDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnTriggerExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnTriggerDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnTriggerDragEnd(obj); });

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }

    }

    public void ClearDisplay()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void OnTriggerDragStart (GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(100, 100);
        mouseObject.transform.SetParent(transform.parent);
        if (itemsDisplayed[obj].ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].icon;
            img.raycastTarget = false;

            mouseItem.obj = mouseObject;
            mouseItem.item = itemsDisplayed[obj];
        }

        

    }
    public void OnTriggerDragEnd (GameObject obj)
    {
        if (mouseItem.hoverObj)
        {
            inventory.MoveItem(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObj]);
        }
        else
        {
            mouseItem.item = null;
        }
        Destroy(mouseItem.obj);
        mouseItem.item = null;

    }
    public void OnTriggerEnter(GameObject obj)
    {
        mouseItem.hoverObj = obj;
        if (itemsDisplayed.ContainsKey(obj))
            mouseItem.hoverItem = itemsDisplayed[obj];
    }
    public void OnTriggerDrag(GameObject obj)
    {
        if (mouseItem.obj != null)
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;

    }
    public void OnTriggerExit(GameObject obj)
    {
        mouseItem.hoverObj = null;
        mouseItem.hoverItem = null;

    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM*(i % NUMBER_OF_COLUMNS)), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMNS)), 0f);
    }


}

public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;

}