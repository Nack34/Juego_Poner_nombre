using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directions : MonoBehaviour
{
    AnimationSelector animationSelector;

    private void Awake()
    {
        animationSelector = GetComponent<AnimationSelector>();

        int i=0;
        int j=0;
        foreach (var item in (Enums.PosibleDirections[])System.Enum.GetValues(typeof(Enums.PosibleDirections)))
        {
            PosibleDirections [i,j] = item;
            //Debug.Log(PosibleDirections [i,j]);
            i++;
            if (i==3){
                j++;
                i=0;
            }
        }
        float xIndex=-1f;
        float yIndex=-1f;
        for (i=0; i<3;i++){
            for (j=0; j<3;j++){
                Debug.Log("A ver que sale: ");
                Debug.Log("En x: "+(ChooseIndex(xIndex+i)-1));
                Debug.Log("En y: "+(ChooseIndex(yIndex+j)-1));
                Debug.Log(PosibleDirections[ChooseIndex(xIndex+i), ChooseIndex(yIndex+j)]);
            }
        }
    }

    
    private Enums.PosibleDirections [,] PosibleDirections = new Enums.PosibleDirections [3,3];
    [SerializeField] private Enums.PosibleDirections direction= Enums.PosibleDirections.Down;
    public Enums.PosibleDirections Direction{ // esto ya no hace falta
        set { 
            direction=value;
            animationSelector.currentDirection = value;
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
