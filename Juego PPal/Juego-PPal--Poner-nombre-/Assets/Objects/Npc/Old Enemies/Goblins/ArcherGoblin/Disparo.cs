using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{

    private float temporizador=0;
    [SerializeField] private float tiempoEntreDisparos= 2f; 
    public GameObject FlechaPrefab;
    

    // Update is called once per frame
    void Update()
    {
        temporizador = temporizador + Time.deltaTime;

        if (temporizador >= tiempoEntreDisparos)
        {
            temporizador = 0;
            
            Quaternion rotation = new Quaternion(); 
            Vector3 positionArrow = gameObject.GetComponent<Transform>().position;
            Instantiate(FlechaPrefab,positionArrow,rotation);

        }


    }
}
