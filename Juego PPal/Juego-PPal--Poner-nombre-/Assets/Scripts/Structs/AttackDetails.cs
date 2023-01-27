using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AttackDetails {

    public string attackName; 

    public Vector2 hurterPosition;
    public float hurterMovementSpeed; // cambiar esto para q sea solo usado por el que hace el daño, y todo lo demas lo usa el q recive el daño
    public Vector2 hurterMovementDirection; // cambiar esto para q sea solo usado por el que hace el daño, y todo lo demas lo usa el q recive el daño

    public Enums.PosibleDamageType damageType;
    public float damageAmount;

    public float pushRate;
    public Vector2 pushDirection;

    public float stunRate;
    public float burnRate;
    public float freezeRate;
    public float slowingRate;
    public float wettingRate;
    public float electrifyRate;
    public float bleedingRate;
}
