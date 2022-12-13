using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentStatsWeapon : MonoBehaviour
{
    
    public enum TipoHabilidad { normalAttack = 0, habilidad1 = 1, habilidad2 = 2, habilidad3 = 3 };
    public TipoHabilidad tipoHabilidad=TipoHabilidad.normalAttack;

    public enum TipoDeArma { dagger = 0, bow = 1, sword = 2, magic= 3 }; // ya esta en el script de currenDagger, no se si tmb va aca, o si tiene su propio script (solo estaria el enum)
    [SerializeField] private TipoDeArma tipoArmaActual=TipoDeArma.dagger;
    public TipoDeArma TipoArmaActual{ 
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
