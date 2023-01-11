using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newSpeciesData", menuName = "Data/Entity Data/Specific Data/Species Data")]
public class D_Species : ScriptableObject
{
    // info de la zona de inicio
    public float baseRadius = 2.0f; // (este base se refiere al radio de la zona de inicio, zona base)

    // Stats base de la entidad
    public float maxMovementSpeed = 1.2f;
    public float minMovementSpeed = 0f;


    public float segundosNecesariosParaCurarse = 10.0f;
    public float cantPorcentajeCuracionXSegundo = 10.0f;
    public float vidaMaxBase = 5; //(despues se cambia a int)

}
