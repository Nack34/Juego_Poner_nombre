using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSearchState : State
{
    public Enums.SearchLevel searchLevel;
    protected float maxTiempoDeBusqueda;
    protected float tiempoDeBusqueda;
    protected Vector3 positionToGoTo; 


    // constructor
    public OpponentSearchState (Entity entity, FiniteStateMachine stateMachine, string animationName, float maxTiempoDeBusqueda) : base(entity, stateMachine, animationName)
    {
        this.maxTiempoDeBusqueda = maxTiempoDeBusqueda;
    } 

    public override void CheckStateTransitions () { // aca van todas las tranciciones e/ estados (depende del estado)
        base.CheckStateTransitions();

        if (Time.time >= startTime + tiempoDeBusqueda){
            stateMachine.ChangeState(entity.moveState);
        }

    }

    public override void Enter () {
        base.Enter();
    
        tiempoDeBusqueda = SetTiempoDeBusqueda(searchLevel); 

        /*
         esto es lo de MoveState, que sea parecido pero mas rapido y evitando obstaculos
        desplazamiento = desplazamiento * 0; // + random (-0.1 o 0.1) grados  e/ -10 y 10 (La idea es q sea un...
                                        //... movimiento dinamico de la cabeza, a la izquierda o derecha). (IMPLEMENTAR en entity)
        entity.LookingDirection = entity.LookingDirection + desplazamiento ; 

        canChangeMovement = CanChangeMovement();
        if((DistanceToBasePosition()) > (entity.entityData.speciesData.baseRadius) && canChangeMovement){
            ChangeMovement();
        }else if (canChangeMovement){
            ChangeMovement();
        }
        */
        entity.destinationSetter.positionToGoTo = positionToGoTo; /*= SetRandomPosition(entity.closestTargetLastSeenPosition, 
                                                    entity.entityData.speciesData.seachRadius);*/
        
        //currentDirection = Vector2.Direction(positionToGoTo,entity.position)

        // entity.lookingDirection = currentDirection // + random (-0.1 o 0.1) grados  e/ -10 y 10 (La idea es q sea un...
                                        //... movimiento dinamico de la cabeza, a la izquierda o derecha). (IMPLEMENTAR en entity) 
        

        if (!entity.destinationSetter.ai.canMove){
            entity.destinationSetter.ai.canMove = true;
        }
        
    }

    public override void Exit () {
        base.Exit();

        
    }

    public override void LogicUpdate () { 
        base.LogicUpdate();

        
    }

    public override void PhysicsUpdate() { 
        base.PhysicsUpdate();

        
    }

    public override void AnimationEnding(){ // es llamada por el script nexo animacion-estado. FALTA IMPLEMENTAR
        base.AnimationEnding();
        

    }

    private float SetTiempoDeBusqueda(Enums.SearchLevel searchLevel){
        switch (searchLevel)
        {
            case Enums.SearchLevel.ItWasInNoRange:
            case Enums.SearchLevel.ItWasInFTFRange:
                tiempoDeBusqueda = maxTiempoDeBusqueda;
                break;
            case Enums.SearchLevel.ItWasInShortRange:
                tiempoDeBusqueda = maxTiempoDeBusqueda * 2/3;
                break;
            case Enums.SearchLevel.ItWasInLongRange:
                tiempoDeBusqueda = maxTiempoDeBusqueda * 1/2;
                break;
            default: Debug.LogError("En la entidad:" + entity.entityData.entityName+", en el OpponentSearchSate se seteo de manera incorrecta el search level");
                break;
        }
        return tiempoDeBusqueda;
    }

    /*private Vector3 SetRandomPosition (){

    }*/
}
