using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats_Player : MonoBehaviour
{
    public int vidaMax=5;
    private int vida; // si tu vida llega a 0, moris
    public int Vida {
        set {
            vida=value;
            if (vida<=0)
                Muerte();
        }
        get {
            return vida;
        }
    }
    public int danioBase =1; // danio base de todas las armas, es sumado(?) por el danio de arma para obtener el danio total 
    public int defensaFisica =1;
    public int defensaMagica =1;
    public int stamina =5;  // utitlizado para correr, trabajar, etc
    public int percepcion =1; // algunas cosas (objetos) del mundo solo pueden verse con cierto puntaje de esto, de tenerlo demasiado bajo, estas cosas seran invicibles o se veran de otra manera
    public int carisma =1; // para relacionearte mejor con los npcs
    public int inteligencia =1; // sirve para resolver puzzles o trabajar en investigacion

    private int tiempoSinRecibirDanio=999;         //   000 -> 001 ---> 111 -> 000
    public int segundosNecesariosParaCurarse= 10;

    // Start is called before the first frame update
    void Start()
    {
        vida = vidaMax;
        Debug.Log("Vida INICIAL: "+Vida);
    }
    
    float temporizadorDeSegundos=0f;
    float acumuladora = 0;
    /*void FixedUpdate()
    {
        temporizadorDeSegundos+=Time.fixedDeltaTime;
        if (temporizadorDeSegundos>=1f){ //temporizadorDeSegundos es para que cada un segundo cheequee si puedo curarme
            temporizadorDeSegundos=0f;

            tiempoSinRecibirDanio++;
            if (tiempoSinRecibirDanio>=segundosNecesariosParaCurarse){
                acumuladora = CantCuracion(acumuladora);
                Curarse(acumuladora);
            }
        }
    }

    public int cantPorcentajeCuracionXSegundo = 5;
    private float CantCuracion (float acumuladora){ // la curacion es de manera pasiva, cada cierta cant de tiempo sin recivir danio, me curo el (cantPorcentajeCuracionXSegundo)% de la vida
        if (Vida<vidaMax) 
            return acumuladora + vidaMax * (cantPorcentajeCuracionXSegundo/100);
        else return 0;
    }
    
    private void Curarse (float acumuladora){
        Vida+=(int)acumuladora;
        if ((int)acumuladora>0)
            acumuladora=0;
    }*/

    public void RecibirDanio (int danio, string tipoDeDanio)
    {
        Debug.Log("Vida0: "+Vida);
        Debug.Log("recivo danio");
        tiempoSinRecibirDanio=0;
        Debug.Log("Vida1: "+Vida);
        int DanioInflijido= (int)CalcularDanioReal(danio,tipoDeDanio) ;
        Debug.Log("Vida2: "+Vida);
        Debug.Log("Danio inflijido: "+DanioInflijido);
        Vida-= DanioInflijido;
        Debug.Log("Vida3: "+Vida);
    }

    private float CalcularDanioReal (int danio, string tipoDeDanio){
        int vidaTotal = CalcularVidaTotal (tipoDeDanio);
        float porcentajeDeDanio = danio*100/vidaTotal;
        return  porcentajeDeDanio * vidaMax / 100 ;
    }

    private int CalcularVidaTotal(string tipoDeDanio){
        switch (tipoDeDanio){
            case "fisico": 
                return  vidaMax + defensaFisica;
            case "magico": 
                return  vidaMax + defensaMagica;
            case "verdadero": 
                return  vidaMax;
            default: //hacer algo para evitar poner esto ya que queda feo
                return vidaMax;
        }   
    }

    private void Muerte (){
        //Destroy(gameObject);
    }

}
