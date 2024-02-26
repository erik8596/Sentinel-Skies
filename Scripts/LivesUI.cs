using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesUI : MonoBehaviour
{

    [Header("References")]
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] int maxWaves;

    void Update()
    {
        int modifiedRounds = PlayerStats.Rounds + 1;

        // Update the UI text
        livesText.text = "Wave " + modifiedRounds.ToString() + "/" + maxWaves.ToString() + "       " + PlayerStats.Lives.ToString() + " Lives Remaining";
    }
}
