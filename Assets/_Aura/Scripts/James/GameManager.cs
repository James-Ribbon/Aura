using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Menu, Playing, Paused, GameOver }
    public GameState CurrentState { get; private set; }

    [SerializeField] private int currentRecallOrbs;
    //private int highScore;
    private int currentLevel = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeGame()
    {
        //Load player information if needed

        CurrentState = GameState.Menu;
    }

    private void Start()
    {
        if(Instance == this)
        {
            EventManager.RecallOrbCollected += UpdateNumberOfRecallOrbs;
        }
    }

    void UpdateNumberOfRecallOrbs()
    {
        currentRecallOrbs += 1;
    }

    private void OnDisable()
    {
        if (Instance == this)
        {
            EventManager.RecallOrbCollected -= UpdateNumberOfRecallOrbs;
        }
    }

    public void StartGame()
    {
        CurrentState = GameState.Playing;
        currentRecallOrbs = 0;
        SceneManager.LoadScene("Level1");
    }

    public void PauseGame()
    {
        if (CurrentState == GameState.Playing)
        {
            CurrentState = GameState.Paused;
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()
    {
        if (CurrentState == GameState.Paused)
        {
            CurrentState = GameState.Playing;
            Time.timeScale = 1f;
        }
    }

    public void GameOver()
    {
        CurrentState = GameState.GameOver;

        //reset level
    }

    // Score management
    public void AddScore(int points)
    {
        currentRecallOrbs += points;
    }

    // Level progression
    public void CompleteLevel()
    {
        currentLevel++;

        //Save System here when needed

        //SceneManager.LoadScene($"Level{currentLevel}");
    }

    public bool IsGamePlaying()
    {
        return CurrentState == GameState.Playing;
    }
}
