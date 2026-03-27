using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ReputationManager : MonoBehaviour
{
    public Button denyButton;
    public Slider reputationSlider;
    public GameObject loseUIPanel;
    public TextMeshProUGUI reputationChangeText;
    public GameObject wasteWarningUI;

    public int reputation;
    public int maxReputation = 100;

    public static ReputationManager Instance;

    void Awake()
    {
        Instance = this;
        wasteWarningUI.SetActive(false);
    }

    void Start()
    {
        reputation = 100;
        reputationSlider.maxValue = maxReputation;
        reputationSlider.value = reputation;
        loseUIPanel.SetActive(false);

        if (denyButton != null)
        {
            denyButton.onClick.RemoveAllListeners();
            denyButton.onClick.AddListener(DenySale);
        }
    }

    void Update()
    {
        DelayOrder();
        reputationSlider.value = reputation;
    }

    public void DenySale()
    {
        var spawner = CustomerSpawner.Instance;
        if(spawner == null || spawner.currentCustomer == null || spawner.currentItem == null) return;

        bool isStudent = spawner.currentCustomer.CompareTag("Student");
        bool isCigarette = spawner.currentItem.itemName == "Cigarettes";

        if (isStudent && isCigarette)
        {
            ChangeReputation(5, "Denied cigarettes for student! +5 Rep");
        }
        else
        {
            ChangeReputation(-5, "Sale denied! -5 Rep");
        }

        spawner.ClearOrder(true);
    }

    public void RightItem()
    {
        ChangeReputation(3, "Correct order! +3 Rep");
    }

    public void WrongItem()
    {
        ChangeReputation(-5, "Wrong item! Customer unhappy. -5 Rep");
    }

    public void UtangMinusReputation()
    {
        ChangeReputation(-15, "Customer didn't pay debt! -15 Rep");
    }

    public void UtangAddReputation()
    {
        ChangeReputation(10, "Customer paid their debt! +10 Rep");
    }

    public void ItemWastedPenalty()
    {
        ChangeReputation(-10, "Item wasted! -10 Rep");
        StopCoroutine(ShowWasteUI()); 
        StartCoroutine(ShowWasteUI());
    }

    void DelayOrder()
    {
        var spawner = CustomerSpawner.Instance;
        if (spawner.isDelayTime && spawner.currentCustomer != null)
        {
            float timeWaiting = Time.time - spawner.lastSpawnTime;
            if (timeWaiting >= 10f)
            {
                spawner.isDelayTime = false; 
                ChangeReputation(-10, "Customer lost patience! -10 Rep");
                ClearCustomer();
            }
        }
    }

    public void ChangeReputation(int amount, string message)
    {
        reputation += amount;
        reputation = Mathf.Clamp(reputation, 0, maxReputation);
        
        Debug.Log(message + " | Current: " + reputation);
        UpdateReputationUI();

        if (reputationChangeText != null)
        {
            reputationChangeText.text = message;
            
            reputationChangeText.color = amount > 0 ? Color.green : Color.red;
            
            StopCoroutine("ShowReputationFeedback");
            StartCoroutine(ShowReputationFeedback());
        }
    }

    public void ChangeReputation(int amount) 
    {
        ChangeReputation(amount, (amount > 0 ? "+" : "") + amount + " Reputation");
    }

    private IEnumerator ShowReputationFeedback()
    {
        reputationChangeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        reputationChangeText.gameObject.SetActive(false);
    }

    void UpdateReputationUI()
    {
        reputationSlider.value = reputation;
        if (reputation <= 0) LoseCondition();
    }

    void ClearCustomer()
    {
        var spawner = CustomerSpawner.Instance;
        if (spawner.orderPanel != null) spawner.orderPanel.SetActive(false);
        if(spawner.currentCustomer != null)
        {
            Destroy(spawner.currentCustomer);
            spawner.currentCustomer = null;
        }
        spawner.ClearOrder(true);
    }

    public void LoseCondition()
    {
        Time.timeScale = 0f;
        loseUIPanel.SetActive(true);
    }

    private IEnumerator ShowWasteUI()
    {
        if (wasteWarningUI != null)
        {
            wasteWarningUI.SetActive(true);
            yield return new WaitForSeconds(5f);
            wasteWarningUI.SetActive(false);
        }
    }
}