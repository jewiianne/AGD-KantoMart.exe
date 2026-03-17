using UnityEngine;
using UnityEngine.UI;

public class ReputationManager : MonoBehaviour
{
    public Button denyButton;
    public Slider reputationSlider;
    public GameObject loseUIPanel;

    public int reputation;
    public int maxReputation = 100;

    public static ReputationManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        reputation = 80;
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

        if(spawner == null || spawner.currentCustomer == null || spawner.currentItem == null)
        {
            return;
        }

        bool isStudent = spawner.currentCustomer.CompareTag("Student");
        bool isCigarette = spawner.currentItem.itemName == "Cigarettes";

        if (isStudent && isCigarette)
        {
            reputation += 5;
            Debug.Log("Denied cigarettes for student, +5 in Reputation, Current Reputation " + reputation);
        }

        else
        {
            reputation -= 5;
            Debug.Log("Sale denied, -5 in Reputation, Current Reputation " + reputation);
        }

        UpdateReputationUI();
        spawner.ClearOrder();
    }

    public void RightItem()
    {
        reputation += 3;
        Debug.Log("Correct order, +3 in Reputation, Current Reputation " + reputation);
    }

    public void WrongItem()
    {
        reputation -= 5;
        Debug.Log("Customer doesn't want the item, -5 in Reputation, Current Reputation " + reputation);
    }

    public void UtangMinusReputation()
    {
        reputation -= 15;
        Debug.Log("Customer didn't paid their debt, -15 in Reputation, Current Reputation " + reputation);
    }

    public void UtangAddReputation()
    {
        reputation += 10;
        Debug.Log("Customer paid their debt, +10 in Reputation, Current Reputation " + reputation);
    }

    void DelayOrder()
    {
        var delayTime = CustomerSpawner.Instance;
        float timeWaiting = Time.time - delayTime.lastSpawnTime;

        if(delayTime.isDelayTime && timeWaiting >= 10f)
        {
            reputation -= 10;
            Debug.Log("Customer lost patience, -10 in Reputation, Current Reputation " + reputation);

            delayTime.isDelayTime = false;
            
            ClearCustomer();
            UpdateReputationUI();
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
}