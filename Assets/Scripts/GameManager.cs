using StarterAssets;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] FirstPersonController player;
    [SerializeField] GameObject activeWeapon;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text enemiesLeftText;

    StarterAssetsInputs inputs;

    int enemiesLeft = 0;

    bool isStarted = false;

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

    private void Start()
    {
        inputs = FindFirstObjectByType<StarterAssetsInputs>();
        inputs.SetCursorState(false);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isStarted)
        {
            inputs.SetCursorState(false);
            player.enabled = false;
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void UpdateEnemiesLeft(int count)
    {
        enemiesLeft += count;
        enemiesLeftText.text = $"Enemies Left: {enemiesLeft}";

        if (enemiesLeft <= 0)
        {
            gameOverText.text = "You Win!";
            inputs.SetCursorState(false);
            player.enabled = false;
            gameOverPanel.SetActive(true);
            Time.timeScale = 0.1f;
        }
    }

    public void StartGame()
    {
        isStarted = true;
        inputs.SetCursorState(true);
        player.enabled = true;
        hud.SetActive(true);
        mainMenu.SetActive(false);
        activeWeapon.SetActive(true);
        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        inputs.SetCursorState(true);
        pausePanel.SetActive(false);
        player.enabled = true;
        Time.timeScale = 1f;
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
