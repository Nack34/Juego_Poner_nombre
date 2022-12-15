using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSelector : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator= GetComponent<Animator>();
    }

    public bool isAlive=true;
    public bool hitted=false;
    public bool isArmed=false;
    public bool isUsingHability=false; 
    public Enums.PosibleHabilityClass currentHabilityClass = Enums.PosibleHabilityClass.Combate;  
    public Enums.PosibleWeaponType currentWeaponType = Enums.PosibleWeaponType.Dagger;
    public Enums.PosibleHabilityType currentHabilityType = Enums.PosibleHabilityType.Idle; 
    public Enums.PosibleDirections currentDirection = Enums.PosibleDirections.Down;  

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) animator.Play(AnimationStrings.Death);
        else
            if (hitted) animator.Play(AnimationStrings.Hitted + AnimationStrings.PosibleDirections[(int)currentDirection]);
            else 
                if (!isArmed) 
                    animator.Play(AnimationStrings.Unarmed +  
                                  AnimationStrings.PosibleHabilityType[(int)currentHabilityType]+ // solo de movimiento
                                  AnimationStrings.PosibleDirections[(int)currentDirection]         );
                else
                    animator.Play(AnimationStrings.PosibleHabilityClass[(int)currentHabilityClass]+
                                  AnimationStrings.PosibleWeaponType[(int)currentWeaponType]+  
                                  AnimationStrings.PosibleHabilityType[(int)currentHabilityType]+
                                  AnimationStrings.PosibleDirections[(int)currentDirection]         );
                   
    }
}


