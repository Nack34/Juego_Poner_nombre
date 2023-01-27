using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpDetSS_LongRangeBehaviour : OpponentDetectedSubState
{
    // constructor
    public OpDetSS_LongRangeBehaviour (Entity entity, FiniteStateMachine stateMachine, string animationName) : base(entity, stateMachine, animationName){
 
    } 


    protected override Transform ClosestTargetInCurrentRange(){
        return entity.LongRangeVisibleOpponents.FindClosest(entity.CurrentPosition); // funcion de KdTree
    }

    protected override bool IsInAttackRange(){
        return entity.LinearDistanceTo(entity.ClosestTarget.position) <=
        ((entity.entityData.speciesData.visionRadius[(int)Enums.PosibleFOVRanges.LongRange] - 
        entity.entityData.speciesData.visionRadius[(int)Enums.PosibleFOVRanges.ShortRange]) / 2);
    }

    protected override void SetSearchLevel(){
        entity.opponentSearchState.searchLevel = Enums.SearchLevel.ItWasInLongRange;
    }
}
