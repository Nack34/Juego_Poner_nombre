using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public State currentState;
    //private int globalCount = 0;
    private int specificTriggerAttackCount = 0;
    private int specificAnimationEndingCount = 0;
    private bool canTriggerAttack = true;
    private bool canAnimationEnding = false;


    private void TriggerAttack(){
        if (canTriggerAttack){
            canTriggerAttack = false;
            canAnimationEnding = true;

            //globalCount++;
            //Debug.Log("AnimationToStateMachine, TriggerAttack, globalCount = "+globalCount);
            

            specificTriggerAttackCount++;
            //Debug.Log("AnimationToStateMachine, TriggerAttack, specificTriggerAttackCount = "+specificTriggerAttackCount);

            currentState.TriggerAttack();

        }
    }
    
    private void AnimationEnding(){
        if (canAnimationEnding){
            canAnimationEnding = false;
            canTriggerAttack = true;

            //globalCount++;
            //Debug.Log("AnimationToStateMachine, AnimationEnding, globalCount = "+globalCount);

            
            specificAnimationEndingCount++;
            //Debug.Log("AnimationToStateMachine, AnimationEnding, specificAnimationEndingCount = "+specificAnimationEndingCount);

            currentState.AnimationEnding();

        }
    }

    private void Start(){
        canTriggerAttack = true;
        canAnimationEnding = false;
    }

    private void Update() {
        if (currentState != null){
            if ((specificAnimationEndingCount > specificTriggerAttackCount) || (specificTriggerAttackCount >= specificAnimationEndingCount+2)){
                Debug.LogError("AnimationToStateMachine dice: ERROR EN LA LLAMADA DE LOS EVENTOS DE ANIMACIONES DE ATAQUE");
                // esto es para probar con CombatState, cuando agregue idle state, o lo borro o cambio
            }
        }
    }

}
