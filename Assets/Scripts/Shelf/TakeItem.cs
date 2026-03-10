using UnityEngine;
using System.Collections.Generic;

public class TakeItem : MonoBehaviour
{
    public Shelf shelf;
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
                    Items itemToGive = shelf.itemInStock[0];
                    PlayerInventory.Instance.ReceiveItem(itemToGive);
                    shelf.RemoveItem();
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
            Debug.Log("Player is in range to take item");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player left the area");
        }
    }
}
