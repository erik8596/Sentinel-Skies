using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
  public void MainMenu()
  {
    SceneManager.LoadSceneAsync(0);
  }

  public void Controls()
  {
    Debug.Log("Show Controls Panel");
  }

  public void TTypes()
  {
    Debug.Log("Show TTypes Panel");
  }

  public void TUpgrades()
  {
    Debug.Log("Show TUpgrades Panel");
  }

  public void HowTo()
  {
    Debug.Log("Show HowTo Panel");
  }

  public void SavingProfile()
  {
    Debug.Log("Show Saving Profile Panel");
  }
  
}