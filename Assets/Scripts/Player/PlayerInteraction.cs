using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionDistance = 6f;

    private void Awake()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.State != GameState.Playing)
        {
            return;
        }

        if (!WasInteractionPressed())
            return;

        Debug.Log("Interaction press detected.");

        TryInteract();
    }

    private bool WasInteractionPressed()
    {
        // Explicit Android/mobile touchscreen input.
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            return true;
        }

        // Desktop/editor testing.
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            return true;
        }

        return false;
    }

    private void TryInteract()
    {
        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        if (!Physics.Raycast(
                ray,
                out RaycastHit hit,
                interactionDistance))
        {
            Debug.Log("Interaction ray hit nothing.");
            return;
        }

        Debug.Log($"Interaction ray hit: {hit.collider.name}");

        IInteractable interactable =
            hit.collider.GetComponent<IInteractable>();

        if (interactable == null)
        {
            Debug.Log(
                $"Object {hit.collider.name} is not interactable."
            );

            return;
        }

        interactable.Interact();
    }
}