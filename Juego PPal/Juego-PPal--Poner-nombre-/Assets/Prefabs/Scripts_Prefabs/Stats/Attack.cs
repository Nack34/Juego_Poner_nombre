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
    void Update()
    {
        
    }


    public int TipoArma{
        get{
            return gameObject.GetComponent<StatsWeapon>().tipoArmaActual;
        }
    }
    private int TipoHabilidad{
        get{
            return gameObject.GetComponent<StatsWeapon>().TipoHabilidad;
        }
    }

    public void NormalAttack() // clic izquierdo de mouse
    { // ACLARACION: el animator tiene un script para activar el collider del arma
        animator.SetInteger(AnimationStrings.tipoArma,TipoArma);
        animator.SetInteger(AnimationStrings.tipoHabilidad,0); // la habilidad 0 es la del golpe normal. Lograr que se entienda sin comentario. Enum?
        animator.SetTrigger(AnimationStrings.isAttacking); 
    }

    public void Disparo (){ // al soltar el clic izquierdo de mouse
        //if (TipoArma = 1 || TipoArma = 2) Podria ser q solo las armas a distancia? las armas cuerpo a cuerpo no crearian conflicto ce
            animator.SetTrigger(AnimationStrings.disparo); 
    }

}
