using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Shelf : MonoBehaviour
{
    public List<Items> itemInStock = new List<Items>();

    public int stockCount => itemInStock.Count;
    public int maxStock = 5;

    public SpriteRenderer itemPrefab;
    public TextMeshProUGUI itemStockText;

    void Start()
    {
        UpdateShelfVisual();
    }

    public void RestockShelf()
    {
        if (itemInStock.Count >= maxStock)
        {
            Debug.Log("Shelf is already fully stocked");
            return;
        }

        int heldIndex = PlayerInventory.Instance.selectedSlotIndex;
        Items itemToRestock = null;
        bool isFromBox = false;
        int boxIndex = -1;

        if (heldIndex < PlayerInventory.Instance.currentItems.Count)
        {
            itemToRestock = PlayerInventory.Instance.currentItems[heldIndex];
            isFromBox = false;
        }
        else if (heldIndex < (PlayerInventory.Instance.currentItems.Count + PlayerInventory.Instance.currentBoxItems.Count))
        {
            boxIndex = heldIndex - PlayerInventory.Instance.currentItems.Count;
            itemToRestock = PlayerInventory.Instance.currentBoxItems[boxIndex].itemData;
            isFromBox = true;
        }

        if (itemToRestock == null)
        {
            Debug.Log("No Item in this slot");
            return;
        }

        if (itemInStock.Count > 0)
        {
            if (itemInStock[0].itemName != itemToRestock.itemName)
            {
                Debug.Log($"Cannot restock! This shelf is for {itemInStock[0].itemName}, not {itemToRestock.itemName}");
                return;
            }
        }

        if (!isFromBox)
        {
            itemInStock.Add(itemToRestock);
            PlayerInventory.Instance.currentItems.RemoveAt(heldIndex);
        }
        else
        {
            itemInStock.Add(itemToRestock);
            BoxItems currentBox = PlayerInventory.Instance.currentBoxItems[boxIndex];
            currentBox.boxItemInsideCount--;

            if (currentBox.boxItemInsideCount <= 0)
            {
                PlayerInventory.Instance.currentBoxItems.RemoveAt(boxIndex);
            }
        }
        
        PlayerInventory.Instance.UpdateInventoryUI();
        UpdateShelfVisual();
    }

    void AddStock(Items item)
    {
        if(itemInStock.Count >= maxStock)
        {
            Debug.Log("Shelf is currently full");
            return;
        }
        itemInStock.Add(item);

        UpdateShelfVisual();
    }

    public void RemoveItem() 
    {
        if (itemInStock.Count > 0)
        {
            itemInStock.RemoveAt(0);

            UpdateShelfVisual();
        }
    }

    public void UpdateShelfVisual()
    {
        if (itemStockText != null)
        {
            itemStockText.text = $"{itemInStock.Count}/{maxStock}";
        }

        if (itemPrefab != null) 
        {
            if (stockCount > 0 && itemInStock != null && itemInStock.Count > 0)
            {
                itemPrefab.sprite = itemInStock[0].itemSprite;
                itemPrefab.gameObject.SetActive(true);
            }
            else
            {
                itemPrefab.sprite = null; 
                //itemPrefab.gameObject.SetActive(false);
            }
        }
    }

    void OnValidate()
    {
        if (itemStockText != null)
        {
            int count = itemInStock != null ? itemInStock.Count : 0;
            itemStockText.text = $"{count}/{maxStock}";
        }
    }
}
