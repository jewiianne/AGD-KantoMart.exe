using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class SellManager : MonoBehaviour
{
    public float currentTotalPrice = 0;
    
    public GameObject sellPanel;
    public AudioClip sellSoundEffects;
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
        
        if (CustomerSpawner.Instance.itemOrder == null || CustomerSpawner.Instance.itemOrder.Count == 0)
        {
            return;
        }

        Items customerWants = CustomerSpawner.Instance.itemOrder[0];

        if (playerHas != null && playerHas.Contains(customerWants))
        {
            sellPanel.SetActive(true);
            UpdateUI(customerWants);
        }
        else
        {
            Debug.Log("You don't have the item this customer wants!");
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
                float priceLimit = (itemToSell.basePrice * 1.15f) + 0.01f;

                if (SoundManager.Instance != null)
                {
                    SoundManager.Instance.PlaySFX(sellSoundEffects);
                }

                if (itemToSell.itemPrice > priceLimit)
                {
                    Debug.Log("Too expensive!");
                    ReputationManager.Instance.ChangeReputation(-15); 
                    RemoveItemFromInventory(itemToSell);
                    FinishTransaction(); 
                    return; 
                }

                if (currentPaymentMethod == "Cash")
                {
                    MoneyManager.Instance.currentMoney += itemToSell.itemPrice; 
                    MoneyManager.Instance.UpdateMoney();
                    ReputationManager.Instance.RightItem();
                }
                else
                {
                    UtangSystem(itemToSell);  
                }
                
                RemoveItemFromInventory(itemToSell);
                FinishTransaction();
            }
            else
            {
                Debug.Log("Wrong item given!");
                ReputationManager.Instance.WrongItem();
                
                RemoveItemFromInventory(itemToSell);
                
                FinishTransaction();
            }
        }
    }

    void RemoveItemFromInventory(Items item)
    {
        item.stockCount--;
        PlayerInventory.Instance.currentItems.Remove(item);
        PlayerInventory.Instance.UpdateInventoryUI();
    }

    public void DenyButton()
    {
        Debug.Log("Sale Denied.");
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
        if (sellPanel != null) sellPanel.SetActive(false);
    
        if (CustomerSpawner.Instance != null)
        {
            CustomerSpawner.Instance.ClearOrder(true); 
        }
    }
}