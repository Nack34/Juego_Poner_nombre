using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentStatsDagger : MonoBehaviour
{
    public Enums.PosibleWeaponType tipoArmaActual= Enums.PosibleWeaponType.Dagger; 

    // Accede al arma usada (hijo) con gameObject.transform.GetChild(0).GetComponent<espada>() y si no es null, se trae el valor a obtener, sino, se pone 0
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
