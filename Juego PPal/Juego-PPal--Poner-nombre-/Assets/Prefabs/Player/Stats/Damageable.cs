using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    // Start is called before the first frame update
    private int vidaMax;
    void Start()
    {
        vidaMax=gameObject.GetComponent<Stats>().vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RecibirDanio (int danio, string tipoDeDanio)
    {
        gameObject.GetComponent<Stats>().TiempoSinRecibirDanio=0;
        float DanioInflijido= CalcularDanioReal(danio,tipoDeDanio) ;
        if (DanioInflijido<1f) // el minimo de danio es 1
            DanioInflijido=1f;
        gameObject.GetComponent<Stats>().Vida-= (int)DanioInflijido;
    }

    private float CalcularDanioReal (int danio, string tipoDeDanio){
        int vidaTotal = CalcularVidaTotal (tipoDeDanio);
        float porcentajeDeDanio = danio*100/vidaTotal;
        return  porcentajeDeDanio * vidaMax / 100 ;
    }

    private int CalcularVidaTotal(string tipoDeDanio){
        switch (tipoDeDanio){
            case "fisico": 
                return  vidaMax + gameObject.GetComponent<Stats>().defensaFisica;
            case "magico": 
                return  vidaMax + gameObject.GetComponent<Stats>().defensaMagica;
            case "verdadero": 
                return  vidaMax;
            default: //hacer algo para evitar poner esto ya que queda feo
                return vidaMax;
        }   
    }

}
