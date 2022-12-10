using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArcher : MonoBehaviour
{
    public GameObject archer; // me traigo e objeto a spawnear (sirve para cualquier objeto, no solo para los arqueros) 

    [SerializeField] private int CantArqueros=5;
    void Start (){  // al arrancar spawnea cantArqueros arqueros
        for ( int i=0; i<CantArqueros; i++)
        {
            float x = Random.Range(-1.20f,1.20f);
            float y = Random.Range(-1.20f,1.20f);
            Vector3 position = new Vector3 (x,y,0);
            Quaternion rotation= new Quaternion();
            Instantiate(archer,position,rotation);
        }
    }

    /* private float temporizador=0f; 
    [SerializeField] private float segundosEntreSpawns= 5f;
    
    private void Update() // cada segundosEntreSpawns segundos, spawnea un nuevo arquero
    {
        temporizador = temporizador + Time.deltaTime;

        if (temporizador >= segundosEntreSpawns)
        {
            float x = Random.Range(-1.20f,1.20f);
            float y = Random.Range(-1.20f,1.20f);
            Vector3 position = new Vector3 (x,y,0);
            Quaternion rotation= new Quaternion();
            temporizador = 0;
            Instantiate(archer,position,rotation);
        }
    }*/
}