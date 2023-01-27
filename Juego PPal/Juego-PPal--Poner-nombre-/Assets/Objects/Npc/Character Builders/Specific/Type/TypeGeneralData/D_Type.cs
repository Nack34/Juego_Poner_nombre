using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newTypeData", menuName = "Data/Entity Data/Specific Data/Type Data")]
public class D_Type : ScriptableObject
{
    [Header("field of view multipliers: ")] 
    public float visionRadiusMultiplier = 1.0f; // multiplicador de los rangos de vision (lo lejos que mira)
    public float visionAngleMultiplier = 1.0f; // multiplicador de los angulo de vision (TOTALES, para un solo lado es la mitad)

    [Header("Provisorio (despues cuando se cree la data de las habilidades, esto iria ahi): ")] 
    public float attackRadius = 0.1f;
    public float attackDistace = 0.1f;
    public GameObject objectToInstance;

    [Header("stats multipliers: ")] 
    public float multiplicadorDeVidaMax = 1.0f;

}
