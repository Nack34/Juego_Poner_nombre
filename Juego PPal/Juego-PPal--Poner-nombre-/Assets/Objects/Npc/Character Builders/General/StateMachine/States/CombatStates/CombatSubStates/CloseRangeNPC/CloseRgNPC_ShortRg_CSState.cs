using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CloseRangeNPC_ShortRange_CSState
public class CloseRgNPC_ShortRg_CSState : ShortRange_CombatSubState
{
    // constructor
    public CloseRgNPC_ShortRg_CSState (Entity entity, FiniteStateMachine stateMachine, string animationName, RandomActionSelector randomCombatActionSelector) : base(entity, stateMachine, animationName, randomCombatActionSelector){
 
   
    } 


    public override void Enter () {
        base.Enter();
        Debug.Log("Enter en Short");
    }

    public override void Exit () {
        base.Exit();
        Debug.Log("Exit en Short");
    }

    public override void LogicUpdate () { 
        base.LogicUpdate();
        //Debug.Log("LogicUpdate en Short");

        //if (no esta en el rango de combate){
            entity.destinationSetter.target = entity.ClosestTarget;
            entity.StartComplexMovement();
        //} else {Combat()}
    }

    public override void PhysicsUpdate() { 
        base.PhysicsUpdate();

    }

    public override void AnimationEnding(){ // es llamada por el script nexo animacion-estado. FALTA IMPLEMENTAR
        base.AnimationEnding();

    }
}
