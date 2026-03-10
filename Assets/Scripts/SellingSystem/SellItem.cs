using UnityEngine;

public class SellItem : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public GameObject sellPanel;
    public float detectionRadius = 1.0f;

    public LayerMask customerLayer;

    private bool isPlayerNearWithCustomer = false;

    void Start()
    {
        sellPanel.SetActive(false);
    }

    void Update()
    {
        Vector2 facingDirection = transform.up;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.5f, facingDirection, detectionRadius, customerLayer);
        isPlayerNearWithCustomer = hit.collider != null;

        if (isPlayerNearWithCustomer)
        {
            //Debug.Log("Player is near with customer.");
            if (Input.GetKeyDown(KeyCode.G))
            {
                sellPanel.SetActive(true);
            }
        }
        else
        {
            if (sellPanel.activeSelf && !isPlayerNearWithCustomer)
            {
                sellPanel.SetActive(false);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 facingDirection = transform.up;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + (facingDirection * detectionRadius));
    }
}