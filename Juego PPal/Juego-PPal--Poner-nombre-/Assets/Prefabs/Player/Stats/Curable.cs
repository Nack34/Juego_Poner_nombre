using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curable : MonoBehaviour
{
    // Start is called before the first frame update
    int vidaMax;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField] private int segundosNecesariosParaCurarse= 10;
    private float temporizadorDeSegundos=0f; //temporizadorDeSegundos es para que cada un segundo cheequee si puedo curarme
    private float curacionAcumulada = 0;
    
    float tiempoSinRecibirDanio;
    void FixedUpdate()
    {
        vidaMax = gameObject.GetComponent<Stats>().vidaMax;
        tiempoSinRecibirDanio = gameObject.GetComponent<Stats>().TiempoSinRecibirDanio;

        temporizadorDeSegundos+=Time.fixedDeltaTime;
        if (temporizadorDeSegundos>=1f){ //cada un segundo cheequeo si puedo curarme
            temporizadorDeSegundos=0f;

            tiempoSinRecibirDanio++;
            if ((tiempoSinRecibirDanio>=segundosNecesariosParaCurarse) && (gameObject.GetComponent<Stats>().Vida<vidaMax) ){
                curacionAcumulada = CantCuracion(curacionAcumulada);
                curacionAcumulada = Curarse(curacionAcumulada);
            }
        }
    }

    [SerializeField] private int cantPorcentajeCuracionXSegundo = 10;
    private float CantCuracion (float curacionAcumulada){ // la curacion es de manera pasiva, cada cierta cant de tiempo sin recibir danio, me curo el (cantPorcentajeCuracionXSegundo)% de la vida    
            return curacionAcumulada + ((vidaMax * cantPorcentajeCuracionXSegundo)/100);
        }
    
    private float Curarse (float curacionAcumulada){
        if ((int)curacionAcumulada>0){
            gameObject.GetComponent<Stats>().Vida+=(int)curacionAcumulada;
            return 0;
        }
        return curacionAcumulada;
    }

}
