using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger_IdleState : IdleState
{
    Dagger dagger;
    
    public Dagger_IdleState (Entity entity, FiniteStateMachine stateMachine, string animationName, D_IdleState stateData, List<string> posibleIdleAnimations, Dagger dagger) : base(entity, stateMachine, animationName, stateData, posibleIdleAnimations)
    {
        this.dagger = dagger;
    } 
    
    public override void Enter () {
        base.Enter();
        
    }

    public override void Exit () {
        base.Exit();
        
    }

    public override void LogicUpdate () {
        base.LogicUpdate();
        
        if (stopIdle){
            //Debug.Log("Termino IdleState");
            stateMachine.ChangeState(dagger.moveState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

    }
}
