using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArcher : MonoBehaviour
{
    
    public GameObject archer;
    public int CantArqueros=5;
    void Start (){
        for ( int i=0; i<CantArqueros; i++)
        {
            float x = Random.Range(-1.20f,1.20f);
            float y = Random.Range(-1.20f,1.20f);
            Vector3 position = new Vector3 (x,y,0);
            Quaternion rotation= new Quaternion();
            Instantiate(archer,position,rotation);
        }
    }

    float temporizador=0; 
    public float segundosEntreSpawns= 5;
    
    void FixedUpdate()
    {
        temporizador = temporizador + Time.fixedDeltaTime;

        if (temporizador >= segundosEntreSpawns)
        {
            float x = Random.Range(-1.20f,1.20f);
            float y = Random.Range(-1.20f,1.20f);
            Vector3 position = new Vector3 (x,y,0);
            Quaternion rotation= new Quaternion();
            temporizador = 0;
            Instantiate(archer,position,rotation);
        }
    }
}