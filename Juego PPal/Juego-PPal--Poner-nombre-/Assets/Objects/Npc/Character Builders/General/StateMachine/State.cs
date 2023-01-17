using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;

    protected float startTime;
    protected bool isInAction = false;
    protected string animationName;

    // constructor
    public State (Entity entity, FiniteStateMachine stateMachine, string animationName){
        this.entity=entity;
        this.stateMachine=stateMachine;
        this.animationName=animationName;
    } 

    public virtual void CheckStateTransitions () { // aca van todas las tranciciones e/ estados (depende del estado)
        
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

    public virtual void AnimationEnding(){ // es llamada por el script nexo animacion-estado. FALTA IMPLEMENTAR
        
        
        isInAction = false;
    }

    // ---
    
    protected void TriggerAction (string actionName, RandomActionSelector randomActionSelector){
        isInAction = true;
        //entity.animator.SetFloat("idleAction", randomActionSelector.SelectActionToUse()); // selecciona una de las posibles animaciones ...
                                                                                            // previamente seleccionadas
        //entity.animator.SetFloat("idleAction", 0); // la vuelve a 0
       
    }
    

}
