using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger_MoveState : MoveState
{
    Dagger dagger;

    public Dagger_MoveState (Entity entity, FiniteStateMachine stateMachine, string animationName, D_MoveState stateData, Dagger dagger) : base(entity, stateMachine, animationName, stateData)
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
        if (!isMoving){
            Debug.Log("Pasa a Idle");
            stateMachine.ChangeState(dagger.idleState);
        }

    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

    }
}
