using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject {

    public string entityName = "Entidad sin nombre";

    /* En el script monobehabiour "Stats" y se calcularia con el valor de las stats base guardadas en el Scriptableobject de la especie de 
    npc (ej: los goblins tienen 3 de vida base y lo orcos 5) y serias multiplicados por los multiplicadores de Type (ej: los tanques tienen *2
    de vida y los magos *0.5), entonces la vida definitiva, guardada en los scripts "Stats", seria otra (ej: vida de orcos magos = 5*0.5 = 2.5 
    y la vida de los goblis tanques = 3*2=6) y con eso se dictarian diferencias entre tipos de npcs y especies de npcs) 
    */
    /* Ademas de eso, en ciertas escenas (para mantener el juego nivelado), se incrementaran todas las stats basicas de todos los npcs. En
    el script npcdata se guardan los valores a sumar a la vida calculada previamente usando los scripts typeData y speciesData
    */
    /* La ultima manera de que los npcs incrementen su vida es "entrenando", por ejemplo, si el player siempre pelea contra el mismo enemigo,
    digamos el goblin dagger, cada vez q este sera derrotado, a las proximas instancias se le incrementaran las stats. De que manera? En el
    script specifiNPCData se guarda los acumumiladores de Stats de esa clase de NPC en especifico
    */

    [Tooltip("Data compartida por todos los npcs")]
    public D_NPC npcData;
    [Tooltip("Data compartida por los npcs del mismo tipo")]
    public D_Type typeData;
    [Tooltip("Data compartida por los npcs de la misma especie")]
    public D_Species speciesData;
    [Tooltip("Data compartida por los npcs del mismo bando")]
    public D_Bando bandoData;
    [Tooltip("Data compartida unicamente por los npcs que comparten Prefab")]
    public D_EspecificNPC specificNPCdata;

}
