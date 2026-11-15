using UnityEngine;
using UnityEngine.UI;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject[] studentPrefabs;
    public Transform spawnPoint;

    public Text heartText;
    public GameManager gameManager;

    void Start()
    {
        // Periksa prefab
        if (studentPrefabs == null || studentPrefabs.Length == 0)
        {
            Debug.LogError("Student Prefabs belum diisi!");
            return;
        }

        // Periksa SpawnPoint
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn Point belum dihubungkan!");
            return;
        }

        // Ambil karakter pilihan
        int selected = PlayerPrefs.GetInt("SelectedCharacter", 0);

        // Amankan index
        if (selected < 0 || selected >= studentPrefabs.Length)
        {
            Debug.LogWarning("Index karakter tidak valid. Menggunakan karakter pertama.");
            selected = 0;
        }

        // Periksa prefab
        if (studentPrefabs[selected] == null)
        {
            Debug.LogError("Prefab karakter pada index " + selected + " kosong!");
            return;
        }

        // Spawn player
        GameObject player = Instantiate(
            studentPrefabs[selected],
            spawnPoint.position,
            spawnPoint.rotation
        );

        // Hubungkan komponen player
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        BounceBackSystem bounce = player.GetComponent<BounceBackSystem>();

        if (health != null)
        {
            health.gameManager = gameManager;
            health.heartText = heartText;
            health.bounceBack = bounce;
        }

        if (bounce != null)
        {
            bounce.playerHealth = health;
            bounce.spawnPoint = spawnPoint;
        }

        Debug.Log("Player berhasil di-spawn: " + player.name);
    }
}
