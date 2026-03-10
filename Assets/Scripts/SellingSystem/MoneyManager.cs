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
        currentMoney = 100;
        MoneyText.text = currentMoney.ToString();
    }

    void Update()
    {
        
    }

    public void UpdateMoney()
    {
        SellManager selected = sellManagerPrice;
        currentMoney += sellManagerPrice.currentTotalPrice;
        MoneyText.text = currentMoney.ToString();

        sellManagerPrice.currentTotalPrice = 0;
    }

    // public void DenyButton()
    // {
    //     StartCoroutine (sellManagerPrice.ResetDisplayItems());
    // }
}
