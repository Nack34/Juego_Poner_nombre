using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsWeapon : MonoBehaviour
{
    public enum TipoDeArma { dagger = 0, bow = 1, magic = 2, sword = 3 };
    public TipoDeArma tipoArmaActual= TipoDeArma.dagger; 
    public enum TipoHabilidad { normalAttack = 0, habilidad1 = 1, habilidad2 = 2, habilidad3 = 3 };
    public TipoHabilidad tipoHabilidad=TipoHabilidad.normalAttack; // HACERLO UN ENUM?

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
