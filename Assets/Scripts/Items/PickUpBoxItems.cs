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
                if (PlayerInventory.Instance.CanReceiveBox())
                {
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
