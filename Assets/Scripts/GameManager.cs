using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text enemiesLeftText;

    int enemiesLeft = 0;

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateEnemiesLeft(int count)
    {
        enemiesLeft += count;
        enemiesLeftText.text = $"Enemies Left: {enemiesLeft}";

        if (enemiesLeft <= 0)
        {
            gameOverText.text = "You Win!";
            StarterAssetsInputs inputs = FindFirstObjectByType<StarterAssetsInputs>();
            inputs.SetCursorState(false);
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
