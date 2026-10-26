using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState State { get; private set; } = GameState.Playing;

    [Header("UI")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject interactionReticle;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        State = GameState.Playing;

        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        if (interactionReticle != null)
        {
            interactionReticle.SetActive(true);
        }
    }

    public void WinGame()
    {
        if (State != GameState.Playing)
            return;

        State = GameState.Won;

        Debug.Log("Game won.");

        if (interactionReticle != null)
        {
            interactionReticle.SetActive(false);
        }

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

    public void LoseGame()
    {
        if (State != GameState.Playing)
            return;

        State = GameState.Lost;

        Debug.Log("Game lost.");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}