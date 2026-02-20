using UnityEngine;

public class CashierLockPosition : MonoBehaviour
{
    public Transform lockPlayer;
    private Rigidbody2D rb;
    private bool isPlayerInZone;
    void Update()
    {
        if (isPlayerInZone && Input.GetKey(KeyCode.E));
        {
            LockPlayer();
        }
    }

    void LockPlayer()
    {
        if (rb != null && lockPlayer)
        {
            rb.position = lockPlayer.position;
            rb.linearVelocity = Vector2.zero;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerInZone = true;
            rb = other.GetComponent<Rigidbody2D>();
        }
    }

    void OnDrawGizmos()
    {
        if(lockPlayer != null)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(lockPlayer.position, 2f);
        }
    }
}
