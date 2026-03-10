using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<Items> currentItems = new List<Items>();

    public int maxSlot = 5;

    public static PlayerInventory Instance;

    void Awake()
    {
        Instance = this;
    }

    public bool CanReceiveItem()
    {
        return currentItems.Count < maxSlot;
    }

    public void ReceiveItem(Items item)
    {
        if(CanReceiveItem())
        {
            currentItems.Add(item);
            Debug.Log("Added: " + item.itemName + " to inventory");
        }
        else
        {
            Debug.Log("Inventory is full");
        }
    }
}
