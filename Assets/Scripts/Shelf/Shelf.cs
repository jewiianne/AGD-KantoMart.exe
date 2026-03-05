using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Shelf : MonoBehaviour
{
    public List<Items> itemInStock;

    public int stockCount;
    public int maxStock = 5;

    public SpriteRenderer itemPrefab;
    public TextMeshProUGUI itemStockText;

    void Start()
    {
        if (itemInStock != null && itemInStock.Count > 0 &&stockCount > 0)
        {
            UpdateShelfVisual(0);
        }
    }

    void AddStock(int index)
    {
        if(stockCount >= maxStock)
        {
            Debug.Log("Shelf is currently full");
            return;
        }

        Debug.Log("Adding stock for: " + itemInStock[index].itemName);
        stockCount++;
        UpdateShelfVisual(index);
    }

    void UpdateShelfVisual(int index)
    {
        Items selected = itemInStock[index];

        if (itemPrefab != null && stockCount > 0)
        {
            GetComponent<SpriteRenderer>().sprite = selected.itemSprite;
        }

    }

    void OnValidate()
    {
        if (itemStockText != null)
        {
            itemStockText.text = $"{stockCount}/{maxStock}";
        }
    }

    // public void TakeItem(PlayerInventory inventory)
    // {
    //     if(stockCount > 0)
    //     {
    //         stockCount--;
    //         inventory.AddItem(item);
    //         UpdateShelfVisual();
    //     }
    //     else
    //     {
    //         Debug.Log("Out of stock!");
    //     }
    // }
    
}
