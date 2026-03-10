using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<Items> currentItems = new List<Items>();

    public static PlayerInventory Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (TakeItem.Instance != null && TakeItem.Instance.itemInInventory > 0)
        {
            TransferItemToInventory();
        }
    }

    void TransferItemToInventory()
    {
        while (TakeItem.Instance.itemInInventory > 0)
        {
            Items itemToAdd = TakeItem.Instance.itemInStock[0];
            string detectName = itemToAdd.itemName;
            Debug.Log("Player received item: " + detectName);
    
            currentItems.Add(itemToAdd);
            TakeItem.Instance.itemInStock.RemoveAt(0);
            TakeItem.Instance.itemInInventory--;
        }
    }
}
