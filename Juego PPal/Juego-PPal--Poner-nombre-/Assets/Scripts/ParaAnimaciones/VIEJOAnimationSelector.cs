using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIEJOAnimationSelector : MonoBehaviour
{
    /*Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator= GetComponent<Animator>();
    }

    public bool isRunning=false;
    public bool isMoving=false;
    public bool isArmed=false;  
    public Enums.PosibleHabilityClass currentHabilityClass = Enums.PosibleHabilityClass.combate;  
    public Enums.PosibleWeaponType currentWeaponType = Enums.PosibleWeaponType.dagger;
    public Enums.PosibleHabilityType currentHabilityType = Enums.PosibleHabilityType.idle; 
    public Enums.PosibleDirections currentDirection = Enums.PosibleDirections.down;  

    // Update is called once per frame
    void Update()
    {
        if (isArmed)
            ChooseHabilityClass();
        else
            ChooseMovementForUnarmed();
    }

    private void ChooseMovementForUnarmed (){
        if (isMoving){
            if (isRunning){
                ChooseDirectionsForUnarmedRun();
            } else {
                ChooseDirectionsForUnarmedWalk();
            }
        }else {
            ChooseDirectionsForUnarmedIdle();
        }
        
    }


    // unarmed run
    private void ChooseDirectionsForUnarmedRun (){
        switch (currentDirection)
        {
            case Enums.PosibleDirections.up: {
                animator.Play(AnimationStrings.unarmedRunUp);
                break;
            }
            case Enums.PosibleDirections.down: {
                animator.Play(AnimationStrings.unarmedRunDown);
                break;
            }
            case Enums.PosibleDirections.right: {
                animator.Play(AnimationStrings.unarmedRunLeft);
                break;
            }
            case Enums.PosibleDirections.left: {
                animator.Play(AnimationStrings.unarmedRunRight);
                break;
            }
        }
    }

    // unnarmed walk
    private void ChooseDirectionsForUnarmedWalk (){
        switch (currentDirection)
        {
            case Enums.PosibleDirections.up: {
                animator.Play(AnimationStrings.unarmedWalkUp);
                break;
            }
            case Enums.PosibleDirections.down: {
                animator.Play(AnimationStrings.unarmedWalkDown);
                break;
            }
            case Enums.PosibleDirections.right: {
                animator.Play(AnimationStrings.unarmedWalkLeft);
                break;
            }
            case Enums.PosibleDirections.left: {
                animator.Play(AnimationStrings.unarmedWalkRight);
                break;
            }
        }
    }




    // unnarmed Idle
    private void ChooseDirectionsForUnarmedIdle (){
        switch (currentDirection)
        {
            case Enums.PosibleDirections.up: {
                animator.Play(AnimationStrings.unnarmedIdleUp);
                break;
            }
            case Enums.PosibleDirections.down: {
                animator.Play(AnimationStrings.unnarmedIdleDown);
                break;
            }
            case Enums.PosibleDirections.right: {
                animator.Play(AnimationStrings.unnarmedIdleLeft);
                break;
            }
            case Enums.PosibleDirections.left: {
                animator.Play(AnimationStrings.unnarmedIdleRight);
                break;
            }
        }
    }



    













    private void ChooseHabilityClass (){
        switch (currentHabilityClass)
        {
            case Enums.PosibleHabilityClass.combate: {
//                ChooseWeaponType();
                break;
            }
            case Enums.PosibleHabilityClass.agricultura: {
//                ChooseToolTypeForAgricultura(); // se necesita Enums.PosibleToolsType
                break;
            }
            case Enums.PosibleHabilityClass.ganaderia: {
//               ChooseToolTypeForGanaderia(); // se necesita Enums.PosibleToolsType
                break;
            }
            case Enums.PosibleHabilityClass.unicas: {
//               ChooseUniqueHability (); // se necesita Enums.PosibleUniqueHability

                break;
            }
        }
    }



    private void ChooseWeaponType(){
        switch (currentWeaponType)
        {
            case Enums.PosibleWeaponType.dagger: {
                    ChooseHabilityTypeForArmedDagger();
                break;
            }
            case Enums.PosibleWeaponType.bow: {
                    ChooseHabilityTypeForArmedBow();
                break;
            }
            case Enums.PosibleWeaponType.sword: {
                    ChooseHabilityTypeForArmedSword();
                break;
            }
            case Enums.PosibleWeaponType.unicas: {
                    ChooseHabilityTypeForArmedMagic();

                break;
            }
        }
   }



    private void ChooseHabilityTypeForArmedDagger(){
        switch (currentWeaponType)
        {
            case Enums.PosibleWeaponType.dagger: {
                    ChooseHabilityTypeForArmedDagger();
                break;
            }
            case Enums.PosibleWeaponType.bow: {
                    ChooseHabilityTypeForArmedBow();
                break;
            }
            case Enums.PosibleWeaponType.sword: {
                    ChooseHabilityTypeForArmedSword();
                break;
            }
            case Enums.PosibleWeaponType.unicas: {
                    ChooseHabilityTypeForArmedMagic();

                break;
            }
        }
   }





    // armed dagger run
    private void ChooseDirectionsForArmedDaggerRun (){
        switch (currentDirection)
        {
            case Enums.PosibleDirections.up: {
                animator.Play(AnimationStrings.armedDaggerRunUp);
                break;
            }
            case Enums.PosibleDirections.down: {
                animator.Play(AnimationStrings.armedDaggerRunDown);
                break;
            }
            case Enums.PosibleDirections.right: {
                animator.Play(AnimationStrings.armedDaggerRunLeft);
                break;
            }
            case Enums.PosibleDirections.left: {
                animator.Play(AnimationStrings.armedDaggerRunRight);
                break;
            }
        }
    }

    private void ChooseDirectionsForArmedDaggerWalk (){
        switch (currentDirection)
        {
            case Enums.PosibleDirections.up: {
                animator.Play(AnimationStrings.armedDaggerWalkUp);
                break;
            }
            case Enums.PosibleDirections.down: {
                animator.Play(AnimationStrings.armedDaggerWalkDown);
                break;
            }
            case Enums.PosibleDirections.right: {
                animator.Play(AnimationStrings.armedDaggerWalkLeft);
                break;
            }
            case Enums.PosibleDirections.left: {
                animator.Play(AnimationStrings.armedDaggerWalkRight);
                break;
            }
        }
    }

    private void ChooseDirectionsForArmedDaggerIdle (){
        switch (currentDirection)
        {
            case Enums.PosibleDirections.up: {
                animator.Play(AnimationStrings.armedDaggerIdleUp);
                break;
            }
            case Enums.PosibleDirections.down: {
                animator.Play(AnimationStrings.armedDaggerIdleDown);
                break;
            }
            case Enums.PosibleDirections.right: {
                animator.Play(AnimationStrings.armedDaggerIdleLeft);
                break;
            }
            case Enums.PosibleDirections.left: {
                animator.Play(AnimationStrings.armedDaggerIdleRight);
                break;
            }
        }
    }


    private void ChooseDirectionsForArmedDaggerHability1 (){
        switch (currentDirection)
        {
            case Enums.PosibleDirections.up: {
                animator.Play(AnimationStrings.armedDaggerHability1Up);
                break;
            }
            case Enums.PosibleDirections.down: {
                animator.Play(AnimationStrings.armedDaggerHability1Down);
                break;
            }
            case Enums.PosibleDirections.right: {
                animator.Play(AnimationStrings.armedDaggerHability1Left);
                break;
            }
            case Enums.PosibleDirections.left: {
                animator.Play(AnimationStrings.armedDaggerHability1Right);
                break;
            }
        }
    }
*/

}
