using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Level Settings")]
    public int targetCustomers;
    public int customersServed;
    public int currentDayIndex = 0;
    private string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

    [Header("UI Elements")]
    public GameObject startDayPanel;
    public GameObject endDayButton;
    public TextMeshProUGUI quotaText; 
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI reputationGoalText;

    [Header("Reputation")]
    public float minRandomRep = 40f;
    public float maxRandomRep = 80f;
    public float requiredReputation = 0;
    private bool isReputationChallengeActive = false;

    private bool isDayRunning = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (endDayButton != null) endDayButton.SetActive(false);
        PrepareLevel();
    }

    void Update()
    {
        if (isDayRunning)
        {
            UpdateQuotaUI();
        }
    }

    void UpdateQuotaUI()
    {
        if (quotaText != null)
            quotaText.text = $"Goal: {customersServed} / {targetCustomers}";
    }

    public void PrepareLevel()
    {
        isDayRunning = false;
        customersServed = 0;
        
        if (endDayButton != null) 
        {
            endDayButton.SetActive(false);
        }
        
        targetCustomers = Random.Range(10, 31);
        dayText.text = daysOfWeek[currentDayIndex % 7];

        CheckForRandomReputationChallenge();
        UpdateQuotaUI();

        startDayPanel.SetActive(true);
    }

    public void StartDay()
    {
        isDayRunning = true;
        startDayPanel.SetActive(false);
        

        if (endDayButton != null) endDayButton.SetActive(false); 

        if (CustomerSpawner.Instance != null)
        {
            StartCoroutine(CustomerSpawner.Instance.SpawnRoutine());
        }
    }

    public void CustomerServed()
    {
        customersServed++;
        UpdateQuotaUI();

        if (customersServed >= targetCustomers)
        {
            isDayRunning = false;
            if (CustomerSpawner.Instance != null) CustomerSpawner.Instance.StopSpawning();
            
            if (endDayButton != null) endDayButton.SetActive(true);
            Debug.Log("Quota met! Showing End Day button.");
        }
    }

    void CheckForRandomReputationChallenge()
    {
        if (Random.value <= 0.10f)
        {
            isReputationChallengeActive = true;
            requiredReputation = Mathf.Round(Random.Range(minRandomRep, maxRandomRep));
            reputationGoalText.text = $"Reputation Goal: {requiredReputation}%";
        }
        else
        {
            isReputationChallengeActive = false;
            requiredReputation = 0;
            reputationGoalText.text = "No Reputation Requirement";
        }
    }

    public void EndDay()
    {
        if (isReputationChallengeActive)
        {
            int currentRep = ReputationManager.Instance.reputation;

            if (currentRep < requiredReputation)
            {
                Debug.Log($"Challenge Failed! Needed {requiredReputation}, but only had {currentRep}");
                
                ReputationManager.Instance.LoseCondition();
                return;
            }
            else 
            {
                Debug.Log("Challenge Passed! Reputation goal met.");
            }
        }

        if (endDayButton != null) endDayButton.SetActive(false);
        
        if (CustomerSpawner.Instance != null)
        {
            CustomerSpawner.Instance.ClearOrder(false); 
        }

        currentDayIndex++;
        PrepareLevel();

    }

    public void OpenPanel()
    {
        startDayPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ClosePanel()
    {
        startDayPanel.SetActive(false);
        if (isDayRunning) Time.timeScale = 1f;
    }
}