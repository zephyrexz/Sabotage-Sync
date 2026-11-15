
using UnityEngine;
using System.Collections;

public class DataFragmentSpawner : MonoBehaviour
{
    public static DataFragmentSpawner Instance;

    public GameObject fragmentPrefab;
    public BoxCollider2D spawnArea;

    public int maxFragments = 8;
    public float respawnDelay = 3f;

    private int currentFragments;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < maxFragments; i++)
            SpawnFragment();
    }

    public void RespawnAfterCollect()
    {
        currentFragments--;

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (GameManager.Instance.RoundEnded)
            yield break;

        SpawnFragment();
    }

    private void SpawnFragment()
    {
        Bounds area = spawnArea.bounds;

        Vector2 position = new Vector2(
            Random.Range(area.min.x, area.max.x),
            Random.Range(area.min.y, area.max.y)
        );

        Instantiate(fragmentPrefab, position, Quaternion.identity);

        currentFragments++;
    }
}
