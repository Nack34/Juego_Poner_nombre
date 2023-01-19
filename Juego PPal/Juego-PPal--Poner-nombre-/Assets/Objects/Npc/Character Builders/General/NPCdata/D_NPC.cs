using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newNPCData", menuName = "Data/Entity Data/Specific Data/NPC Data")]
public class D_NPC : ScriptableObject {

    [Tooltip("Usado en moveState: ")]
    public ContactFilter2D movementFilter; // posible collisions
    public float collisionOffset = 0.0005f; // offset for checking collision

    [Tooltip("Usado en los FOVs: ")]
    // obstruccion filter
    public LayerMask nonSeeThroughObstaclesFilter; // Obstaculos que ocultan a los oponentes

    // Incremento en las stats (solo stats base) de todos los NPCs
    // Se incrementan por un script controlador (tendria q estar fuera de las escenas o ejecutarse al pasar por una escena), luego de ciertas etapas (pero eso creo q seria el incremento en las estas base de las especies, o podria estar en la Data general de todos los npcs, ))
    [Tooltip("Incremento en las stats (a todos los NPC): ")]
    public int incrementoDeVidaMax = 0;
    
}
