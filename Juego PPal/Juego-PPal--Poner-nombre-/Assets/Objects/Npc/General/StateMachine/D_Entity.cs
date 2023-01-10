using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data", order = 0)]
public class D_Entity : ScriptableObject {

    public ContactFilter2D movementFilter; // posible collisions
    public float collisionOffset = 0.0005f; // offset for checking collision
    public float baseRadius = 2.0f;
    

}
