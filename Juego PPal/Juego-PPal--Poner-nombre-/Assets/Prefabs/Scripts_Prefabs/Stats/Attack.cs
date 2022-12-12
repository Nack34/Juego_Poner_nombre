using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private float temporizadorDeIsAttacking=0.5f, tiempoQueIsAttacking=0f;
    void Update()
    {
        tiempoQueIsAttacking+=Time.deltaTime;
        if (tiempoQueIsAttacking>=temporizadorDeIsAttacking){
            tiempoQueIsAttacking=0f;
            animator.SetBool(AnimationStrings.isAttacking,false);        
        }
    }


    private int TipoArma{
        get{
            return (int) gameObject.GetComponent<StatsWeapon>().tipoArmaActual;
        }
    }
    private int TipoHabilidad{
        get{
            return (int) gameObject.GetComponent<StatsWeapon>().tipoHabilidad;
        }
    }

    public void NormalAttack() // clic izquierdo de mouse
    { // ACLARACION: el animator tiene un script para activar el collider del arma
        animator.SetInteger(AnimationStrings.tipoArma,TipoArma);
        animator.SetInteger(AnimationStrings.tipoHabilidad,0); // la habilidad 0 es la del golpe normal. Lograr que se entienda sin comentario. Enum?
        animator.SetBool(AnimationStrings.isAttacking,true);
        tiempoQueIsAttacking=0f;
    }

    public void Disparo (){ // al soltar el clic izquierdo de mouse
        //if (TipoArma = 1 || TipoArma = 2) Podria ser q solo las armas a distancia? las armas cuerpo a cuerpo no crearian conflicto 
            animator.SetTrigger(AnimationStrings.disparo); 
    }

}
