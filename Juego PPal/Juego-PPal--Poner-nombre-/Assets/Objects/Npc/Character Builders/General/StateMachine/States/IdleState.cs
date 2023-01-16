using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    protected D_IdleState stateData;
    protected bool stopIdle;
    
    /*
    protected RandomAnimationSelector posibleIdleAnimations;
*/
    // constructor
    public IdleState (Entity entity, FiniteStateMachine stateMachine, string animationName, D_IdleState stateData/*, RandomAnimationSelector posibleIdleAnimations*/) : base(entity, stateMachine, animationName)
    {
        this.stateData=stateData;
        /*
        this.posibleIdleAnimations=posibleIdleAnimations; // ya viene cargado con las posibles animaciones a ejecutar
        */
        
        
    } 
    
    public override void CheckStateTransitions (){
        
        if (stopIdle){
            //Debug.Log("Termino IdleState");
            stateMachine.ChangeState(entity.moveState);
        }
    }


    public override void Enter () {
        base.Enter();
        entity.CurrentSpeed=0.0f;
        stopIdle = false;
        
        
    }

    public override void Exit () {
        base.Exit();
    }

    public override void LogicUpdate () {
        base.LogicUpdate();

        if ( Time.time >=  startTime + 1.0f){
        SeleccionarAnimacion(); // selecciona en el animator la prox animacion
        }

        
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

    }

    public void SeleccionarAnimacion (){ 
        //entity.animator.SetFloat("idleAnimation", posibleIdleAnimations.SelectAnimation()); // selecciona una de las posibles animaciones ...
                                                                                            // previamente seleccionadas
        AnimationEnding(); // linea de codigo PROVISORIA. Sacar de aca al crear las animaciones idle y el script de random selector

        
    }

    public void AnimationEnding(){ // es llamada por el script nexo animacion-estado 
        stopIdle = true;
    }

    
}
