using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    public GameObject turretObj;
    public Turret turret;
    public TurretFreeze turretFreeze;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.main.IsHoveringUI()) return;

        if (turretObj != null)
        {
            // Check the type of turret and open the select UI accordingly
            if (turret != null)
            {
                turret.OpenSelectUI();
            }
            else if (turretFreeze != null)
            {
                turretFreeze.OpenSelectUI();
            }

            return;
        }

        StoreTurret turretToBuild = BuildManager.main.GetSelectedTurret();

        if (turretToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("You cannot afford turret");
            return;
        }

        LevelManager.main.SpendCurrency(turretToBuild.cost);
        turretObj = Instantiate(turretToBuild.prefab, transform.position, Quaternion.identity);

        // Set the corresponding turret type after instantiation
        turret = turretObj.GetComponent<Turret>();
        turretFreeze = turretObj.GetComponent<TurretFreeze>();
    }
}

