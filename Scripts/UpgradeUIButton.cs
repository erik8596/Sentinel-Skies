using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUIButton : MonoBehaviour
{

    [Header("References")]
    [SerializeField] TextMeshProUGUI upgradeCostText;

    // Update is called once per frame
    void Update()
    {
        int modifiedRounds = PlayerStats.Rounds + 1;

        // Update the UI text
        upgradeCostText.text = "Wave " + modifiedRounds.ToString();
    }
}

