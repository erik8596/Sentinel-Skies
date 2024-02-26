using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;
    public static bool gameEnded;

    public GameObject gameOverUI;
    public GameObject pauseMenuUI;
    public GameObject completedLevelUI;
    public int currency;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        gameEnded = false;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough currency");
            return false;
        }
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
    }

    private void Update()
    {
        if (gameEnded)
        {
            return;
        }

        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }

        if (Input.GetKeyDown("p"))
        {
            TogglePauseMenu();
        }

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        gameEnded = true;
        completedLevelUI.SetActive(true);
    }

    public void TogglePauseMenu()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);

        if (pauseMenuUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        TogglePauseMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void OnPauseButtonClick()
    {
        TogglePauseMenu();
    }

    public void OnExitButtonClick()
    {
        EndGame();
    }
}
