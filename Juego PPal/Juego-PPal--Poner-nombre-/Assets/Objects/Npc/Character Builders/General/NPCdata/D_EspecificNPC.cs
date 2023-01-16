using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEspecificNPCData", menuName = "Data/Entity Data/EspecificNPC Data")]
public class D_EspecificNPC : ScriptableObject
{
    // Probabilidades de seleccion de acciones (Si cambia la longitud del array hay q borrarlo y crearlo nuevamente)
    public float[] posibleIdleAnimationsProbabilities = new float [System.Enum.GetValues(typeof(Enums.PosibleIdleAnimations)).Length];
    public float[] posibleLongRangeActionsProbabilities = new float [System.Enum.GetValues(typeof(Enums.PosibleLongRangeActions)).Length];
    public float[] posibleShortRangeActionsProbabilities = new float [System.Enum.GetValues(typeof(Enums.PosibleShortRangeActions)).Length];
    public float[] posibleFTFRangeActionsProbabilities = new float [System.Enum.GetValues(typeof(Enums.PosibleFTFRangeActions)).Length];


    // Incrementadores de Stats (solo stats base)
    // Estos valores son especificos de la especie, bando y typo de NPC, se incrementan luego de las peleas. Es para que la siguiente vez...
    //... q peleen sean mas fuertes. Por ej, si se curan una cierta cantidad de vida, su vida max se incrementaria (el igual q el player)
    public int incrementoDeVidaMax = 0;
}
