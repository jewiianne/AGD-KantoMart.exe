using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    private Rigidbody2D rb;
    private Vector2 input;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.y);
        animator.SetFloat("Speed", input.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed; 
    }
}
