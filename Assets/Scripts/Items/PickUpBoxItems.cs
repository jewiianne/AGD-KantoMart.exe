using UnityEngine;

public class PickUpBoxItems : MonoBehaviour
{
    public BoxItems boxItem;
    private bool isPlayerInRange = false;

     void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerInventory.Instance != null && boxItem != null)
            {
                int itemsAdded = 0;
                int totalToReceive = boxItem.boxItemInsideCount;

                for (int i = 0; i < totalToReceive; i++)
                {
                    if (PlayerInventory.Instance.CanReceiveItem())
                    {
                        PlayerInventory.Instance.ReceiveItem(boxItem.itemData);
                        itemsAdded++;
                    }
                    else
                    {
                        Debug.Log("Picked up " + itemsAdded + "x " + boxItem.boxItemName);
                        break;
                    }
                }

                if (itemsAdded > 0)
                {
                    Debug.Log("Picked up " + itemsAdded + "x " + boxItem.boxItemName);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.color = Color.grey;
            Debug.Log("Player is in range to take item");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.color = Color.white;
            Debug.Log("Player left the area");
        }
    }
}
