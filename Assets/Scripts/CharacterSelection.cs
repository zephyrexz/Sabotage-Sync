
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public static int selectedCharacterIndex = 0;

    public void SelectCharacter(int index)
    {
        selectedCharacterIndex = index;
        SceneManager.LoadScene("Gameplay");
    }
}
