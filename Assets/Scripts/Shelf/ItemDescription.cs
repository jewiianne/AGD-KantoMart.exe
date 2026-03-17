using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class ItemDescription : MonoBehaviour
{
    public List<Items> itemInStock = new List<Items>();
    public Shelf shelf;
    public GameObject itemDescriptionPanel;
    public TextMeshProUGUI itemDescriptionName;
    public TextMeshProUGUI itemDescriptionStock;
    public TextMeshProUGUI itemDescriptionPrice;
    private bool isPlayerInRange = false;

    void Start()
    {
        shelf = GetComponentInParent<Shelf>();

        itemDescriptionPanel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            itemDescriptionPanel.SetActive(true);

            if(itemDescriptionPanel)
            {
                UpdateItemDescription();
            }
        }
    }

    void UpdateItemDescription()
    {
        if (shelf != null && shelf.stockCount > 0)
        {
            string itemName = shelf.itemInStock[0].itemName;
            itemDescriptionName.text = "Name: " + itemName;
            itemDescriptionStock.text = $"Stock: {shelf.stockCount}/{shelf.maxStock}";
        }
    }

    public void IncreasePrice()
    {
        if (shelf.itemInStock != null && shelf.stockCount > 0)
        {
            shelf.itemInStock[0].itemPrice += 1.0f;
            UpdatePriceUI();   
        }
    }

    public void DecreasePrice()
    {
        if (shelf.itemInStock != null && shelf.stockCount > 0 && shelf.itemInStock[0].itemPrice != 0)
        {
            shelf.itemInStock[0].itemPrice -= 1.0f;
            UpdatePriceUI();   
        }
    }

    void UpdatePriceUI()
    {
        if (shelf != null && shelf.itemInStock.Count > 0)
        {
            itemDescriptionPrice.text = shelf.itemInStock[0].itemPrice.ToString();   
        }
    }

    public void SaveExit()
    {
        itemDescriptionPanel.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.color = Color.grey;
            Debug.Log("Player is in range to view item description");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.color = Color.white;
        }
    }
}
