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

        if(PlayerInventory.Instance != null && PlayerInventory.Instance.currentItems.Count > 0)
        {
            Items itemToRestock = PlayerInventory.Instance.currentItems[0];
            PlayerInventory.Instance.currentItems.RemoveAt(0);

            itemInStock.Add(itemToRestock);

            UpdateShelfVisual();
        }
        else
        {
            Debug.Log("No items in inventory to restock the shelf");
            return;
        }
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

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        
        if (stockCount > 0 && itemInStock != null && itemInStock.Count > 0)
        {
            sr.sprite = itemInStock[0].itemSprite;
        }
        else
        {
            sr.sprite = null; 
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
