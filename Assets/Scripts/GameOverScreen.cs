using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] Button restartButton;

    public void Setup(bool playerWon)
    {
        gameObject.SetActive(true);
        if (playerWon)
        {
            gameOverText.text = "AWAKENED!";
            gameOverText.color = Color.green;
            restartButton.gameObject.SetActive(false);
        }
        else
        {
            gameOverText.text = "ASLEEP FOREVER!";
            gameOverText.color = Color.red;
            restartButton.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        FindFirstObjectByType<ScenePersist>().ResetScenePersist();
        FindFirstObjectByType<DialogueUI>().ResetDialogueUI();
        SceneManager.LoadScene(1);
        Destroy(FindFirstObjectByType<GameSession>().gameObject);
    }

    public void ExitGame()
    {
        gameObject.SetActive(false);
        Destroy(FindFirstObjectByType<GameSession>().gameObject);
        Destroy(FindFirstObjectByType<ScenePersist>().gameObject);
        Destroy(FindFirstObjectByType<DialogueUI>().gameObject);
        SceneManager.LoadScene(0);
    }
}
