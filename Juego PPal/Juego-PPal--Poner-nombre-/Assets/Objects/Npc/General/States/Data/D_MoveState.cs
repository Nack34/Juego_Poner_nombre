using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/State Data/Move State")]
public class D_MoveState : ScriptableObject {

    public float maxTiempoNecesarioParaCambiarDeMovimiento=2.5f;  
    public float minTiempoNecesarioParaCambiarDeMovimiento=1.5f; 
    public int maxCantCambiosDeSpeedParaPasarAIdle = 7;

    public float velocidadMinimaDeMovimiento= 0.4f;

}
