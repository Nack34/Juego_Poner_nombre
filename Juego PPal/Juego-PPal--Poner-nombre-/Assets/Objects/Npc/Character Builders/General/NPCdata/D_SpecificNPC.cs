using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEspecificNPCData", menuName = "Data/Entity Data/EspecificNPC Data")]
public class D_SpecificNPC : ScriptableObject
{
    // Probabilidades de seleccion de acciones (Si cambia la longitud del array hay q borrarlo y crearlo nuevamente)

    [Header("Probabilidades de seleccion de acciones (Las acciones posibles se encuentran en el script Enums): ")]
    
    [Header("Idle actions probabilities: ")]
    public float[] idleActions_UseProbabilities = new float [System.Enum.GetValues(typeof(Enums.PosibleIdleActions)).Length];
    public float[] idleActions_SeleccionProbabilities = new float [System.Enum.GetValues(typeof(Enums.PosibleIdleActions)).Length];

    [Header("FTF Range Combat actions probabilities: ")]
    public float[] FTFRangeCombatActions_UseProbabilities = new float [System.Enum.GetValues(typeof(Enums.PosibleCombatActions)).Length];
    public float[] FTFRangeCombatActions_SelectionProbabilities = new float [System.Enum.GetValues(typeof(Enums.PosibleCombatActions)).Length];
        
    [Header("Close Range Combat actions probabilities: ")]
    public float[] closeRangeCombatActions_UseProbabilities = new float [System.Enum.GetValues(typeof(Enums.PosibleCombatActions)).Length];
    public float[] closeRangeCombatActions_SelectionProbabilities = new float [System.Enum.GetValues(typeof(Enums.PosibleCombatActions)).Length];

    [Header("Long Range Combat actions probabilities: ")]
    public float[] longRangeCombatActions_UseProbabilities = new float [System.Enum.GetValues(typeof(Enums.PosibleCombatActions)).Length];
    public float[] longRangeCombatActions_SelectionProbabilities = new float [System.Enum.GetValues(typeof(Enums.PosibleCombatActions)).Length];


    // Incrementadores de Stats (solo stats base)
    // Estos valores son especificos de la especie, bando y typo de NPC, se incrementan luego de las peleas. Es para que la siguiente vez...
    //... q peleen sean mas fuertes. Por ej, si se curan una cierta cantidad de vida, su vida max se incrementaria (el igual q el player)
    [Header("Incrementadores de Stats: ")]
    public int incrementoDeVidaMax = 0;
}
