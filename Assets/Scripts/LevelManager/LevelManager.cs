using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Level Settings")]
    public int targetCustomers = 20;
    public int customersServed;
    public int currentDayIndex = 0;
    private string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    private float initializationTime;

    [Header("Timer")]
    public float openingHour = 8f;
    public float closingHour = 20f;
    public float timeMultiplier = 600f;
    private float currentInGameTime;
    private bool isDayRunning = false;

    [Header("UI Elements")]
    public GameObject startDayPanel;
    public GameObject endDayButton;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI reputationGoalText;

    [Header("Reputation")]
    public float minRandomRep = 40f;
    public float maxRandomRep = 80f;
    public float requiredReputation = 0;
    private bool isReputationChallengeActive = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PrepareLevel();
        endDayButton.SetActive(false);
    }

    void Update()
    {
        if (isDayRunning)
        {
            currentInGameTime += (Time.deltaTime / 3600f) * timeMultiplier;

            UpdateGameTimer();

            if (currentInGameTime >= closingHour)
            {
                currentInGameTime = closingHour;
                isDayRunning = false;
                CustomerSpawner.Instance.StopSpawning();
                
                if (customersServed >= targetCustomers)
                {
                    endDayButton.SetActive(true);
                }
                else 
                {
                    Debug.Log("Day ended but quota not met!");
                }
            }
        }
    }

    void UpdateGameTimer()
    {
        int hours = Mathf.FloorToInt(currentInGameTime);
        int minutes = Mathf.FloorToInt((currentInGameTime - hours) * 60);

        timerText.text = string.Format("{0:00}:{1:00}", hours, minutes);
    }

    public void PrepareLevel()
    {
        customersServed = 0;
        currentInGameTime = openingHour;
        dayText.text = daysOfWeek[currentDayIndex % 7];

        CheckForRandomReputationChallenge();
        UpdateGameTimer();

        startDayPanel.SetActive(true);
        endDayButton.SetActive(false);
    }

    public void StartDay()
    {
        isDayRunning = true;
        startDayPanel.SetActive(false);
        endDayButton.SetActive(false);

        if (CustomerSpawner.Instance != null)
        {
            StartCoroutine(CustomerSpawner.Instance.SpawnRoutine());
        }
    }

    void CheckForRandomReputationChallenge()
    {
        if (Random.value <= 0.10f)
        {
            isReputationChallengeActive = true;
            requiredReputation = Mathf.Round(Random.Range(minRandomRep, maxRandomRep));
            reputationGoalText.text = $"Goal: {requiredReputation}% Rep";
        }
        else
        {
            isReputationChallengeActive = false;
            requiredReputation = 0;
            reputationGoalText.text = "No Required Reputation to Reach";
        }
    }

    public void CustomerServed()
    {
        customersServed++;

        if (customersServed >= targetCustomers)
        {
            CustomerSpawner.Instance.StopSpawning();
            endDayButton.SetActive(true);
            Debug.Log("Quota met! You can now end the day.");
        }
    }

    public void EndDay()
    {
        endDayButton.SetActive(false);
        
        if (isReputationChallengeActive && ReputationManager.Instance.reputation < requiredReputation)
        {
            Debug.Log("Cannot proceed: Reputation too low!");
            ReputationManager.Instance.LoseCondition();
            return;
        }

        isDayRunning = false;
        endDayButton.SetActive(false);
        CustomerSpawner.Instance.ClearOrder();

        currentDayIndex++;
        string nextDay = daysOfWeek[currentDayIndex % 7];
        Debug.Log("Moving to " + daysOfWeek[currentDayIndex % 7]);

        if (nextDay == "Sunday")
        {
            Inflation.Instance.ApplySundayInflation();
        }

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
