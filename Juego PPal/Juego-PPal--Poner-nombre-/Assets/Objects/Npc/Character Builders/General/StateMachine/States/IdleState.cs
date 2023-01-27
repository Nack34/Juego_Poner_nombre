using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected bool stopIdle;
    
    protected RandomActionSelector randomActionSelector;

    // constructor
    public IdleState (Entity entity, FiniteStateMachine stateMachine, string animationName, RandomActionSelector randomActionSelector) : base(entity, stateMachine, animationName)
    {
        this.randomActionSelector = randomActionSelector; // ya viene cargado con las posibles animaciones a ejecutar
        
        
        
    } 
    
    public override void CheckStateTransitions (){
        
        
        if(entity.HasTarget){
            stateMachine.ChangeState(entity.opponentDetectedState);
        } else if (stopIdle){
            //Debug.Log("Termino IdleState");
            stateMachine.ChangeState(entity.moveState);
        }
    }

    public override void Enter () {
        base.Enter();
        entity.animationToStateMachine.currentState = this;
        entity.CurrentSpeed=0.0f;
        stopIdle = false;
        
        
    }

    public override void Exit () {
        base.Exit();
    }

    public override void LogicUpdate () {
        base.LogicUpdate();
        if ( (Time.time >=  startTime + 1.0f) && !entity.isInAction){
            // TriggerAction(AnimationStrings.IdleAction, randomActionSelector.SelectActionToUse()); // selecciona en el animator la prox animacion
            
            AnimationEnding(); // linea de codigo PROVISORIA. Sacar de aca al crear las animaciones idle y el script de random selector

        }

        
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

    }

    public override void AnimationEnding(){ // es llamada por el script nexo animacion-estado. FALTA IMPLEMENTAR
        base.AnimationEnding();
        stopIdle = true;
    }

    
}
