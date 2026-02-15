using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2 (1f, 0f) * Time.deltaTime);
    }
}
