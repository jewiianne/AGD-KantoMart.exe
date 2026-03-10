using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class SellManager : MonoBehaviour
{
    public float currentTotalPrice = 0;
    
    public GameObject sellPanel;
    public TextMeshProUGUI itemsText;
    public TextMeshProUGUI totalPriceText;
    public TextMeshProUGUI paymentMethodText;

    public static SellManager Instance;

    void Awake()
    {
        Instance = this;
        sellPanel.SetActive(false);
    }

    public void OpenSellPanel()
    {
        if (CustomerSpawner.Instance == null || CustomerSpawner.Instance.currentCustomer == null)
        {
            return;
        }

        List<Items> playerHas = PlayerInventory.Instance.currentItems;
        List<Items> customerWants = CustomerSpawner.Instance.itemOrder;

        if (playerHas != null && playerHas.Contains(customerWants[0]))
        {
            sellPanel.SetActive(true);
        }
        else
        {
            Debug.Log("You don't have any items");
        }
    }

    public void UpdateUI(Items item)
    {
        if (item == null)
        {
            itemsText.text = "Item: None";
            totalPriceText.text = "Price: 0.00";
            paymentMethodText.text = "Payment: None";
            return;
        }

        itemsText.text = "Item: " + item.itemName;
        currentTotalPrice = item.itemPrice;
        totalPriceText.text = "Price: " + currentTotalPrice.ToString("F2");
        
        string method = (Random.value > 0.1f) ? "Cash" : "Utang"; 
        paymentMethodText.text = "Payment: " + method;

        itemsText.ForceMeshUpdate();
        totalPriceText.ForceMeshUpdate();
    }

    public void SellButton()
    {
        List<Items> playerInventory = PlayerInventory.Instance.currentItems;
        List<Items> customerOrder = CustomerSpawner.Instance.itemOrder;

        if (playerInventory.Count > 0)
        {
            Items itemToSell = playerInventory[0];

            if (customerOrder.Count > 0 && customerOrder.Contains(itemToSell))
            {
                MoneyManager.Instance.currentMoney += itemToSell.itemPrice; 
                MoneyManager.Instance.UpdateMoney();
                Debug.Log($"Sold {itemToSell.itemName} for {itemToSell.itemPrice}!");
            }
            else
            {
                Debug.Log($"Sold {itemToSell.itemName}, but customer didn't want it. No money gained.");
            }
            itemToSell.stockCount--;
            playerInventory.Remove(itemToSell);

            FinishTransaction();
        }

        else
        {
            Debug.Log("Transaction failed: Your inventory is empty!");
        }
    }

    public void DenyButton()
    {
        Debug.Log("Sale Denied.");
        FinishTransaction();
    }

    private void FinishTransaction()
    {
        sellPanel.SetActive(false);
        CustomerSpawner.Instance.ClearOrder();
    }
}