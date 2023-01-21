using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Usa moverse sin chocarse que tiene que estar en entity
public class CombatState : State
{
    protected FaceToFaceRange_CombatSubState faceToFaceRange_CombatSubState;
    protected ShortRange_CombatSubState shortRange_CombatSubState;
    protected LongRange_CombatSubState longRange_CombatSubState;
    private Enums.PosibleFOVRanges rangeOnWhichTargetWasLastSeen;
    protected bool HasTarget{
        get{
            return entity.HasTarget;
        }
    }

    // constructor
    public CombatState (Entity entity, FiniteStateMachine stateMachine, string animationName) : base(entity, stateMachine, animationName)
    {
    } 

    private bool InitializedState(){
        return faceToFaceRange_CombatSubState != null || shortRange_CombatSubState != null || longRange_CombatSubState != null ;
    }

    public void Initialize ( FaceToFaceRange_CombatSubState faceToFaceRange_CombatSubState, 
                        ShortRange_CombatSubState shortRange_CombatSubState,
                        LongRange_CombatSubState longRange_CombatSubState ){
        // Debug.Log("INICIALIZACION DE COMBATE STATE");
        this.faceToFaceRange_CombatSubState = faceToFaceRange_CombatSubState ;
        this.shortRange_CombatSubState = shortRange_CombatSubState ;
        this.longRange_CombatSubState = longRange_CombatSubState ;
    }

    public override void CheckStateTransitions () { // aca van todas las tranciciones e/ estados (depende del estado)
        if (!HasTarget){
            stateMachine.ChangeState(entity./*moveState);*/opponentSearchState);
        }
    }
    
    public override void Enter () {
        base.Enter();

        if (!InitializedState())
            Debug.LogError("En la entidad: "+ entity.entityData.entityName+ ", al menos un Combat Sub State no se cargo correctamente");
        
        
        entity.CurrentSpeed = entity.destinationSetter.ai.maxSpeed = entity.entityData.speciesData.maxMovementSpeed;

        if (HasTarget){ // Se ejecuta solamente una de las 3 una vez. La idea seria que la animacion ...
                        //... de alerta de deteccion de oponente dure mas mientras MAS lejos este el oponente 
            if (entity.HasTargetInFaceToFaceRange){
                rangeOnWhichTargetWasLastSeen = Enums.PosibleFOVRanges.FaceToFaceRange;
                faceToFaceRange_CombatSubState.Enter();
            } else 
            if (entity.HasTargetInShortRange){
                rangeOnWhichTargetWasLastSeen = Enums.PosibleFOVRanges.ShortRange;
                shortRange_CombatSubState.Enter();
            } else 
            if (entity.HasTargetInLongRange){
                rangeOnWhichTargetWasLastSeen = Enums.PosibleFOVRanges.LongRange;
                longRange_CombatSubState.Enter();
            } else Debug.LogError("En la entidad "+entity.entityData.entityName+", se seteo como si hubiera target, pero no hay target en ninguna de las distancias");
        } 
    }



    public override void Exit () {// Se ejecuta solamente una de las 3 una vez. La idea seria que al pasar a LookForOpponentState, mientras...
                                //... mas lejos este el oponente lo busque por mas tiempo (y mas desesperadamente? como podria hacerse? ) 
        base.Exit();

        switch (rangeOnWhichTargetWasLastSeen)
        {
            case Enums.PosibleFOVRanges.FaceToFaceRange :
                faceToFaceRange_CombatSubState.Exit();
                break;
            case Enums.PosibleFOVRanges.ShortRange :
                shortRange_CombatSubState.Exit();
                break;
            case Enums.PosibleFOVRanges.LongRange :
                longRange_CombatSubState.Exit();
                break;
            default: 
                Debug.LogError("En la entidad "+entity.entityData.entityName+", se seteo como si hubiera target, pero no hay target en ninguna de las distancias");
                break;
        }
    }
    
    public override void LogicUpdate () { // Cambio dinamico y directio, pasa directamente de un Update a otro
        base.LogicUpdate();

        if (HasTarget){
            if (entity.HasTargetInFaceToFaceRange){
                rangeOnWhichTargetWasLastSeen = Enums.PosibleFOVRanges.FaceToFaceRange;
                faceToFaceRange_CombatSubState.LogicUpdate();
            } else 
            if (entity.HasTargetInShortRange){
                rangeOnWhichTargetWasLastSeen = Enums.PosibleFOVRanges.ShortRange;
                shortRange_CombatSubState.LogicUpdate();
            } else 
            if (entity.HasTargetInLongRange){
                rangeOnWhichTargetWasLastSeen = Enums.PosibleFOVRanges.LongRange;
                longRange_CombatSubState.LogicUpdate();
            } else Debug.LogError("En la entidad "+entity.entityData.entityName+", se seteo como si hubiera target, pero no hay target en ninguna de las distancias");
        } 

    }


    public override void PhysicsUpdate() { // intenta moverse con la direccion seleccionada
        base.PhysicsUpdate();
        
        if (HasTarget){
            if (entity.HasTargetInFaceToFaceRange){
                faceToFaceRange_CombatSubState.PhysicsUpdate();
            } else 
            if (entity.HasTargetInShortRange){
                shortRange_CombatSubState.PhysicsUpdate();
            } else 
            if (entity.HasTargetInLongRange){
                longRange_CombatSubState.PhysicsUpdate();
            } else Debug.LogError("En la entidad "+entity.entityData.entityName+", se seteo como si hubiera target, pero no hay target en ninguna de las distancias");
        }
    }

    
    public override void AnimationEnding(){ // es llamada por el script nexo animacion-estado. FALTA IMPLEMENTAR
        base.AnimationEnding();
    }


}
