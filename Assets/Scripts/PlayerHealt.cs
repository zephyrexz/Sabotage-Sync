
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHearts = 3;
    public int currentHearts;

    public Transform respawnPoint;

    private Rigidbody2D rb;

    private bool invulnerable;
    private bool isDead;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHearts = maxHearts;
    }

    public void TakeHit(int damage = 1)
    {
        if (isDead || invulnerable || GameManager.Instance.RoundEnded)
            return;

        currentHearts -= damage;

        if (currentHearts <= 0)
        {
            if (BounceBackSystem.Instance.TryRevive(this))
                return;

            isDead = true;
            GameManager.Instance.PlayerDied();
        }
    }

    public void Revive(int hearts, float protection)
    {
        currentHearts = hearts;

        if (respawnPoint != null)
            rb.position = respawnPoint.position;

        rb.linearVelocity = Vector2.zero;

        StartCoroutine(Protection(protection));
    }

    private System.Collections.IEnumerator Protection(float duration)
    {
        invulnerable = true;
        yield return new WaitForSeconds(duration);
        invulnerable = false;
    }
}
