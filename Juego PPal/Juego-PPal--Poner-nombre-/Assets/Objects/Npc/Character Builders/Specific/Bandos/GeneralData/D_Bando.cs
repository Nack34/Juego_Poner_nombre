using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newBandoData", menuName = "Data/Entity Data/Specific Data/Bando Data")]
public class D_Bando : ScriptableObject
{
    public string miBando; // para que se setee automaticamente en hurtbox

    // target filter
    public LayerMask oponentFilter; // Obstaculos que ocultan a los oponentes


}
