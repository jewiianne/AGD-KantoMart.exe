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
        reputation = 70;
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
    
        LoseCondition();
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
            reputation += 3;
            Debug.Log("Denied Cigarettes for Student, Current Reputation" + reputation);
        }

        else
        {
            reputation -= 5;
            Debug.Log("Sale Denied, Current Reputation" + reputation);
        }

        UpdateReputationUI();
        spawner.ClearOrder();
    }

    void UpdateReputationUI()
    {
        reputation = Mathf.Clamp(reputation, 0, maxReputation);
        reputationSlider.value = reputation;
    }

    void DelayOrder()
    {
        var delayTime = CustomerSpawner.Instance;
        float timeWaiting = Time.time - delayTime.lastSpawnTime;

        if(delayTime.isDelayTime && timeWaiting >= 10f)
        {
            reputation -= 10;
            Debug.Log("Customer lost patience, -10 in reputation");

            delayTime.isDelayTime = false;
            
            delayTime.ClearOrder();
        }
    }

    void LoseCondition()
    {
        if (reputation == 0)
        {
            Time.timeScale = 0f;

            Debug.Log("You lose");
            loseUIPanel.SetActive(true);
        }
    }
}