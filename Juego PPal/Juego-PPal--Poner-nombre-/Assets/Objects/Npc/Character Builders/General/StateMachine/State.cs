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

    public virtual void AnimationEnding(){ // es llamada por el script nexo animacion-estado. FALTA IMPLEMENTAR (solo se usa en idle y combat animatios)
        entity.isInAction = false;
    }

    public virtual void TriggerAttack(){ // es llamada por el script nexo animacion-estado. FALTA IMPLEMENTAR (solo se usa en Combat animations)

    }

    // ---

    
    protected void TriggerAction (string actionName, int actionSelected){
        entity.isInAction = true;
        //entity.animator.SetFloat(actionName, actionSelected); // selecciona una de las posibles animaciones ...
                                                                // ... previamente seleccionadas
        //entity.animator.SetFloat(actionName, 0); // la vuelve a 0
       
    }
    

}
