using UnityEngine;
using System.Collections; // Needed for Coroutines

public class PickUpBoxItems : MonoBehaviour
{
    public BoxItems boxItem;
    private bool isPlayerInRange = false;
    
    private float despawnTime = 15f;
    private bool wasPickedUp = false;

    void Start()
    {
        Invoke("ExpireItem", despawnTime);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerInventory.Instance != null && boxItem != null)
            {
                if (PlayerInventory.Instance.CanReceiveBox())
                {
                    wasPickedUp = true;
                    CancelInvoke("ExpireItem"); 

                    PlayerInventory.Instance.ReceiveBox(boxItem);
                    Debug.Log("Picked up the box: " + boxItem.boxItemName);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Inventory full, cannot carry box!");
                }
            }
        }
    }

    void ExpireItem()
    {
        if (!wasPickedUp)
        {
            Debug.Log(boxItem.boxItemName + " expired and was removed!");
            
            if (ReputationManager.Instance != null)
            {
                ReputationManager.Instance.ItemWastedPenalty();
            }

            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Renderer renderer = GetComponent<Renderer>();
            if(renderer != null) renderer.material.color = Color.grey;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Renderer renderer = GetComponent<Renderer>();
            if(renderer != null) renderer.material.color = Color.white;
        }
    }
}