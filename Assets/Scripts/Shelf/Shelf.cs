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

        if (heldIndex < PlayerInventory.Instance.currentItems.Count)
        {
            Items item = PlayerInventory.Instance.currentItems[heldIndex];
            PlayerInventory.Instance.currentItems.RemoveAt(heldIndex);
            itemInStock.Add(item);
        }
        else if (heldIndex < (PlayerInventory.Instance.currentItems.Count + PlayerInventory.Instance.currentBoxItems.Count))
        {
            int boxIndex = heldIndex - PlayerInventory.Instance.currentItems.Count;
            BoxItems currentBox = PlayerInventory.Instance.currentBoxItems[boxIndex];

            itemInStock.Add(currentBox.itemData);
            currentBox.boxItemInsideCount--;

            if (currentBox.boxItemInsideCount <= 0)
            {
                PlayerInventory.Instance.currentBoxItems.RemoveAt(boxIndex);
            }
        }
        else
        {
            Debug.Log("No Item to this slot");
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
