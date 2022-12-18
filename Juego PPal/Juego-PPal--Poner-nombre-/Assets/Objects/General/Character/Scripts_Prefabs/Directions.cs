using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directions : MonoBehaviour // calcula la direccion a enviar a animator
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        int i=0;
        int j=0;
        foreach (var item in (Enums.PosibleDirections[])System.Enum.GetValues(typeof(Enums.PosibleDirections)))
        {
            PosibleDirections [i,j] = item; // llena una matriz con las posibles direcciones para que en Check Directions se elija
            i++;
            if (i==3){
                j++;
                i=0;
            }
        }
    }

    private Enums.PosibleDirections [,] PosibleDirections = new Enums.PosibleDirections [3,3];
    [SerializeField] private Enums.PosibleDirections direction= Enums.PosibleDirections.Down;
    public Enums.PosibleDirections Direction{ 
        set { 
            direction=value;
        }
        get {
            return direction;
        }
    }
    
    public void CheckDirection(Vector2 movementInput){
        if (movementInput != Vector2.zero){
            Direction = PosibleDirections[ChooseIndex(movementInput.x), ChooseIndex(movementInput.y)]; // no es utilizado para nada actualmente. IMPLEMENTAR su uso
            animator.SetFloat(AnimationStrings.DirectionX,movementInput.x);
            animator.SetFloat(AnimationStrings.DirectionY,movementInput.y);
        }
        else
            Debug.Log("Directions avisa: se intento setear la direccion como NotDirection. Esto no deberia haber pasado, la direccion quedara como la ultima");
    }

    private int ChooseIndex (float velocity){
        if (velocity>0)
            return 2;
        else if ( velocity==0) return 1;
             else return 0; // velocity < 0
    }

}
