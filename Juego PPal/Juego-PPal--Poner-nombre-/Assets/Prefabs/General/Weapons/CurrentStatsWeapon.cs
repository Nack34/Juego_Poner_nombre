using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentStatsWeapon : MonoBehaviour
{
    
    
    public Enums.PosibleHabilityType tipoHabilidad=Enums.PosibleHabilityType.NormalAttack;

    [SerializeField] private Enums.PosibleWeaponType tipoArmaActual=Enums.PosibleWeaponType.Dagger;
    public Enums.PosibleWeaponType TipoArmaActual{ 
        get{
            return tipoArmaActual;
        }
        set{ /* Accede al arma usada (hijo) con gameObject.transform.GetChild(0).GetComponent<espada>() y si no es null, 
            se trae el valor a obtener, sino, se pone 0. Si es null es un ERROR, no deberian de ser asi las mecanicas del juego (no tendria
            que haber llegado hasta este punto si no tiene el arma en cuestion equipada) */
            tipoArmaActual =value;
            /*switch (tipoArmaActual){
                case tipoArmaActual.dagger: 
                    //CurrentStatsDagger currentWeapon = gameObject.transform.GetChild(0).GetComponent<CurrentStatsDagger>();
                    break;

                case tipoArmaActual.bow: 
                    //CurrentStatsBow currentWeapon = gameObject.transform.GetChild(0).GetComponent<CurrentStatsBow>();
                    break;
                
                case tipoArmaActual.sword:
                    //CurrentStatsSword currentWeapon = gameObject.transform.GetChild(0).GetComponent<CurrentStatsSword>();
                    break;
                
                case tipoArmaActual.magic:
                    //CurrentStatsMagic currentWeapon = gameObject.transform.GetChild(0).GetComponent<CurrentStatsMagic>();
                    break;
                    
                //if (currentWeapon!=null)
                //    danioBase= currentWeapon.danioBase;
                //else {
                //    Debug.Log("Ubicacion: Script \"CurrentStatsWeapon\", Indicacion: El tipo de arma que se intento setear como tipoArmaActual no se encuentra equipada. El tipoArmaActual en cuestion es: "+tipoArmaActual+". El danioBase que se guardara es 0");
                //    danioBase=0;
                //}
            }*/
        }
    }

    public int danioBase=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
