using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directions : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public enum PosibleDirections {down,up,left,right}
    public PosibleDirections direction=PosibleDirections.down;
    public PosibleDirections Direction{ // esto ya no hace falta
        set { 
            direction=value;
            switch (direction)
            {
                case PosibleDirections.up: {
                    animator.SetBool(AnimationStrings.isLookingUp,true);
                    animator.SetBool(AnimationStrings.isLookingDown,false);
                    animator.SetBool(AnimationStrings.isLookingLeft,false);
                    animator.SetBool(AnimationStrings.isLookingRight,false);
                    break;
                }
                case PosibleDirections.down: {
                    animator.SetBool(AnimationStrings.isLookingUp,false);
                    animator.SetBool(AnimationStrings.isLookingDown,true);
                    animator.SetBool(AnimationStrings.isLookingLeft,false);
                    animator.SetBool(AnimationStrings.isLookingRight,false);
                    break;
                }
                case PosibleDirections.right: {
                    animator.SetBool(AnimationStrings.isLookingUp,false);
                    animator.SetBool(AnimationStrings.isLookingDown,false);
                    animator.SetBool(AnimationStrings.isLookingLeft,false);
                    animator.SetBool(AnimationStrings.isLookingRight,true);
                    break;
                }
                case PosibleDirections.left: {
                    animator.SetBool(AnimationStrings.isLookingUp,false);
                    animator.SetBool(AnimationStrings.isLookingDown,false);
                    animator.SetBool(AnimationStrings.isLookingLeft,true);
                    animator.SetBool(AnimationStrings.isLookingRight,false);
                    break;
                }
            }
        }
        get {
            return direction;
        }
    }
    
    // IMPLEMENTAR: ACA MODIFICAR LA DETECTION ZONE (si no es null) DEL OBJETO y el box collider del arma (traer el arma actual)
    public void CheckDirection(Vector2 movementInput){ // le da prioridad al eje X
        if (movementInput.x == 0f)
            if (movementInput.y > 0f)
                Direction = PosibleDirections.up; 
            else Direction = PosibleDirections.down;
        else if (movementInput.x > 0f)
            Direction = PosibleDirections.right;
            else Direction = PosibleDirections.left;
    }



}
