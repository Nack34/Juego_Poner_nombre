using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject {

    public string entityName = "Entidad sin nombre";


    // estos 2 son generales de la especie, HACER un scriptable object q sea compartido por toda una misma especie
    public float velocidadMinimaDeMovimiento= 0.4f;


    /* HACER un scriptable object q sea particular de la entidad (Pensar: que guardaria? Sus stats? No, eso se guardaria en el script 
    monobehabiour "Stats" y se calcularia con el valor de las stats base guardadas en el Scriptableobject del tipo de enemigo (ej: los 
    arqueros tienen 3 de vida) y serias multiplicados por los multiplicadores de especie (ej: los orcos tienen *2 de vida y los goblis *0.5), 
    entonces la vida definitiva, guardada en los scripts "Stats", seria otra (ej: vida de orcos arqueros = 3*2 = 6 y la vida de los 
    arqueros goblis = 3*0.5=1.5) y con eso se dictarian diferencias entre tipos de npcs y especies de npcs)
    */
    
    public D_NPC npcData;
    public D_Type typeData;
    public D_Species speciesData;
    public D_Bando bandoData;


}
