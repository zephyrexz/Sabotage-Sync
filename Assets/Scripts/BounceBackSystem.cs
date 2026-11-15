
using UnityEngine;

public class BounceBackSystem : MonoBehaviour
{
    public static BounceBackSystem Instance;

    public int maxRevives = 1;
    public int heartsAfterRevive = 3;
    public float protectionDuration = 2.5f;

    private int remainingRevives;

    private void Awake()
    {
        Instance = this;
        remainingRevives = maxRevives;
    }

    public bool TryRevive(PlayerHealth player)
    {
        if (remainingRevives <= 0)
            return false;

        remainingRevives--;

        player.Revive(
            heartsAfterRevive,
            protectionDuration
        );

        return true;
    }
}
