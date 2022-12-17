using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSelector : MonoBehaviour // obtiene datos y selecciona la animacion
{
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator= GetComponent<Animator>();
    }
    private string animacion;
    private bool seguirAnimando=true;
    public bool SeguirAnimando {
        set {
            seguirAnimando=value;
//            if (seguirAnimando)
//                animator.Play(AnimationStrings.Revivir); // <- IMPLEMENTAR (ACA NO)
        }
        get {
            return seguirAnimando; // return animator.seguirAnimando (no quiero que cualquier animacion pueda ...
                                   // ... pisar a cualquier otra. Solo quiero q la animacion cambie OnExit) <- IMPLEMENTAR
        }
    }
    public bool isAlive=true;
    public bool hitted=false; 
    public Enums.PosibleHabilityClass currentHabilityClass = Enums.PosibleHabilityClass.Unarmed;  
    public Enums.PosibleWeaponType currentWeaponType = Enums.PosibleWeaponType.Dagger;
    public Enums.PosibleHabilityType currentHabilityType = Enums.PosibleHabilityType.Idle; 
    public Enums.PosibleDirections currentDirection = Enums.PosibleDirections.Down;  

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { animacion = AnimationStrings.Death; 
            SeguirAnimando=false;
            }
        else // is alive
            if (hitted) animacion = AnimationStrings.Hitted + AnimationStrings.PosibleDirections[(int)currentDirection];
            else 
                if (!(currentHabilityClass == Enums.PosibleHabilityClass.Unarmed)) // if (isArmed)
                    animacion=( AnimationStrings.PosibleHabilityClass[(int)currentHabilityClass]+
                                AnimationStrings.PosibleWeaponType[(int)currentWeaponType]+  
                                AnimationStrings.PosibleHabilityType[(int)currentHabilityType]+
                                AnimationStrings.PosibleDirections[(int)currentDirection]         );
                else // isUnarmed
                    if (currentHabilityType == Enums.PosibleHabilityType.Idle || currentHabilityType == Enums.PosibleHabilityType.Walk|| currentHabilityType == Enums.PosibleHabilityType.Run)
                        animacion=( AnimationStrings.Unarmed +  
                                    AnimationStrings.PosibleHabilityType[(int)currentHabilityType]+ // solo de movimiento
                                    AnimationStrings.PosibleDirections[(int)currentDirection]         );
        if (SeguirAnimando) {
            animator.Play(animacion);
        }
    }
}


