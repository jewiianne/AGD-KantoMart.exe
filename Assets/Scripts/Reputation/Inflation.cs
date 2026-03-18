using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Inflation : MonoBehaviour
{
    [Header("UI Price Labels")]
    public TextMeshProUGUI blueChipsPrice;
    public TextMeshProUGUI redChipsPrice;
    public TextMeshProUGUI redBiscuitPrice;
    public TextMeshProUGUI yellowBiscuitPrice;
    public TextMeshProUGUI cigarettesPrice;

    public List<BoxItems> items;
    public static Inflation Instance;

    void Awake()
    {
        Instance = this;
    }

    public void ApplySundayInflation()
    {
        foreach (BoxItems box in items)
        {
            box.boxItemPrice = Mathf.Round(box.boxItemPrice * 1.10f);

            if (box.itemData != null) 
            {
                box.itemData.itemPrice = Mathf.Round(box.itemData.itemPrice * 1.10f);
            }
        }

        UpdatePriceUI();
        Debug.Log("Sunday Inflation: Restock costs and Selling prices increased by 10%.");
    }

    public void UpdatePriceUI()
    {
        blueChipsPrice.text = GetPriceString("BlueChips");
        redChipsPrice.text = GetPriceString("RedChips");
        redBiscuitPrice.text = GetPriceString("RedBiscuit");
        yellowBiscuitPrice.text = GetPriceString("YellowBiscuit");
        cigarettesPrice.text = GetPriceString("Cigarettes");
    }

    private string GetPriceString(string boxItemName)
    {
        BoxItems item = items.Find(x => x.boxItemName == boxItemName);
        if (item != null)
        {
            return $"5 pcs for  {item.boxItemPrice} pesos";
        }
        return "N/A";
    }

}
