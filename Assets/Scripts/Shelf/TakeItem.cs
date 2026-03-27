using UnityEngine;
using System.Collections.Generic;

public class TakeItem : MonoBehaviour
{
    public Shelf shelf;
    public AudioClip pickupSound;
    private bool isPlayerInRange = false;

    void Start()
    {
        shelf = GetComponentInParent<Shelf>();
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (shelf != null && shelf.itemInStock.Count > 0)
            {
                if (PlayerInventory.Instance.CanReceiveItem())
                {
                    Items itemToReceive = shelf.itemInStock[0];
                    PlayerInventory.Instance.ReceiveItem(itemToReceive);
                    shelf.RemoveItem();

                    if (SoundManager.Instance != null)
                    {
                        SoundManager.Instance.PlaySFX(pickupSound);
                    }
                }

                else
                {
                    Debug.Log("Inventory is full!");
                }
            }
            else 
            {
                Debug.Log("The shelf is empty");
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            // Renderer renderer = GetComponentInParent<Renderer>();
            // renderer.material.color = Color.grey;
            Debug.Log("Player is in range to take item");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            // Renderer renderer = GetComponentInParent<Renderer>();
            // renderer.material.color = Color.white;
            Debug.Log("Player left the area");
        }
    }
}
