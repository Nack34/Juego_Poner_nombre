using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRange_CombatSubState : CombatSubState
{
    
    // constructor
    public ShortRange_CombatSubState (Entity entity, FiniteStateMachine stateMachine, string animationName, RandomActionSelector randomCombatActionSelector) : base(entity, stateMachine, animationName, randomCombatActionSelector){
 
   
    } 


    public override void Enter () {
        base.Enter();
    }

    public override void Exit () {
        base.Exit();
        entity.opponentSearchState.searchLevel = Enums.SearchLevel.ItWasInShortRange;
    }

    public override void LogicUpdate () { 
        base.LogicUpdate();

        entity.ClosestTarget = entity.ShortRangeVisibleOpponents.FindClosest(entity.transform.parent.transform.position); // funcion de KdTree
    }                                                                       

    public override void PhysicsUpdate() { 
        base.PhysicsUpdate();

    }

    public override void AnimationEnding(){ // es llamada por el script nexo animacion-estado. FALTA IMPLEMENTAR
        base.AnimationEnding();

    }
}
