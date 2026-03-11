using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public SellManager sellManagerPrice;

    public float currentMoney = 0;

    public TextMeshProUGUI MoneyText;

    public static MoneyManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentMoney = 80;
        MoneyText.text = currentMoney.ToString();
    }


    public void UpdateMoney()
    {
        SellManager selected = sellManagerPrice;
        currentMoney += sellManagerPrice.currentTotalPrice;
        MoneyText.text = currentMoney.ToString();

        sellManagerPrice.currentTotalPrice = 0;
    }
}
