
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int targetFragments = 20;
    public int currentFragments = 0;

    public bool RoundEnded = false;

    public GameObject winnerWindow;
    public GameObject gameOverWindow;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        winnerWindow.SetActive(false);
        gameOverWindow.SetActive(false);
    }

    public void AddFragment()
    {
        if (RoundEnded) return;

        currentFragments++;

        if (currentFragments >= targetFragments)
        {
            RoundEnded = true;
            winnerWindow.SetActive(true);
        }
    }

    public void PlayerDied()
    {
        if (RoundEnded) return;

        RoundEnded = true;
        gameOverWindow.SetActive(true);
    }
}
