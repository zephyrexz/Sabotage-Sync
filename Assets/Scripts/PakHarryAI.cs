
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PakHarryAI : MonoBehaviour
{
    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.2f;

    [Header("Detection")]
    public float detectionRange = 6f;
    public float attackRange = 1.25f;

    [Header("Attack")]
    public float attackCooldown = 2f;
    private float nextAttackTime;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    private int currentPoint;

    private Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    public void SetPlayer(Transform target)
    {
        player = target;
    }

    private void Update()
    {
        if (GameManager.Instance.RoundEnded)
        {
            movement = Vector2.zero;
            return;
        }

        if (player == null)
        {
            Patrol();
            return;
        }

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distance <= attackRange)
        {
            movement = Vector2.zero;
            Attack();
        }
        else if (distance <= detectionRange)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(
            rb.position +
            movement * Time.fixedDeltaTime
        );
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0)
        {
            movement = Vector2.zero;
            return;
        }

        Transform target = patrolPoints[currentPoint];

        movement = GetDirection(target.position) * patrolSpeed;

        if (Vector2.Distance(transform.position, target.position) < 0.15f)
        {
            currentPoint++;

            if (currentPoint >= patrolPoints.Length)
                currentPoint = 0;
        }
    }

    private void Chase()
    {
        movement = GetDirection(player.position) * chaseSpeed;
    }

    private void Attack()
    {
        if (Time.time < nextAttackTime)
            return;

        PlayerHealth health = player.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.TakeHit(1);
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private Vector2 GetDirection(Vector2 target)
    {
        Vector2 difference = target - rb.position;

        if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
            return new Vector2(Mathf.Sign(difference.x), 0);

        if (Mathf.Abs(difference.y) > 0)
            return new Vector2(0, Mathf.Sign(difference.y));

        return Vector2.zero;
    }
}
