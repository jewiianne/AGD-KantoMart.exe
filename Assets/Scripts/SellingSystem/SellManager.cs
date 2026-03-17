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
    public string currentPaymentMethod;

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
        
        currentPaymentMethod = (Random.value > 0.1f) ? "Cash" : "Utang"; 
        paymentMethodText.text = "Payment: " + currentPaymentMethod;

        itemsText.ForceMeshUpdate();
        totalPriceText.ForceMeshUpdate();
    }

    public void SellButton()
    {
        List<Items> playerInventory = PlayerInventory.Instance.currentItems;
        Items wantedItem = CustomerSpawner.Instance.currentItem;

        if (playerInventory.Count > 0)
        {
            Items itemToSell = playerInventory[PlayerInventory.Instance.selectedSlotIndex];

            if (wantedItem != null && itemToSell == wantedItem)
            {
                if (currentPaymentMethod == "Cash")
                {
                    MoneyManager.Instance.currentMoney += itemToSell.itemPrice; 
                    MoneyManager.Instance.UpdateMoney();
                    ReputationManager.Instance.RightItem();

                    Debug.Log($"Sold {itemToSell.itemName} for {itemToSell.itemPrice}");   
                }

                else
                {
                    UtangSystem(itemToSell);  
                }
            }

            else
            {
                ReputationManager.Instance.WrongItem();
            }
            
            itemToSell.stockCount--;
            playerInventory.Remove(itemToSell);
            PlayerInventory.Instance.UpdateInventoryUI();

            FinishTransaction();
        }
    }

    public void DenyButton()
    {
        ReputationManager.Instance.DenySale();
        FinishTransaction();
    }

    void UtangSystem(Items itemToSell)
    {
        if (Random.value > 0.5f)
        {
            MoneyManager.Instance.currentMoney += itemToSell.itemPrice; 
            MoneyManager.Instance.UpdateMoney();
            ReputationManager.Instance.UtangAddReputation();
        }

        else
        {
            ReputationManager.Instance.UtangMinusReputation();    
        }
    }

    private void FinishTransaction()
    {
        sellPanel.SetActive(false);
        CustomerSpawner.Instance.ClearOrder();
    }
}