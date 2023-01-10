using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/State Data/Move State", order = 0)]
public class D_MoveState : ScriptableObject {

    public int maxMovementSpeed = 3; // siempre camina
    public float baseRadius = 2.0f;
    public float maxTiempoNecesarioParaCambiarDeMovimiento=2.5f; 
    public int maxCantCambiosDeSpeedParaPasarAIdle = 7;

}
