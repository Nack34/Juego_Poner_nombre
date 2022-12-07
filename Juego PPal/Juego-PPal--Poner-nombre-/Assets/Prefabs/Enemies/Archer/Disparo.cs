using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{

    float temporizador;

    public GameObject FlechaPrefab;
    

    // Update is called once per frame
    void Update()
    {
        temporizador = temporizador + Time.deltaTime;

        if (temporizador >= 2f)
        {
            temporizador = 0;
            Instantiate(FlechaPrefab);

        }


    }
}
