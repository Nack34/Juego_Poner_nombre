using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/State Data/Move State", order = 0)]
public class D_MoveState : ScriptableObject {

    public float maxMovementSpeed = 1.2f;
    public float minMovementSpeed = 0f;
    public float maxTiempoNecesarioParaCambiarDeMovimiento=2.5f;  
    public float minTiempoNecesarioParaCambiarDeMovimiento=1.5f; 
    public int maxCantCambiosDeSpeedParaPasarAIdle = 7;

}
