using UnityEngine;
using System.Collections.Generic;

public class TakeItem : MonoBehaviour
{
    public Shelf shelf;
    public List<Items> itemInStock;
    public int itemInInventory = 0;

    private bool isPlayerInRange = false;
    public static TakeItem Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        shelf = GetComponentInParent<Shelf>();
    }

    void Update()
    {
        if (isPlayerInRange && shelf.stockCount > 0 && Input.GetKeyDown(KeyCode.E))
        {
            itemInInventory++;
            shelf.RemoveItem();
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

    public void AddItem(Items newItem)
    {
        itemInStock.Add(newItem);
    }
}
