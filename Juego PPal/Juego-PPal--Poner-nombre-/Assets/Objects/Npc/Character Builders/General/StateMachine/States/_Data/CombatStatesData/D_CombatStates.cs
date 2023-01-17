using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCombatStateData", menuName = "Data/State Data/Combat State")]
public class D_CombatStates : ScriptableObject
{
    public D_CombatSubState faceToFaceRange_CombatSubStateData;
    public D_CombatSubState shortRange_CombatSubStateData;
    public D_CombatSubState longRange_CombatSubStateData;
}
