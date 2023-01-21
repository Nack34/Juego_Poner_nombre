using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newSpeciesData", menuName = "Data/Entity Data/Specific Data/Species Data")]
public class D_Species : ScriptableObject
{
    // field of view base data

    [Header("field of view base data: ")] 
    public float[] visionRadius = new float [System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length]; // rango de vision (lo lejos que mira)
    
    [Range(0,360)]
    public float[] visionAngle = new float [System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length]; // angulo TOTAL, para un solo lado es la mitad


    [Header("General data: ")] 
    // info de la zona de inicio
    public float baseRadius = 2.0f; // (este base se refiere al radio de la zona de inicio, zona base)



    [Header("Stats de Aggro: ")] 
    // seria como el "orgullo" de la especie (algunas especies no permitirian que se les escape un objetivo, y a otras no les importaria tanto)
    public float maxTiempoDeBusqueda = 10.0f; //usado en OpponentSearchState
    public float maxSearchRadius = 1.5f;

    [Header("Stats del Nivel de Actividad de uso general: ")] 
    //los siguientes son para todos los States menos MoveState 
    public float maxMovementSpeed = 1.2f;
    public float minMovementSpeed = 0.0f;

    
    [Header("Stats del Nivel de Actividad usado en MoveState: ")]
    //los siguientes son para MoveState
    public int maxCantCambiosDeSpeedParaPasarAIdle = 7;
    public float maxTiempoNecesarioParaCambiarDeMovimiento=2.5f; 
    public float minTiempoNecesarioParaCambiarDeMovimiento=1.5f; 
    public float velocidadMinimaDeMovimiento= 0.4f;

    // Stats base de la entidad

    [Header("Stats base de la especie (se incrementan al pasar ciertas condiciones): ")] // IMPLEMENTAR SCRIPT INCREMENTADOR
    public float segundosNecesariosParaCurarse = 10.0f;
    public float cantPorcentajeCuracionXSegundo = 10.0f;

    public float vidaMaxBase = 5.0f; //(despues se cambia a int)
    // defensas base
    // ataque base


    



}
