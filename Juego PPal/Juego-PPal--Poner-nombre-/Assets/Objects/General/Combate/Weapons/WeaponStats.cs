using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour // este script es comun a todas las armas, pero tiene los datos de privados de un arma en especifico. Se puede encontrar en el arma
{
    [SerializeField] private CurrentWeaponStats currentWeaponStats; // se refencia en unity

    [SerializeField] private Enums.PosibleWeaponType tipoArma; 
    public int danioBase; 

    void Awake()
    {
        //currentWeaponStats = GetComponent<CurrentWeaponStats>();
    }

    void Start()
    {
        currentWeaponStats.armasEquipadas[(int)tipoArma] = this;
    }

}
