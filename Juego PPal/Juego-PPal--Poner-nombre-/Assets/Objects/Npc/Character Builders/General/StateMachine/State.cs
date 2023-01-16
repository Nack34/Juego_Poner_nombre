using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;

    protected float startTime;

    protected string animationName;

    // constructor
    public State (Entity entity, FiniteStateMachine stateMachine, string animationName){
        this.entity=entity;
        this.stateMachine=stateMachine;
        this.animationName=animationName;
    } 

    public virtual void CheckStateTransitions () {
        
    }
    public virtual void Enter () {
        startTime=Time.time;
        entity.animator.SetBool(animationName, true);
    }

    public virtual void Exit () {
        entity.animator.SetBool(animationName, false);
    }

    public virtual void LogicUpdate () {
        CheckStateTransitions();
    }

    public virtual void PhysicsUpdate() {

    }
    

}
