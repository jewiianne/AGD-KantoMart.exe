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

    public void CheckPriceFairness()
    {
        var spawner = CustomerSpawner.Instance;
        if (spawner == null || spawner.currentItem == null) return;

        float fairPrice = spawner.currentItem.itemPrice;

        if (spawner.currentItem.itemPrice > (spawner.currentItem.basePrice * 1.15f)) 
        {
            Debug.Log("Customer noticed overpricing! Leaving...");

            ChangeReputation(-15); 

            ClearCustomer();
            spawner.ClearOrder();
        }
    }

    public void DenySale()
    {
        var spawner = CustomerSpawner.Instance;

        if(spawner == null || spawner.currentCustomer == null || spawner.currentItem == null)
        {
            return;
        }

        bool isStudent = spawner.currentCustomer.CompareTag("Student");
        bool isCigarette = spawner.currentItem.itemName == "Cigarettes";

        if (isStudent && isCigarette)
        {
            ChangeReputation(5);
            Debug.Log("Denied cigarettes for student, +5 in Reputation, Current Reputation " + reputation);
        }

        else
        {
            ChangeReputation(-5);
            Debug.Log("Sale denied, -5 in Reputation, Current Reputation " + reputation);
        }

        UpdateReputationUI();
        spawner.ClearOrder();
    }

    public void RightItem()
    {
        ChangeReputation(3);
        Debug.Log("Correct order, +3 in Reputation, Current Reputation " + reputation);
    }

    public void WrongItem()
    {
        ChangeReputation(-5);
        Debug.Log("Customer doesn't want the item, -5 in Reputation, Current Reputation " + reputation);
    }

    public void UtangMinusReputation()
    {
        ChangeReputation(-15);
        Debug.Log("Customer didn't paid their debt, -15 in Reputation, Current Reputation " + reputation);
    }

    public void UtangAddReputation()
    {
        ChangeReputation(+10);
        Debug.Log("Customer paid their debt, +10 in Reputation, Current Reputation " + reputation);
    }

    void DelayOrder()
    {
        var spawner = CustomerSpawner.Instance;

        if (spawner.isDelayTime && spawner.currentCustomer != null)
        {
            float timeWaiting = Time.time - spawner.lastSpawnTime;

            if (timeWaiting >= 10f)
            {
                ChangeReputation(-10);
                Debug.Log("Customer lost patience, -10 in Reputation");

                spawner.isDelayTime = false; 
                
                ClearCustomer();
                UpdateReputationUI();
            }
        }
        else 
        {
            spawner.isDelayTime = false;
        }
    }

    public void ItemWastedPenalty()
    {
        ChangeReputation(-10);
        Debug.Log("Item wasted! -10 Reputation. Current: " + reputation);
        UpdateReputationUI();
        
        StopAllCoroutines(); 
        StartCoroutine(ShowWasteUI());
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
    void ClearCustomer()
    {
        var spawner = CustomerSpawner.Instance;
        spawner.orderPanel.SetActive(false);
        if(spawner.currentCustomer != null)
        {
            Destroy(spawner.currentCustomer);
            spawner.currentCustomer = null;
        }
    }

    public void LoseCondition()
    {
        
        Time.timeScale = 0f;

        Debug.Log("You lose, You Didn't reached the requirements of " + LevelManager.Instance.requiredReputation + " Reputation, Current Reputation " + reputation);
        loseUIPanel.SetActive(true);
    }

    void UpdateReputationUI()
    {
        reputation = Mathf.Clamp(reputation, 0, maxReputation);
        reputationSlider.value = reputation;

        if (reputation <= 0)
        {
            LoseCondition();
        }
    }

    public void ChangeReputation(int amount)
    {
        reputation += amount;
        reputation = Mathf.Clamp(reputation, 0, maxReputation);
        
        UpdateReputationUI();

        if (reputationChangeText != null)
        {
            string prefix = amount > 0 ? "+" : "";
            reputationChangeText.text = $"{prefix}{amount}";
            
            reputationChangeText.color = amount > 0 ? Color.black : Color.red;
            
            StopCoroutine("ShowReputationFeedback");
            StartCoroutine(ShowReputationFeedback());
        }
    }

    private IEnumerator ShowReputationFeedback()
    {
        reputationChangeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        reputationChangeText.gameObject.SetActive(false);
    }
}