using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Usa moverse sin chocarse que tiene que estar en entity
public class CombatSubState : State
{
    protected RandomActionSelector randomCombatActionSelector;

    // constructor
    public CombatSubState (Entity entity, FiniteStateMachine stateMachine, string animationName, RandomActionSelector randomCombatActionSelector) : base(entity, stateMachine, animationName){
        
        // stateMachine al igual que animation no se usa. No se puede sacar de alguna manera?
    } 

    
    public override void CheckStateTransitions () { // NO LO USO EN LOS SUBSTATES
        base.CheckStateTransitions(); // NO LO USO EN LOS SUBSTATES
    } // NO LO USO EN LOS SUBSTATES

    public override void Enter () {
        base.Enter();
        if (entity.ClosestTarget != null){
            entity.LookingDirection = entity.CalculateDirection(entity.ClosestTarget.position);
        }

    }

    public override void Exit () {
        base.Exit();
        
        entity.destinationSetter.target = entity.ClosestTarget = null; 
    }

    public override void LogicUpdate () { 
        base.LogicUpdate();
        if (entity.ClosestTarget != null){
            entity.LookingDirection = entity.CalculateDirection(entity.ClosestTarget.position);
        }
        for (int i = 0; i < System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length; i++)
        {
            entity.visibleOpponents[i].UpdatePositions(); // funcion de KdTree
        }
    }
    
    /*
    public virtual void Combat() { 
        entity.StopComplexMovement();
        

    }
    */

    public override void PhysicsUpdate() { 
        base.PhysicsUpdate();

    }

    public override void AnimationEnding(){ // es llamada por el script nexo animacion-estado. FALTA IMPLEMENTAR
        base.AnimationEnding();

    }
}
