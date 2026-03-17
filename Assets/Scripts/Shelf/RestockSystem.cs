using UnityEngine;

public class RestockSystem : MonoBehaviour
{
    private bool isPlayerInRange = false;
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Q))
        {
            Shelf shelf = GetComponentInParent<Shelf>();
            
            if(PlayerInventory.Instance != null)
            {
                shelf.RestockShelf();
            }

            if(PlayerInventory.Instance != null && PlayerInventory.Instance.currentItems.Count == 0)
            {
                Debug.Log("Nothing to Store");
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            GetComponent<SpriteRenderer>().color = Color.grey;
            Debug.Log("Player is in range to restock shelf");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Renderer renderer = GetComponent<Renderer>();
            GetComponent<SpriteRenderer>().color = Color.white;
            Debug.Log("Player left the area");
        }
    }
}
