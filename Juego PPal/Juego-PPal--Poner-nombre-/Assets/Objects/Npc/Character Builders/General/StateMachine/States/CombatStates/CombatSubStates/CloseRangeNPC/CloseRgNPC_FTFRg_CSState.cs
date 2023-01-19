using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CloseRangeNPC_FTFRange_CSState
public class CloseRgNPC_FTFRg_CSState : FaceToFaceRange_CombatSubState
{
    // constructor
    public CloseRgNPC_FTFRg_CSState (Entity entity, FiniteStateMachine stateMachine, string animationName, RandomActionSelector randomCombatActionSelector) : base(entity, stateMachine, animationName, randomCombatActionSelector){
 
 
    } 


    public override void Enter () {
        base.Enter();
        Debug.Log("Enter en FTF");
    }

    public override void Exit () {
        base.Exit();
        Debug.Log("Exit en FTF");
    }

    public override void LogicUpdate () { 
        base.LogicUpdate();
        //Debug.Log("LogicUpdate en FTF");

        //if (no esta en el rango de combate){
            if (!entity.destinationSetter.ai.canMove){
                entity.destinationSetter.ai.canMove = true;
            }
            entity.destinationSetter.target = entity.ClosestTarget;
        //} else {Combate()}
    }

    public override void PhysicsUpdate() { 
        base.PhysicsUpdate();

    }

    public override void AnimationEnding(){ // es llamada por el script nexo animacion-estado. FALTA IMPLEMENTAR
        base.AnimationEnding();

    }
}
