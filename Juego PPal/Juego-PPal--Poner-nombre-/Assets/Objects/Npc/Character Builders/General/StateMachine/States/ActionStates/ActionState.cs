using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : State
{
    
    // protected D_ActionState stateData; PARA QUE ME SERVIRIA?
     protected D_MoveState stateData; // falso para que no salgan errores BORRAR
/*
    protected FaceToFaceRangeActionState faceToFaceRangeActionState;
    protected ShortRangeActionState shortRangeActionState;
    protected LongRangeActionState longRangeActionState;
    protected bool hasTarget;
*/
    // constructor
    public ActionState (Entity entity, FiniteStateMachine stateMachine, string animationName, /*D_ActionState*/ D_MoveState stateData /*, 
                        FaceToFaceRangeActionState faceToFaceRangeActionState, 
                        ShortRangeActionState shortRangeActionState,
                        LongRangeActionState longRangeActionState
                        */
                        ) : base(entity, stateMachine, animationName)
    {
        this.stateData=stateData;
    } 

    /*
    public override void Enter () {
        base.Enter();

        hasTarget = entity.hasTarget; 

        if (entity.hasTarget){
            if (entity.HasTargetInFaceToFaceRange){
                faceToFaceRangeActionState.Enter();
            } else 
            if (entity.HasTargetInShortRange){
                shortRangeActionState.Enter();
            } else 
            if (entity.HasTargetInLongRange){
                longRangeActionState.Enter();
            } else Debug.LogError("En la entidad "+entity.entityData.entityName+", se seteo como si hubiera target, pero no hay target en ninguna de las distancias");
        } else{
            hasTarget = false;
        }
    }

    
    public override void LogicUpdate () { 
        base.LogicUpdate();

        if (entity.hasTarget){
            if (entity.HasTargetInFaceToFaceRange){
                faceToFaceRangeActionState.LogicUpdate();
            } else 
            if (entity.HasTargetInShortRange){
                shortRangeActionState.LogicUpdate();
            } else 
            if (entity.HasTargetInLongRange){
                longRangeActionState.LogicUpdate();
            } else Debug.LogError("En la entidad "+entity.entityData.entityName+", se seteo como si hubiera target, pero no hay target en ninguna de las distancias");
        } else{
            hasTarget = false;
        }

    }

    
    public override void Exit () {
        base.Exit();

        if (entity.hasTarget){
            if (entity.HasTargetInFaceToFaceRange){
                faceToFaceRangeActionState.Exit();
            } else 
            if (entity.HasTargetInShortRange){
                shortRangeActionState.Exit();
            } else 
            if (entity.HasTargetInLongRange){
                longRangeActionState.Exit();
            } else Debug.LogError("En la entidad "+entity.entityData.entityName+", se seteo como si hubiera target, pero no hay target en ninguna de las distancias");
        }
    }



    public override void PhysicsUpdate() { // intenta moverse con la direccion seleccionada
        base.PhysicsUpdate();
        
        if (entity.hasTarget){
            if (entity.HasTargetInFaceToFaceRange){
                faceToFaceRangeActionState.PhysicsUpdate();
            } else 
            if (entity.HasTargetInShortRange){
                shortRangeActionState.PhysicsUpdate();
            } else 
            if (entity.HasTargetInLongRange){
                longRangeActionState.PhysicsUpdate();
            } else Debug.LogError("En la entidad "+entity.entityData.entityName+", se seteo como si hubiera target, pero no hay target en ninguna de las distancias");
        }
    }


    // Usa moverse sin chocarse que tiene que estar en entity

*/
}
