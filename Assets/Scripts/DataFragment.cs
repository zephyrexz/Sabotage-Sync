
using UnityEngine;

public class DataFragment : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponentInParent<PlayerHealth>();

        if (player == null) return;

        if (GameManager.Instance.RoundEnded) return;

        GameManager.Instance.AddFragment();

        DataFragmentSpawner.Instance.RespawnAfterCollect();

        Destroy(gameObject);
    }
}
