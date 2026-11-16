
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject interactionPrompt;
    public QuizManager quizManager;

    private PCInteractable nearbyPC;

    void Start()
    {
        interactionPrompt.SetActive(false);
    }

    void Update()
    {
        if (nearbyPC != null && !nearbyPC.isCompleted)
        {
            interactionPrompt.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                quizManager.StartQuiz(nearbyPC, this);
            }
        }
        else
        {
            interactionPrompt.SetActive(false);
        }
    }

    public void SetNearbyPC(PCInteractable pc)
    {
        nearbyPC = pc;
    }

    public void RemoveNearbyPC(PCInteractable pc)
    {
        if (nearbyPC == pc)
        {
            nearbyPC = null;
        }
    }

    public void SetControl(bool active)
    {
        PlayerMovement movement =
            GetComponent<PlayerMovement>();

        if (movement != null)
        {
            movement.enabled = active;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null && !active)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
