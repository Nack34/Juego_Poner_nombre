using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dagger_OpponentDetectedSubState_FaceToFaceBehaviour
public class Dagger_OpDetSS_FTFB : OpDetSS_FaceToFaceBehaviour
{
    // constructor
    public Dagger_OpDetSS_FTFB (Entity entity, FiniteStateMachine stateMachine, string animationName) : base(entity, stateMachine, animationName){
 
    } 
    
    protected override void FarBehaviour(){
        Debug.Log("FTF R FarBehaviour");
        entity.SetComplexMovementDestination(entity.ClosestTarget);
        //NearBehaviour();
    }

    protected override void NearBehaviour(){
        Debug.Log("FTF R NearBehaviour");
        if (canChangeMovement){
            positionToGoTo = RandomNearPositionInCircle((entity.entityData.speciesData.visionRadius[(int)Enums.PosibleFOVRanges.FaceToFaceRange] / 2));
            entity.SetComplexMovementDestination(positionToGoTo);
        }  
        canChangeMovement = entity.destinationSetter.ai.reachedDestination;
    }

}

