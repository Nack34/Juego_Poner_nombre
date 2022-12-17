using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directions : MonoBehaviour // calcula la direccion a enviar a AnimationSelector
{
    AnimationSelector animationSelector;

    private void Awake()
    {
        animationSelector = GetComponent<AnimationSelector>();

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
            if (!(value == Enums.PosibleDirections.NotDirection)){
                direction=value;
            }
            else {
//              Debug.Log("Script Directions avisa: Se intento setear la direccion NotDirection. La direccion mantendria su valor previo");
            }
            animationSelector.currentDirection = direction;
        }
        get {
            return direction;
        }
    }
    
    // IMPLEMENTAR: EN LAS ANIMACIONES MODIFICAR LA DETECTION ZONE DEL OBJETO y el box collider del arma (traer el arma actual)
    public void CheckDirection(Vector2 movementInput){ // le da prioridad al eje X
        Direction = PosibleDirections[ChooseIndex(movementInput.x), ChooseIndex(movementInput.y)];
    }

    private int ChooseIndex (float velocity){
        if (velocity>0)
            return 2;
        else if ( velocity==0) return 1;
             else return 0; // velocity < 0
    }

}
