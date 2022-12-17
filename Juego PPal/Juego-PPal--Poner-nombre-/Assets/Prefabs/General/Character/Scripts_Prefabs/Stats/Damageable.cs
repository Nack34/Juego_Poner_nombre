using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour // recive el danio que se quiere realizar, se calcula el danio real a realizar (se trar )
{
    // Start is called before the first frame update
    
    Stats stats;
    AnimationSelector animationSelector;
    private void Awake() {
        animationSelector = GetComponent<AnimationSelector>();
        stats = gameObject.GetComponent<Stats>();
    }

    void Start(){

    }

    public bool isInvincible=false; // es publico ya que por si se necesita (para animaciones o pociones de invincibilidad? ) 
    public float tiempoDeInvincibilidad = 0.2f;
    public float tiempoDesdeComienzoDeInvencibilidad = 0f;
    void Update()
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
    
    private bool isAlive=true; // utilizado para condiciones y la animacion de morir
    public bool IsAlive{
        get{
            return isAlive;
        }
        set{
            isAlive=value;
            animationSelector.isAlive=value;
        }
    }



    private int vidaMax; // utilizado para calcular el danio real recibido
    public void RecibirDanio (int danio, Enums.PosibleDamageType tipoDeDanio)
    {
        if (IsAlive && !isInvincible) {
            animationSelector.hitted = true;  
            gameObject.GetComponent<Stats>().TiempoSinRecibirDanio=0;
            isInvincible=true;
            vidaMax=gameObject.GetComponent<Stats>().vidaMax;

            float DanioInflijido= CalcularDanioReal(danio,tipoDeDanio) ;
            if (DanioInflijido<1f) // el minimo de danio es 1
                DanioInflijido=1f;
            gameObject.GetComponent<Stats>().Vida-= (int)DanioInflijido;
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

    public void Muerte (){ // IMPLEMENTAR: SI EL PLAYER MUERE, SE DESACTIVA, NO SE DESTRUYE. Si no es el player, tratar de usar la pileta de spawneo esa (del video)
        Debug.Log("muerte de player");
        IsAlive=false; // (modifica la variable isAlive y el animator)
        //Destroy(gameObject);
    }

}
