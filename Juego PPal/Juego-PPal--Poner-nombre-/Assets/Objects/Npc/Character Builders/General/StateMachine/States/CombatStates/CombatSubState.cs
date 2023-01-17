using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Usa moverse sin chocarse que tiene que estar en entity
public class CombatSubState : State
{
    protected D_CombatSubState stateData;
    protected RandomActionSelector randomCombatActionSelector;

    // constructor
    public CombatSubState (Entity entity, FiniteStateMachine stateMachine, string animationName, D_CombatSubState stateData, RandomActionSelector randomCombatActionSelector) : base(entity, stateMachine, animationName){
        this.stateData = stateData;
        
        // stateMachine al igual que animation no se usa. No se puede sacar de alguna manera?
    } 

    
    public override void CheckStateTransitions () { // NO LO USO EN LOS SUBSTATES
        base.CheckStateTransitions(); // NO LO USO EN LOS SUBSTATES
    } // NO LO USO EN LOS SUBSTATES

    public override void Enter () {
        base.Enter();
    }

    public override void Exit () {
        base.Exit();
    }

    public override void LogicUpdate () { 
        base.LogicUpdate();
        for (int i = 0; i < System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length; i++)
        {
            entity.visibleOpponents[i].UpdatePositions(); // funcion de KdTree
        }

    }

    public override void PhysicsUpdate() { 
        base.PhysicsUpdate();

    }

    public override void AnimationEnding(){ // es llamada por el script nexo animacion-estado. FALTA IMPLEMENTAR
        base.AnimationEnding();

    }
}
