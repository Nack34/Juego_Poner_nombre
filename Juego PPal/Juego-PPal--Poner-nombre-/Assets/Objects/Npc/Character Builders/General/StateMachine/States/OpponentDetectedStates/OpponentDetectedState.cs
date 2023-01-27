using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Usa moverse sin chocarse que tiene que estar en entity
// renombrar a OpponentDetectedState. IMPLEMENTAR
public class OpponentDetectedState : State
{
    protected OpDetSS_FaceToFaceBehaviour faceToFaceBehaviour;
    protected OpDetSS_ShortRangeBehaviour shortRangeBehaviour;
    protected OpDetSS_LongRangeBehaviour longRangeBehaviour;
    protected Enums.PosibleFOVRanges rangeOnWhichTargetWasLastSeen;
    protected bool HasTarget{
        get{
            return entity.HasTarget;
        }
    }

    public float lastAttackTime;
    public bool isInAttackRange;
    protected bool IsAbleToAttack{
        get{
            return (Time.time > lastAttackTime + 0.5f) && isInAttackRange;
        }
    }

    // constructor
    public OpponentDetectedState (Entity entity, FiniteStateMachine stateMachine, string animationName) : base(entity, stateMachine, animationName)
    {
    } 

    private bool InitializedState(){
        return faceToFaceBehaviour != null || shortRangeBehaviour != null || longRangeBehaviour != null ;
    }

    public void Initialize ( OpDetSS_FaceToFaceBehaviour faceToFaceBehaviour, 
                        OpDetSS_ShortRangeBehaviour shortRangeBehaviour,
                        OpDetSS_LongRangeBehaviour longRangeBehaviour ){
        // Debug.Log("INICIALIZACION DE COMBATE STATE");
        this.faceToFaceBehaviour = faceToFaceBehaviour ;
        this.shortRangeBehaviour = shortRangeBehaviour ;
        this.longRangeBehaviour = longRangeBehaviour ;
        }

    public override void CheckStateTransitions () { // aca van todas las tranciciones e/ estados (depende del estado)
        if (IsAbleToAttack){
            //TRANCICION A AttackState. IMPLEMENTAR

            // entity.currentHability = randomActionSelector.SelectActionToUse();
            //Attack("MagicAnimation", 0); // los datos enviados son provisionales, despues cambiar por entity.currentWeapon.name y entity.currentHability.ID, IMPLEMENTAR
        
        } else if (!HasTarget){
            entity.destinationSetter.target = entity.ClosestTarget = null; 
            stateMachine.ChangeState(entity.opponentSearchState);
        }
    }
    
    public override void Enter () {
        base.Enter();

        if (!InitializedState())
            Debug.LogError("En la entidad: "+ entity.entityData.entityName+ ", al menos un Combat Sub State no se cargo correctamente");
        
        isInAttackRange = false;
        entity.CurrentSpeed = entity.destinationSetter.ai.maxSpeed = entity.entityData.speciesData.maxMovementSpeed;

        if (HasTarget){ // Se ejecuta solamente una de las 3 una vez. La idea seria que la animacion ...
                        //... de alerta de deteccion de oponente dure mas mientras MAS lejos este el oponente 
            if (entity.HasTargetInFaceToFaceRange){
                rangeOnWhichTargetWasLastSeen = Enums.PosibleFOVRanges.FaceToFaceRange;
                faceToFaceBehaviour.Enter();
            } else 
            if (entity.HasTargetInShortRange){
                rangeOnWhichTargetWasLastSeen = Enums.PosibleFOVRanges.ShortRange;
                shortRangeBehaviour.Enter();
            } else 
            if (entity.HasTargetInLongRange){
                rangeOnWhichTargetWasLastSeen = Enums.PosibleFOVRanges.LongRange;
                longRangeBehaviour.Enter();
            } else Debug.LogError("En la entidad "+entity.entityData.entityName+", se seteo como si hubiera target, pero no hay target en ninguna de las distancias");
        } 
    }



    public override void Exit () {// Se ejecuta solamente una de las 3 una vez. La idea seria que al pasar a LookForOpponentState, mientras...
                                //... mas lejos este el oponente lo busque por mas tiempo (y mas desesperadamente? como podria hacerse? ) 
        base.Exit();

        switch (rangeOnWhichTargetWasLastSeen)
        {
            case Enums.PosibleFOVRanges.FaceToFaceRange :
                faceToFaceBehaviour.Exit();
                break;
            case Enums.PosibleFOVRanges.ShortRange :
                shortRangeBehaviour.Exit();
                break;
            case Enums.PosibleFOVRanges.LongRange :
                longRangeBehaviour.Exit();
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
                //Debug.Log("opponentDetectedState: Hace el update de ftf");
                rangeOnWhichTargetWasLastSeen = Enums.PosibleFOVRanges.FaceToFaceRange;
                faceToFaceBehaviour.LogicUpdate();
            } else 
            if (entity.HasTargetInShortRange){
                rangeOnWhichTargetWasLastSeen = Enums.PosibleFOVRanges.ShortRange;
                shortRangeBehaviour.LogicUpdate();
            } else 
            if (entity.HasTargetInLongRange){
                rangeOnWhichTargetWasLastSeen = Enums.PosibleFOVRanges.LongRange;
                longRangeBehaviour.LogicUpdate();
            } else Debug.LogError("En la entidad "+entity.entityData.entityName+", se seteo como si hubiera target, pero no hay target en ninguna de las distancias");
        } 

    }


    public override void PhysicsUpdate() { // intenta moverse con la direccion seleccionada
        base.PhysicsUpdate();
        
        if (HasTarget){
            if (entity.HasTargetInFaceToFaceRange){
                faceToFaceBehaviour.PhysicsUpdate();
            } else 
            if (entity.HasTargetInShortRange){
                shortRangeBehaviour.PhysicsUpdate();
            } else 
            if (entity.HasTargetInLongRange){
                longRangeBehaviour.PhysicsUpdate();
            } else Debug.LogError("En la entidad "+entity.entityData.entityName+", se seteo como si hubiera target, pero no hay target en ninguna de las distancias");
        }
    }


}
