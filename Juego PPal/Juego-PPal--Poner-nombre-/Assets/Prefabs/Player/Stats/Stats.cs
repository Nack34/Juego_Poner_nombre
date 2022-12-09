using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private float tiempoSinRecibirDanio=999f;   
    public float TiempoSinRecibirDanio {
        set {
            tiempoSinRecibirDanio=value;
        }
        get {
            return tiempoSinRecibirDanio;
        }
    }  
    
    public int vidaMax=5;
    [SerializeField] private int vida=5;  // vida actual
    public int Vida {
        set {
            vida=value;
            if (vida<=0) // si tu vida llega a 0, moris
                Muerte();
        }
        get {
            return vida;
        }
    }
    public int defensaFisica =1;
    public int defensaMagica =1;
    public int danioBase =1; // danio base de todas las armas, es sumado(?) por el danio de arma para obtener el danio total 
    public int stamina =5;  // utitlizado para correr, trabajar, etc
    public int percepcion =1; // algunas cosas (objetos) del mundo solo pueden verse con cierto puntaje de esto, de tenerlo demasiado bajo, estas cosas seran invicibles o se veran de otra manera
    public int carisma =1; // para relacionearte mejor con los npcs
    public int inteligencia =1; // sirve para resolver puzzles o trabajar en investigacion
    
    void Start()
    {
        //Vida = vidaMax;
    }
    
    private void Muerte (){
        Debug.Log("muerte de player");
        //Destroy(gameObject);
    }

    void FixedUpdate(){
        TiempoSinRecibirDanio+=Time.fixedDeltaTime;
    }




}
