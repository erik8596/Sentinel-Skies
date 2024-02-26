using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject selectUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private TextMeshProUGUI upgradeCostText; // Add a Text component to display upgrade cost
 
    [Header("Attributes")]
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float bulletsPerSec = 1f;
    [SerializeField] private int baseUpgradeCost = 100;
    [SerializeField] private int sellCost = 100;

    private float bulletsPerSecBase;
    private float targetingRangeBase;

    private Transform target;
    private float timeUntilFire;

    private int level = 1;

    private void Start(){
        bulletsPerSecBase = bulletsPerSec;
        targetingRangeBase = targetingRange;
        upgradeButton.onClick.AddListener(Upgrade);
        sellButton.onClick.AddListener(Sell);

        UpdateUpgradeCostText(); // Call the method to initialize the upgrade cost text
    }

    private void Update(){
        if(target == null){
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if(!CheckTargetIsInRange()){
            target = null;
        } else {
            timeUntilFire += Time.deltaTime;

            if(timeUntilFire >= 1f/bulletsPerSec){
                Shoot();
                timeUntilFire = 0f;
            }
        }

    }

    private void Shoot(){
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>(); 
        bulletScript.SetTarget(target);
    }

    private void FindTarget(){
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2) transform.position, 0f, enemyMask);

        if (hits.Length > 0){
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange(){
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget(){
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint .rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void OpenSelectUI(){
        selectUI.SetActive(true);
    }

    public void CloseSelectUI(){
        selectUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void Upgrade(){
        if(CalculateCost() > LevelManager.main.currency || level >= 5) return;

        LevelManager.main.SpendCurrency(CalculateCost());

        level++;

        bulletsPerSec = CalculateBulletsPerSec();
        targetingRange = CalculateRange();

        UpdateUpgradeCostText();

        CloseSelectUI();
    }

    public void Sell(){
        LevelManager.main.currency += sellCost;
        Destroy(gameObject);
        CloseSelectUI();
    }

    public int CalculateCost()
    {
        return baseUpgradeCost * level;
    }

    public int GetUpdatedCost()
    {
        return CalculateCost();
    }

    private void UpdateUpgradeCostText()
    {
        if (upgradeCostText != null)
        {
            if (level < 5)
            {
                int currentUpgradeCost = CalculateCost();
                upgradeCostText.text = "Upgrade" + currentUpgradeCost.ToString();
            }
            else
            {
                upgradeCostText.text = "Max";
                upgradeButton.interactable = false; // Disable the upgrade button when at max level
            }
        }
    }

    private float CalculateBulletsPerSec(){
        return bulletsPerSecBase * level;
    }

    private float CalculateRange(){
        return targetingRangeBase * level;
    }

}
