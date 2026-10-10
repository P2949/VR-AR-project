using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    private bool activated;
    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void Interact()
    {
        activated = !activated;

        transform.localScale = activated
            ? originalScale * 1.5f
            : originalScale;

        Debug.Log($"{name} interacted with.");
    }
}