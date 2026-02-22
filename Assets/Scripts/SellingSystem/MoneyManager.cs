using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public SellManager sellManagerPrice;

    public float currentMoney = 0;

    public TextMeshProUGUI MoneyText;
    public Button sellButton;
    public Button denyButton;

    void Start()
    {
        currentMoney = 100;
        MoneyText.text = currentMoney.ToString();

        sellButton.onClick.AddListener(SellButton);
    }

    void Update()
    {
        
    }

    public void SellButton()
    {
        SellManager selected = sellManagerPrice;
        currentMoney += sellManagerPrice.currentTotalPrice;
        MoneyText.text = currentMoney.ToString();

        sellManagerPrice.currentTotalPrice = 0;

        StartCoroutine (sellManagerPrice.ResetDisplayItems());
    }

    public void DenyButton()
    {
        StartCoroutine (sellManagerPrice.ResetDisplayItems());
    }
}
