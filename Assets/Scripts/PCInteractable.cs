
using UnityEngine;

public class PCInteractable : MonoBehaviour
{
    public bool isCompleted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerInteraction player =
            other.GetComponent<PlayerInteraction>();

        if (player != null && !isCompleted)
        {
            player.SetNearbyPC(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerInteraction player =
            other.GetComponent<PlayerInteraction>();

        if (player != null)
        {
            player.RemoveNearbyPC(this);
        }
    }

    public void CompletePC()
    {
        isCompleted = true;
    }
}
