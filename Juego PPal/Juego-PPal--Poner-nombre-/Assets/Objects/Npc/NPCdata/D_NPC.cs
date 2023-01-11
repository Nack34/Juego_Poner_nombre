using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newNPCData", menuName = "Data/Entity Data/Specific Data/NPC Data")]
public class D_NPC : ScriptableObject {

    public ContactFilter2D movementFilter; // posible collisions
    public float collisionOffset = 0.0005f; // offset for checking collision
}
