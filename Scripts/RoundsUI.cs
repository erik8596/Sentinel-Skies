using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundsUI : MonoBehaviour
{

    [Header("References")]
    [SerializeField] TextMeshProUGUI roundsText;

    // Update is called once per frame
    void Update()
    {
        roundsText.text = "Wave " + PlayerStats.Rounds.ToString() + " / 50 ";
    }
}
