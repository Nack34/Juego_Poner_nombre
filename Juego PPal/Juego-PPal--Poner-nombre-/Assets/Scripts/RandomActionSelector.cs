using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomActionSelector 
{
    private int[] posibleAnimations; 
    private float[] animationsSeleccionProbabilities;

    private float[] animationsUseProbabilities;
    private int[] posibleAnimationsToUse;

    public RandomActionSelector(int[] posibleAnimations, float[] animationsSeleccionProbabilities, float[] animationsUseProbabilities){
        this.posibleAnimations = posibleAnimations;
        this.animationsSeleccionProbabilities=animationsSeleccionProbabilities;
        this.animationsUseProbabilities = animationsUseProbabilities;
    }

    public void Initialize(){
        // se cargan las posibles animaciones a usar en posibleAnimationsToUse, teniendo en cuenta animationsSeleccionProbabilities;
    }

    /*public int SelectActionToUse(){
        selecciona y devuelve un elemento de posibleAnimationsToUse, teniendo en cuenta animationsUseProbabilities;
    }*/


}
