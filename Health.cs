using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int startHitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    [Header("UI")]
    public Image healthBar;

    private bool isDestroyed = false;
    private float hitPoints;

    public void Start()
    {
        hitPoints = startHitPoints;
    }

    public void TakeDamage(int dmg){
        hitPoints -= dmg;

        healthBar.fillAmount = hitPoints / startHitPoints;

        if(hitPoints <= 0 && !isDestroyed){
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
