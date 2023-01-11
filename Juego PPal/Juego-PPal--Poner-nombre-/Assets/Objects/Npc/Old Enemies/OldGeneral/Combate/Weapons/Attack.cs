using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    
    CurrentWeaponStats CurrentWeaponStats;
    private void Awake() {
        CurrentWeaponStats = GetComponent<CurrentWeaponStats>();
    }

    OldDamageable damageable;
    public void NormalAttack(Collider2D other) 
    { 
        damageable = other.GetComponent<OldDamageable>();
        if (damageable != null )  // puede ser que lo que haya tocado no tenga que recibir danio
            damageable.RecibirDanio(CurrentWeaponStats.BaseDamageCurrentWeapon,CurrentWeaponStats.tipoDeDanio);
    }
}
