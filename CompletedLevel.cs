using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompletedLevel : MonoBehaviour
{
    public string nextLevel = "Level02";
    public int levelToUnlock = 2;

    public void Continue()
    {
        Debug.Log("Win Level!");
        string nextLevelName = "Map" + levelToUnlock;

        if (SceneExists(nextLevelName))
        {
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogWarning("Next level does not exist, returning to scene 0!");
            SceneManager.LoadScene(0); // Load the first scene in build settings
        }
    }

    // Helper method to check if a scene exists
    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            if (scenePath.EndsWith(sceneName + ".unity"))
            {
                return true;
            }
        }
        return false;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
