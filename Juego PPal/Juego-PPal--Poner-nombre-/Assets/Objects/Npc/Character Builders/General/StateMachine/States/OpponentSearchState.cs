using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSearchState : State
{
    public Enums.SearchLevel searchLevel;
    protected float maxTiempoDeBusqueda;
    protected float tiempoDeBusqueda;
    protected Vector3 positionToGoTo; 
    
    protected float lastLookingDirectionChangeTime;
    protected float tiempoEntreLookingDirectionChanges = 0.2f;
    protected float maxGradosDeMovimientoDeMira = 20.0f;

    protected float SearchRadius{
        get{
            return entity.currentSeachRadius;
        }
        set {
            entity.currentSeachRadius = value;
        }
    }

    protected float tiempoNecesarioParaCambiarDeMovimiento;
    protected float tiempoConElMismoMovimiento;
    protected bool canChangeMovement;

    protected float moveSpeed;


    // constructor
    public OpponentSearchState (Entity entity, FiniteStateMachine stateMachine, string animationName, float maxTiempoDeBusqueda) : base(entity, stateMachine, animationName)
    {
        this.maxTiempoDeBusqueda = maxTiempoDeBusqueda;
    } 

    public override void CheckStateTransitions () { // aca van todas las tranciciones e/ estados (depende del estado)
        base.CheckStateTransitions();

        if(entity.HasTarget){
            stateMachine.ChangeState(entity.combatState);
        } else if (Time.time >= startTime + tiempoDeBusqueda){
            stateMachine.ChangeState(entity.moveState);
        }

    }

    public override void Enter () {
        base.Enter();
    
        SetParametrosDeBusqueda(searchLevel); 
        
        lastLookingDirectionChangeTime = Time.time;

        ChangeMovement();

        entity.StartComplexMovement();
    }

    public override void Exit () {
        base.Exit();

        
    }

    public override void LogicUpdate () { 
        base.LogicUpdate();

        lastLookingDirectionChangeTime = entity.ChangeLookingDirection(lastLookingDirectionChangeTime, tiempoEntreLookingDirectionChanges, maxGradosDeMovimientoDeMira);

        canChangeMovement = CanChangeMovement();
        if((entity.LinearDistanceTo(entity.closestTargetLastSeenPosition)) > (SearchRadius) && canChangeMovement){
            ChangeMovement();
        }else if (canChangeMovement){
            ChangeMovement();
        }
    }

    public override void PhysicsUpdate() { 
        base.PhysicsUpdate();

        
    }

    public override void AnimationEnding(){ // es llamada por el script nexo animacion-estado. FALTA IMPLEMENTAR
        base.AnimationEnding();
        

    }

    // --- Inicializacion

    private void SetParametrosDeBusqueda(Enums.SearchLevel searchLevel){

        /*
        Debug.Log("OpponentSearchState dice:"+
        " maxTiempoDeBusqueda = "+maxTiempoDeBusqueda+
        ", entity.entityData.speciesData.maxSearchRadius = "+entity.entityData.speciesData.maxSearchRadius +
        ", entity.entityData.speciesData.maxMovementSpeed  = "+entity.entityData.speciesData.maxMovementSpeed);
        */
        
        switch (searchLevel)
        {
            case Enums.SearchLevel.ItWasInNoRange:
                tiempoDeBusqueda = maxTiempoDeBusqueda;

                tiempoEntreLookingDirectionChanges = 0.05f;
                maxGradosDeMovimientoDeMira = 30.0f;

                SearchRadius = entity.entityData.speciesData.maxSearchRadius;
                
                moveSpeed = entity.entityData.speciesData.maxMovementSpeed * 2/3;

                break;
            case Enums.SearchLevel.ItWasInFTFRange:
                tiempoDeBusqueda = maxTiempoDeBusqueda;

                tiempoEntreLookingDirectionChanges = 0.05f;
                maxGradosDeMovimientoDeMira = 30.0f;

                SearchRadius = entity.entityData.speciesData.maxSearchRadius * 1/3;

                moveSpeed = entity.entityData.speciesData.maxMovementSpeed;

                break;
            case Enums.SearchLevel.ItWasInShortRange:
                tiempoDeBusqueda = maxTiempoDeBusqueda * 2/3;
                
                tiempoEntreLookingDirectionChanges = 0.1f;
                maxGradosDeMovimientoDeMira =25.0f;

                SearchRadius = entity.entityData.speciesData.maxSearchRadius * 2/3;

                moveSpeed = entity.entityData.speciesData.maxMovementSpeed * 5/6;

                break;
            case Enums.SearchLevel.ItWasInLongRange:
                tiempoDeBusqueda = maxTiempoDeBusqueda * 1/2;

                tiempoEntreLookingDirectionChanges = 0.2f;
                maxGradosDeMovimientoDeMira = 20.0f;

                SearchRadius = entity.entityData.speciesData.maxSearchRadius;
                
                moveSpeed = entity.entityData.speciesData.maxMovementSpeed * 4/6;

                break;
            default: Debug.LogError("En la entidad:" + entity.entityData.entityName+", en el OpponentSearchSate se seteo de manera incorrecta el search level");
                break;
        }

        tiempoNecesarioParaCambiarDeMovimiento = tiempoDeBusqueda * 1/5;
        entity.CurrentSpeed = entity.destinationSetter.ai.maxSpeed = moveSpeed;

        
        /*
        Debug.Log(searchLevel+":"+
        " tiempoDeBusqueda = "+tiempoDeBusqueda+
        ", tiempoEntreLookingDirectionChanges = "+tiempoEntreLookingDirectionChanges+
        ", maxGradosDeMovimientoDeMira = "+maxGradosDeMovimientoDeMira+
        ", SearchRadius = "+SearchRadius+
        ", tiempoNecesarioParaCambiarDeMovimiento = "+tiempoNecesarioParaCambiarDeMovimiento+
        ", moveSpeed = "+moveSpeed+
        ", entity.destinationSetter.ai.maxSpeed = "+entity.destinationSetter.ai.maxSpeed);
        */
    }


    // --- Metodos para el movimiento (similar a MoveState, pero no igual)


    private bool CanChangeMovement(){ // cada cierto tiempo sin cambiar el movimiento hay que cambiarlo
        tiempoConElMismoMovimiento+=Time.deltaTime;
        return tiempoConElMismoMovimiento>=tiempoNecesarioParaCambiarDeMovimiento;
    }
    
    private void ChangeMovement(){ // selecciona otro movimiento random dentro de la zona 
        tiempoConElMismoMovimiento=0.0f;

        entity.LookingDirection = RandomSearchZoneDireccion(); // actualizo direcciones 
        Debug.Log("entity.LookingDirection = "+entity.LookingDirection);
        lastLookingDirectionChangeTime = Time.time;
    }
    
    private Vector2 RandomSearchZoneDireccion(){ // setea la posicion a la que queremos ir y devuelve la direccion a la cual tiene que mirar 
        Vector2 newPosition = entity.destinationSetter.positionToGoTo = entity.RandomPosition(-SearchRadius,SearchRadius) + entity.closestTargetLastSeenPosition; 
        return entity.CalculateDirection(newPosition); 
    }

}
