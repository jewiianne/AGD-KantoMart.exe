using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CustomerSpawner : MonoBehaviour
{
    [Header("References")]
    public List<CustomerTraits> availableCustomers;
    public List<Items> itemOrder;
    public Items currentItem;

    [Header("Spawner Settings")]
    public Transform spawnPoint;
    public float spawnInterval = 5f;
    public float checkInterval = 1f;

    [Header("UI References")]
    public GameObject orderPanel;
    public Image itemPrefab;
    public GameObject currentCustomer;
    
    private bool isSpawning = true;
    public float lastSpawnTime;
    public bool isDelayTime = false;
    private float initializationTime;
    public static CustomerSpawner Instance;

    void Awake()
    {
        Instance = this;
        initializationTime = Time.time;
    }
    
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (isSpawning)
        {
            if (currentCustomer == null)
            {
                 SpawnRandomCustomer();
            }
            else
            {
                Debug.Log("Store is currently full");
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SpawnRandomCustomer()
    {
        if (availableCustomers.Count == 0 || spawnPoint == null)
        {
            return;
        }

        int randomIndex = Random.Range(0, availableCustomers.Count);
        CustomerTraits selectedData = availableCustomers[randomIndex];

        if (selectedData.customerPrefabs != null && selectedData.customerPrefabs.Count > 0)
        {
            int randomPrefabIndex = Random.Range(0, selectedData.customerPrefabs.Count);
            GameObject prefabToSpawn = selectedData.customerPrefabs[randomPrefabIndex];

            currentCustomer = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
            lastSpawnTime = Time.time;
            isDelayTime = true;

            CustomerOrder(selectedData);

            Debug.Log($"A {selectedData.customerType} has entered the mart!");
        }
    }

    public void CustomerOrder(CustomerTraits traits)
    {
        if (itemOrder.Count == 0 || orderPanel == null)
        {
            return;
        }

        int randomIndex = Random.Range(0, itemOrder.Count);
        currentItem = itemOrder[randomIndex];

        if (orderPanel != null)
        {
            orderPanel.SetActive(true);
        }
        
        if (itemPrefab != null && currentItem.itemSprite != null)
        {
            itemPrefab.sprite = currentItem.itemSprite;
        }

        if (SellManager.Instance != null)
        {
            SellManager.Instance.UpdateUI(currentItem);
        }

        Debug.Log(traits.customerType + " ordered: " + currentItem.itemName);

    }

    public void ClearOrder()
    {
        orderPanel.SetActive(false);
        if(currentCustomer != null)
        {
            Destroy(currentCustomer);
            currentCustomer = null;
        }
    }
} 