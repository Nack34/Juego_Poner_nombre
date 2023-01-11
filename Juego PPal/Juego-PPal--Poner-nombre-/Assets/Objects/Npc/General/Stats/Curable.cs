using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curable 
{
    private NPCStats stats;
    private float segundosNecesariosParaCurarse;
    private float cantPorcentajeCuracionXSegundo;
    
    public Curable(NPCStats stats, float segundosNecesariosParaCurarse, float cantPorcentajeCuracionXSegundo){
        this.stats=stats;
        this.segundosNecesariosParaCurarse = segundosNecesariosParaCurarse;
        this.cantPorcentajeCuracionXSegundo = cantPorcentajeCuracionXSegundo;
    }

    private float tiempoDeCuracion = Time.time; 
    private float curacionAcumulada = 0f;
    
    public void LogicUpdate(float tiempoSinRecibirDanio)  
    {
        if (Time.time >= tiempoDeCuracion + 1.0f ){ //cada un segundo cheequeo si puedo curarme
            tiempoDeCuracion=Time.time;;

            if ((tiempoSinRecibirDanio>=segundosNecesariosParaCurarse) && (stats.Vida<stats.vidaMax) ){
                curacionAcumulada = CantCuracion(curacionAcumulada);
                curacionAcumulada = Curarse(curacionAcumulada);
            }
        }
    }

    private float CantCuracion (float curacionAcumulada){ // la curacion es de manera pasiva, cada cierta cant de tiempo sin recibir danio, me curo el (cantPorcentajeCuracionXSegundo)% de la vida    
            return curacionAcumulada + ((stats.vidaMax * cantPorcentajeCuracionXSegundo)/100);
        }
    
    private float Curarse (float curacionAcumulada){
        if ((int)curacionAcumulada>0){
            stats.Vida+=(int)curacionAcumulada;
            return 0;
        }
        return curacionAcumulada;
    }


}

