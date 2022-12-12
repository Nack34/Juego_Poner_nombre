using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
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
            animator.SetBool(AnimationStrings.isAlive,value);
        }
    }



    private int vidaMax; // utilizado para calcular el danio real recibido
    public void RecibirDanio (int danio, string tipoDeDanio)
    {
        if (IsAlive && !isInvincible) {
            animator.SetTrigger(AnimationStrings.isBeingHitted);
            gameObject.GetComponent<Stats>().TiempoSinRecibirDanio=0;
            isInvincible=true;
            vidaMax=gameObject.GetComponent<Stats>().vidaMax;

            float DanioInflijido= CalcularDanioReal(danio,tipoDeDanio) ;
            if (DanioInflijido<1f) // el minimo de danio es 1
                DanioInflijido=1f;
            gameObject.GetComponent<Stats>().Vida-= (int)DanioInflijido;
        }
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

    public void Muerte (){
        Debug.Log("muerte de player");
        IsAlive=false; // (modifica la variable isAlive y el animator)
        //Destroy(gameObject);
    }

}
