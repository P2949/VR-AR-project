using UnityEngine;

public class ExitDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 openOffset =
        new Vector3(0f, 3f, 0f);

    private Vector3 closedPosition;

    private bool unlocked;
    private bool opened;

    private void Awake()
    {
        closedPosition = transform.localPosition;
    }

    public void Unlock()
    {
        unlocked = true;

        Debug.Log("Exit door unlocked.");
    }

    public void Interact()
    {
        if (opened)
            return;

        if (!unlocked)
        {
            Debug.Log("Exit door is locked.");
            return;
        }

        opened = true;

        transform.localPosition =
            closedPosition + openOffset;

        Debug.Log("Exit door opened.");
    }
}