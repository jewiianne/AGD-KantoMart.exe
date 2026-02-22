using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class SellManager : MonoBehaviour
{
    public List<Items> availableItems;

    public float currentTotalPrice = 0;

    public TextMeshProUGUI itemsText;
    public TextMeshProUGUI totalPriceText;
    public TextMeshProUGUI paymentMethodText;

    void Start()
    {
        if (availableItems != null && availableItems.Count > 0)
        {
            DisplayItems(1);
            PaymentMethod();
        }
    }

    void DisplayItems(int index)
    {
        Items selected = availableItems[index];
        itemsText.text = "Items: " + selected.itemName;

        currentTotalPrice = selected.itemPrice;
        totalPriceText.text = "Total Price: " + currentTotalPrice.ToString();
    }

    void PaymentMethod()
    {
        string method = (Random.value > 0.01f)? "Cash" : "Utang";
        paymentMethodText.text = "Payment Method: " + method;
    }

    public IEnumerator ResetDisplayItems()
    {
        yield return new WaitForSeconds(1.5f);
        itemsText.text = "Items: None";
        totalPriceText.text = "Total Price: 0";
        paymentMethodText.text = "Payment Method: None";
        currentTotalPrice = 0;
    }
}