using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private PuzzleButton[] correctSequence;
    [SerializeField] private ExitDoor exitDoor;

    private int progress;
    private bool solved;

    public void PressButton(PuzzleButton button)
    {
        if (solved)
            return;

        if (correctSequence == null || correctSequence.Length == 0)
        {
            Debug.LogWarning("Puzzle has no correct sequence configured.");
            return;
        }

        PuzzleButton expectedButton = correctSequence[progress];

        if (button == expectedButton)
        {
            button.SetPressed(true);

            progress++;

            Debug.Log(
                $"Correct button. Progress: {progress}/{correctSequence.Length}"
            );

            if (progress >= correctSequence.Length)
            {
                SolvePuzzle();
            }

            return;
        }

        Debug.Log("Wrong button. Puzzle reset.");
        ResetPuzzle();
    }

    private void ResetPuzzle()
    {
        progress = 0;

        foreach (PuzzleButton button in correctSequence)
        {
            if (button != null)
            {
                button.SetPressed(false);
            }
        }
    }

    private void SolvePuzzle()
    {
        solved = true;

        Debug.Log("Puzzle solved.");

        if (exitDoor != null)
        {
            exitDoor.Unlock();
        }
    }
}