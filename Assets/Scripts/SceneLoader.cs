using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Cached Parameters
    GameSession gameStatus;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameSession>();
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex+1);
    }

    public void LoadStartScene()
    {
        gameStatus.ResetGame();
        SceneManager.LoadScene(0);
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
