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
        if (itemInStock.Count > 0 && itemInStock.Count < stockCount)
        {
            Items prototype = itemInStock[0];
            while (itemInStock.Count < stockCount)
            {
                itemInStock.Add(prototype);
            }
        }
    
    UpdateShelfVisual();
    }

    void AddStock()
    {
        if(stockCount >= maxStock)
        {
            Debug.Log("Shelf is currently full");
            return;
        }
        stockCount++;

        UpdateShelfVisual();
    }

    public void RemoveItem() 
    {
        if (stockCount > 0)
        {
            stockCount--;

            if(itemInStock != null && itemInStock.Count > 0)
            {
                itemInStock.RemoveAt(0);
            }

            UpdateShelfVisual();
        }
    }

    public void UpdateShelfVisual()
    {
        if (itemStockText != null)
        {
            itemStockText.text = $"{stockCount}/{maxStock}";
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
            itemStockText.text = $"{stockCount}/{maxStock}";
        }
    }
}
