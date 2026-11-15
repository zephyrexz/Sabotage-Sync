
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public Animator animator;

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.RoundEnded)
        {
            movement = Vector2.zero;
            UpdateAnimation();
            return;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(x) > Mathf.Abs(y))
            movement = new Vector2(Mathf.Sign(x), 0);
        else if (y != 0)
            movement = new Vector2(0, Mathf.Sign(y));
        else
            movement = Vector2.zero;

        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.RoundEnded) return;

        rb.MovePosition(
            rb.position + movement * moveSpeed * Time.fixedDeltaTime
        );
    }

    private void UpdateAnimation()
    {
        animator.SetBool("ifLeft", movement.x < 0);
        animator.SetBool("ifRight", movement.x > 0);
        animator.SetBool("ifUp", movement.y > 0);
        animator.SetBool("ifDown", movement.y < 0);
    }
}
