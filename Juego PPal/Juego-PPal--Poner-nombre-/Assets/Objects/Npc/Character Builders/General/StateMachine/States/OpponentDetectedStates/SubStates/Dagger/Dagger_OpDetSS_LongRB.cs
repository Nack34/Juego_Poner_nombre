using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dagger_OpponentDetectedSubState_LongBehaviour
public class Dagger_OpDetSS_LongRB : OpDetSS_LongRangeBehaviour
{
    // constructor
    public Dagger_OpDetSS_LongRB (Entity entity, FiniteStateMachine stateMachine, string animationName) : base(entity, stateMachine, animationName){
    
    } 

    
    protected override void FarBehaviour(){
        Debug.Log("Long R FarBehaviour");
        entity.SetComplexMovementDestination(entity.ClosestTarget);
        //NearBehaviour();
    }

    protected override void NearBehaviour(){
        Debug.Log("Long R NearBehaviour");

        if (canChangeMovement){
            positionToGoTo = RandomNearPositionInCircle((entity.entityData.speciesData.visionRadius[(int)Enums.PosibleFOVRanges.LongRange] - 
        entity.entityData.speciesData.visionRadius[(int)Enums.PosibleFOVRanges.ShortRange]) / 4);
            entity.SetComplexMovementDestination(positionToGoTo);
        }  
        canChangeMovement = entity.destinationSetter.ai.reachedDestination;
    }

/*
    protected override void NearBehaviour() { 
        entity.SetComplexMovementDestination(entity.ClosestTarget);

    }
*/
}
