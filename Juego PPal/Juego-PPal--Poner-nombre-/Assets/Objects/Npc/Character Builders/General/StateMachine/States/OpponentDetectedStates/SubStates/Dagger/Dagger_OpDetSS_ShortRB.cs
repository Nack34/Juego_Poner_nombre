using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dagger_OpponentDetectedSubState_ShortRangeBehaviour
public class Dagger_OpDetSS_ShortRB : OpDetSS_ShortRangeBehaviour
{
    // constructor
    public Dagger_OpDetSS_ShortRB (Entity entity, FiniteStateMachine stateMachine, string animationName) : base(entity, stateMachine, animationName){
 
    } 

    
    protected override void FarBehaviour(){
        Debug.Log("Short R FarBehaviour");
        entity.SetComplexMovementDestination(entity.ClosestTarget);
        //NearBehaviour();
    }

    protected override void NearBehaviour(){
        Debug.Log("Short R NearBehaviour");
        if (canChangeMovement){
            positionToGoTo = RandomNearPositionInCircle((entity.entityData.speciesData.visionRadius[(int)Enums.PosibleFOVRanges.ShortRange] - 
        entity.entityData.speciesData.visionRadius[(int)Enums.PosibleFOVRanges.FaceToFaceRange]) / 3);
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
