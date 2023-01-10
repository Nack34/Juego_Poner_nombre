using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    protected D_IdleState stateData;
    protected bool stopIdle;
    //protected RandomAnimationSelector randomAnimatorSelector; (lo q le dije a alvaro) // pasarlo a entity
    
    [SerializeField]
    protected List<string> posibleIdleAnimations= new List<string>();

    // constructor
    public IdleState (Entity entity, FiniteStateMachine stateMachine, string animationName, D_IdleState stateData, List<string> posibleIdleAnimations) : base(entity, stateMachine, animationName)
    {
        this.stateData=stateData;
        this.posibleIdleAnimations=posibleIdleAnimations;
        
        /* pasar lo siguiente a entity (posibleIdleAnimations ya viene cargado) 
        foreach (var animation in System.Enum.GetValues(typeof(D_IdleState.PosibleIdleAnimations)))
        {
            Debug.Log("Idle state dice: "+ animation.ToString());
            posibleIdleAnimations.Add(animation.ToString());
        }
        */ 
    } 
    
    
    public override void Enter () {
        base.Enter();
        stopIdle = false;
        //randomAnimatorSelector = new RandomAnimationSelector(posibleIdleAnimations); (lo q le dije a alvaro) 
        SeleccionarAnimacion(); // selecciona en el animator la prox animacion, 
    }

    public override void Exit () {
        base.Exit();
    }

    public override void LogicUpdate () {
        base.LogicUpdate();
        
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

    }

    public void SeleccionarAnimacion (){ 
        //entity.animator.SetFloat("idleAnimation", entity.randomAnimatorSelector.SelectAnimation());
    }

    public void AnimationEnding(){ // es llamada por el script nexo animacion-estado 
        stopIdle = true;
    }

    
}
