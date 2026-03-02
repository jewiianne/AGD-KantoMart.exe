using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerSpawner : MonoBehaviour
{
    public List<CustomerTraits> availableCustomers; 
    public Transform spawnPoint;
    public float spawnInterval = 5f;
    public float checkInterval = 1f;

    private GameObject currentCustomer;
    private bool isSpawning = true;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private System.Collections.IEnumerator SpawnRoutine()
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
        if (availableCustomers.Count == 0 || spawnPoint == null) return;

        int randomIndex = Random.Range(0, availableCustomers.Count);
        CustomerTraits selectedData = availableCustomers[randomIndex];

        if (selectedData.customerPrefabs != null && selectedData.customerPrefabs.Count > 0)
        {
            int randomPrefabIndex = Random.Range(0, selectedData.customerPrefabs.Count);
            GameObject prefabToSpawn = selectedData.customerPrefabs[randomPrefabIndex];

            currentCustomer = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

            Debug.Log($"A {selectedData.customerName} has entered the mart!");
        }
    }
} 