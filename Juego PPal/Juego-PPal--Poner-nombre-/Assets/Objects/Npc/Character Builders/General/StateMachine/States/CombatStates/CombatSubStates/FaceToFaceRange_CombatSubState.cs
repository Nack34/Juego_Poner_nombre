using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToFaceRange_CombatSubState : CombatSubState
{
    
    // constructor
    public FaceToFaceRange_CombatSubState (Entity entity, FiniteStateMachine stateMachine, string animationName, D_CombatSubState stateData, RandomActionSelector randomCombatActionSelector) : base(entity, stateMachine, animationName, stateData, randomCombatActionSelector){
 
 
    } 


    public override void Enter () {
        base.Enter();
    }

    public override void Exit () {
        base.Exit();
    }

    public override void LogicUpdate () { 
        base.LogicUpdate();
        
        entity.closestTarget = entity.FaceToFaceRangeVisibleOpponents.FindClosest(entity.transform.parent.transform.position); // funcion de KdTree
    }

    public override void PhysicsUpdate() { 
        base.PhysicsUpdate();

    }

    public override void AnimationEnding(){ // es llamada por el script nexo animacion-estado. FALTA IMPLEMENTAR
        base.AnimationEnding();

    }
}
