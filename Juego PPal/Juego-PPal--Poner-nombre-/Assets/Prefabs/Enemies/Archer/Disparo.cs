using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{

    private float temporizador=0;
    public float tiempoEntreDisparos= 2f; 
    public GameObject FlechaPrefab;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        temporizador = temporizador + Time.fixedDeltaTime;

        if (temporizador >= tiempoEntreDisparos)
        {
            temporizador = 0;
            
            Quaternion rotation = new Quaternion(); 
            Vector3 positionArrow = gameObject.GetComponent<Transform>().position;
            Instantiate(FlechaPrefab,positionArrow,rotation);

        }


    }
}
