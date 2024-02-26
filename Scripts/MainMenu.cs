using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Levels to Load")]
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGamedDialog = null;

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        if (!PlayerPrefs.HasKey("levelReached"))
        {
            PlayerPrefs.SetInt("levelReached", 1); // Set the initial level if not already set
        }
    }

    public void NewGameDialogYes()
    {
        PlayerPrefs.DeleteAll(); // Reset PlayerPrefs
        InitializeGame(); // Initialize the game state
        SceneManager.LoadScene(4); // Load the initial scene (or any other scene as needed)
    }

    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("levelReached"))
        {
            int levelReached = PlayerPrefs.GetInt("levelReached");
            if (levelReached > 1)
            {
                levelToLoad = "Map" + levelReached;
                SceneManager.LoadScene(4);
            }
            else
            {
                noSavedGamedDialog.SetActive(true);
            }
        }
        else
        {
            noSavedGamedDialog.SetActive(true);
        }
    }

    public void Tutorial()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void Settings()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void Leaderboard()
    {
        Debug.Log("Go to Leaderboard");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
