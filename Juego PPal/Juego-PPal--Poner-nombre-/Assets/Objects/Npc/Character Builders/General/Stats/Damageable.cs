using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable 
{
    private EntityStats stats;
    
    public Damageable(EntityStats stats){
        this.stats=stats;
    }
    
/*


    public bool isInvincible=false; // es publico ya que por si se necesita (para animaciones o pociones de invincibilidad? ) 
    public float tiempoDeInvincibilidad = 0.2f;
    [SerializeField] private float tiempoDesdeComienzoDeInvencibilidad = 0f;
    private void Update()
    {
        if (isInvincible) {
            tiempoDesdeComienzoDeInvencibilidad+=Time.deltaTime;
            if (tiempoDesdeComienzoDeInvencibilidad >= tiempoDeInvincibilidad)
            {
                isInvincible=false;
                tiempoDesdeComienzoDeInvencibilidad=0;
            }
        }
    }
    
    private int vidaMax; // utilizado para calcular el danio real recibido
    public void RecibirDanio (int danio, Enums.PosibleDamageType tipoDeDanio) // Funcion llamada por otros objetos
    {
        if (stats.IsAlive && !isInvincible) {
            animator.SetTrigger(AnimationStrings.Hitted);  
            stats.TiempoSinRecibirDanio=0;
            isInvincible=true; // no se q hacer con esto
            vidaMax=stats.vidaMax;

            float DanioInflijido= CalcularDanioReal(danio,tipoDeDanio) ;
            if (DanioInflijido>=vidaMax) // no se permiten one-shots
                DanioInflijido=vidaMax-1;
            if (DanioInflijido<1.0f) // el minimo de danio es 1
                DanioInflijido=1.0f;
            stats.Vida-= (int)DanioInflijido;
        }
    }

    private float CalcularDanioReal (int danio, Enums.PosibleDamageType tipoDeDanio){
        int vidaTotal = CalcularVidaTotal (tipoDeDanio);
        float porcentajeDeDanio = danio*100/vidaTotal;
        return  porcentajeDeDanio * vidaMax / 100 ;
    }

    private int CalcularVidaTotal(Enums.PosibleDamageType tipoDeDanio){
        return vidaMax + stats.vectorDeDefensas[(int)tipoDeDanio];
    }


*/
}
