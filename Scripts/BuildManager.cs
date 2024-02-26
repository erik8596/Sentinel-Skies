using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private StoreTurret[] turrets; 

    private int selectedTurret = 0;

    public void Awake(){
        main = this;
    }

    public StoreTurret GetSelectedTurret(){
        return turrets[selectedTurret];
    }

    public void SetSelectedTurret(int _selectedTurret){
        selectedTurret = _selectedTurret;
    }

}
