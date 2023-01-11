using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data", order = 0)]
public class D_Entity : ScriptableObject {

    // estos 2 son generales de todas las entidades, HACER un scriptable object q sea compartido por todos los NPCS
    public ContactFilter2D movementFilter; // posible collisions
    public float collisionOffset = 0.0005f; // offset for checking collision

    // HACER un scriptable object q sea compartido por todos los del mismo bando
    // attack filter (seria los otros bandos y el player (en bando 1 no seria el player))

    // HACER un scriptable object q sea compartido por todos los del mismo tipo (arqueros, tanques, etc)

    // estos 2 son generales de la especie, HACER un scriptable object q sea compartido por toda una misma especie
    public float baseRadius = 2.0f;
    public float velocidadMinima= 0.4f;


    /* HACER un scriptable object q sea particular de la entidad (Pensar: que guardaria? Sus stats? No, eso se guardaria en el script 
    monobehabiour "Stats" y se calcularia con el valor de las stats base guardadas en el Scriptableobject del tipo de enemigo (ej: los 
    arqueros tienen 3 de vida) y serias multiplicados por los multiplicadores de especie (ej: los orcos tienen *2 de vida y los goblis *0.5), 
    entonces la vida definitiva, guardada en los scripts "Stats", seria otra (ej: vida de orcos arqueros = 3*2 = 6 y la vida de los 
    arqueros goblis = 3*0.5=1.5) y con eso se dictarian diferencias entre tipos de npcs y especies de npcs)
    */

}
