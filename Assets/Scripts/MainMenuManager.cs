using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string characterSelectionScene =
        "CharacterSelection";

    public void Play()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            characterSelectionScene
        );
    }

    public void Quit()
    {
        Application.Quit();

        Debug.Log("Game ditutup.");
    }
}