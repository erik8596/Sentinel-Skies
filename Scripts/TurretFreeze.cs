using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class TurretFreeze : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject selectUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private TextMeshProUGUI upgradeCostText;


    [Header("Attributes")]
    [SerializeField] private float targetingRange = 2f;
    [SerializeField] private float freezesPerSecond = 0.25f;
    [SerializeField] private float freezeTime = 1f;
    [SerializeField] private int baseUpgradeCost = 200;
    [SerializeField] private int sellCost = 100;

    private float timeUntilFire;

    private float freezesPerSecondBase;
    private float targetingRangeBase;

    private int level = 1;

    private void Start()
    {
        freezesPerSecondBase = freezesPerSecond;
        targetingRangeBase = targetingRange;
        upgradeButton.onClick.AddListener(Upgrade);
        sellButton.onClick.AddListener(Sell);
        UpdateUpgradeCostText();
    }

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / freezesPerSecond)
        {
            FreezeEnemy();
            timeUntilFire = 0f;
        }
    }

    private void FreezeEnemy()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                // Assuming that EnemyMovement script has an UpdateSpeed and ResetSpeed method
                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(0.5f);

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
    }

    public void OpenSelectUI()
    {
        selectUI.SetActive(true);
    }

    public void CloseSelectUI()
    {
        selectUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void Upgrade()
    {
        if (CalculateCost() > LevelManager.main.currency || level >= 3) return;

        LevelManager.main.SpendCurrency(CalculateCost());

        level++;

        freezesPerSecond = CalculateFreezesPerSecond();
        targetingRange = CalculateRange();

        UpdateUpgradeCostText();

        CloseSelectUI();
    }

    public void Sell()
    {
        LevelManager.main.currency += sellCost;
        Destroy(gameObject);
        CloseSelectUI();
    }

    private int CalculateCost()
    {
        return baseUpgradeCost * level;
    }

    private float CalculateFreezesPerSecond()
    {
        return freezesPerSecondBase * level;
    }

    private float CalculateRange()
    {
        return targetingRangeBase * level;
    }

    private void UpdateUpgradeCostText()
    {
        if (upgradeCostText != null)
        {
            if (level < 3)
            {
                int currentUpgradeCost = CalculateCost();
                upgradeCostText.text = "Upgrade " + currentUpgradeCost.ToString();
            }
            else
            {
                upgradeCostText.text = "Max";
                upgradeButton.interactable = false; // Disable the upgrade button when at max level
            }
        }
    }


}

