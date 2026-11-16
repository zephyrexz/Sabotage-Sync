
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] characters;
    public Transform spawnPoint;

    void Start()
    {
        int index = CharacterSelection.selectedCharacterIndex;

        if (index < 0 || index >= characters.Length)
        {
            Debug.LogError("Index karakter tidak valid!");
            return;
        }

        Instantiate(
            characters[index],
            spawnPoint.position,
            Quaternion.identity
        );
    }
}
