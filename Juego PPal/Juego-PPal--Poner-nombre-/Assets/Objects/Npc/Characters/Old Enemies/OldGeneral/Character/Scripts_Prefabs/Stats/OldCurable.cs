using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(OldStats))]
public class OldCurable : MonoBehaviour
{

    
    OldStats stats;
//    AnimationSelector animationSelector;
    private void Awake() {
//        animationSelector = GetComponent<AnimationSelector>();
        stats = GetComponent<OldStats>();
    }




    private float vidaMax;
    [SerializeField] private int segundosNecesariosParaCurarse= 10;
    private float temporizadorDeSegundos=0f; //temporizadorDeSegundos es para que cada un segundo cheequee si puedo curarme
    private float curacionAcumulada = 0f;
    
    float tiempoSinRecibirDanio;
    void Update()
    {
        vidaMax = stats.vidaMax;
        tiempoSinRecibirDanio = stats.TiempoSinRecibirDanio;

        temporizadorDeSegundos+=Time.deltaTime;
        if (temporizadorDeSegundos>=1f){ //cada un segundo cheequeo si puedo curarme
            temporizadorDeSegundos=0f;

            tiempoSinRecibirDanio++;
            if ((tiempoSinRecibirDanio>=segundosNecesariosParaCurarse) && (stats.Vida<vidaMax) ){
                curacionAcumulada = CantCuracion(curacionAcumulada);
                curacionAcumulada = Curarse(curacionAcumulada);
            }
        }
    }

    [SerializeField] private float cantPorcentajeCuracionXSegundo = 10;
    private float CantCuracion (float curacionAcumulada){ // la curacion es de manera pasiva, cada cierta cant de tiempo sin recibir danio, me curo el (cantPorcentajeCuracionXSegundo)% de la vida    
            return curacionAcumulada + ((vidaMax * cantPorcentajeCuracionXSegundo)/100);
        }
    
    private float Curarse (float curacionAcumulada){
        if ((int)curacionAcumulada>0){
            stats.Vida+=(int)curacionAcumulada;
            return 0;
        }
        return curacionAcumulada;
    }

}
