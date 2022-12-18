using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentWeaponStats : MonoBehaviour // Guarda todas las armas actuales (una de cada tipo), cuando el arma actual es modificada, cambian las estadisticas
{
//    [SerializeField] private AnimationSelector animationSelector; // se refencia en unity
    private int cantArmas = System.Enum.GetValues(typeof(Enums.PosibleWeaponType)).GetLength(0);
    public WeaponStats [] armasEquipadas; // es cargado por las armas (script WeaponStats)
    [SerializeField] private WeaponStats armaActual;
    
    void Awake()
    {
        armasEquipadas = new WeaponStats [cantArmas]; 
    }

    void Start()
    {
        int i=0;
        while (armaActual==null && i<cantArmas){
            armaActual = armasEquipadas[i];
            i++;
        }
            
        if (armaActual!=null)
            BaseDamageCurrentWeapon = armaActual.danioBase;
        else 
            BaseDamageCurrentWeapon = 0;
    }


    public int BaseDamageCurrentWeapon=1;
    public Enums.PosibleDamageType tipoDeDanio=Enums.PosibleDamageType.Fisico;
    
    [SerializeField] private Enums.PosibleWeaponType tipoArmaActual=Enums.PosibleWeaponType.Dagger;
    public Enums.PosibleWeaponType TipoArmaActual{ 
        get{
            return tipoArmaActual;
        }
        set{             
            if (armasEquipadas[(int)value]!=null){
                tipoArmaActual=value;
                armaActual =armasEquipadas[(int)tipoArmaActual]; 
                BaseDamageCurrentWeapon= armaActual.danioBase;
//              animationSelector.currentWeaponType=tipoArmaActual;
                }
            else {
                Debug.Log("Ubicacion: Script \"CurrentStatsWeapon\", Indicacion: El tipo de arma que se intento setear como tipoArmaActual no se encuentra equipada. El tipoArmaActual en cuestion es: "+tipoArmaActual+". El danioBase y el arma quedaran como la ultima seleccionada");
            }
        
        }
    }

}
