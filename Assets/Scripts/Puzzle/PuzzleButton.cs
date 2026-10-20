using UnityEngine;

public class PuzzleButton : MonoBehaviour, IInteractable
{
    [SerializeField] private PuzzleManager puzzleManager;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void Interact()
    {
        if (puzzleManager == null)
        {
            Debug.LogWarning($"{name} has no PuzzleManager assigned.");
            return;
        }

        puzzleManager.PressButton(this);
    }

    public void SetPressed(bool pressed)
    {
        transform.localScale = pressed
            ? originalScale * 0.8f
            : originalScale;
    }
}