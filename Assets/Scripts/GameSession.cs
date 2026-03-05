using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameOverScreen gameOverScreen;
    public bool CanMove { get; private set; }

    [SerializeField] Dictionary<string, int> enemyHits = new Dictionary<string, int>
    {
        {"EmberEnemy", 1},
        {"SnakeyEnemy", 2},
        {"BoneyEnemy", 3}
    };
    
    private Vector3 currentRespawnpoint = new Vector3(1, 1, 0);

    void Awake()
    {
        int numberGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        CanMove = true;
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath(string enemyName)
    {
        int lifesToTake = enemyHits.ContainsKey(enemyName) ? enemyHits[enemyName] : 1;
        if (playerLives > lifesToTake)
        {
            TakeLife(lifesToTake);
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToLife(int livesToAdd)
    {
        playerLives += livesToAdd;
        livesText.text = playerLives.ToString();
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    public void SubstractScore(int pointsToSubstract)
    {
        score -= pointsToSubstract;
        if (score < 0)
        {
            score = 0;
        }
        scoreText.text = score.ToString();
    }

    public bool CheckIfHasEnoughPoints(int pointsToCheck)
    {
        return score >= pointsToCheck;
    }

    void TakeLife(int lifesToTake)
    {
        playerLives -= lifesToTake;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.sceneLoaded += MovePlayerAfterLoad;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    void MovePlayerAfterLoad(Scene scene, LoadSceneMode mode)
    {
        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();

        if (player != null)
        {
            player.transform.position = currentRespawnpoint;
        }

        SceneManager.sceneLoaded -= MovePlayerAfterLoad;
    }
    
    public void SetCurrentRespawnPoint(GameObject newRespawnPoint)
    {
        currentRespawnpoint = newRespawnPoint.transform.position;
    }

    public void ShowWinScreen()
    {
        CanMove = false;
        gameOverScreen.Setup(true);
    }

    void ResetGameSession()
    {
        CanMove = false;
        gameOverScreen.Setup(false);
    }
}
