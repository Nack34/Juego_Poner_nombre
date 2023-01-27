using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpDetSS_FaceToFaceBehaviour : OpponentDetectedSubState
{
    // constructor
    public OpDetSS_FaceToFaceBehaviour (Entity entity, FiniteStateMachine stateMachine, string animationName) : base(entity, stateMachine, animationName){
 
    } 


    protected override Transform ClosestTargetInCurrentRange(){
        return entity.FaceToFaceRangeVisibleOpponents.FindClosest(entity.CurrentPosition); // funcion de KdTree
    }

    protected override bool IsInAttackRange(){
        return entity.LinearDistanceTo(entity.ClosestTarget.position) <=
        (entity.entityData.speciesData.visionRadius[(int)Enums.PosibleFOVRanges.FaceToFaceRange] / 2);
    }

    protected override void SetSearchLevel(){
        entity.opponentSearchState.searchLevel = Enums.SearchLevel.ItWasInFTFRange;
    }
}

