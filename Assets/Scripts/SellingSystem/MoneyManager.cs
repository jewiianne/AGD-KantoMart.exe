using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public float currentMoney = 0;

    public TextMeshProUGUI MoneyText;

    public static MoneyManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentMoney = 300;
        MoneyText.text = currentMoney.ToString();
    }


    public void UpdateMoney()
    {
        MoneyText.text = currentMoney.ToString();
    }
}
