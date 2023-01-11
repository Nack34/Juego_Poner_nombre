using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;
    protected float moveSpeed = 2.0f;
    protected bool isMoving;
    protected float tiempoConElMismoMovimiento = 999.0f;
    protected float tiempoNecesarioParaCambiarDeMovimiento;
    protected int cantCambiosDeSpeed = 0;
    protected int cambiosDeSpeedParaPasarAIdle;

    // constructor
    public MoveState (Entity entity, FiniteStateMachine stateMachine, string animationName, D_MoveState stateData) : base(entity, stateMachine, animationName)
    {
        this.stateData=stateData;
    } 

    public override void Enter () {
        base.Enter();
        isMoving = true;
        
        cantCambiosDeSpeed=0;
        
        cambiosDeSpeedParaPasarAIdle = Random.Range(2,stateData.maxCantCambiosDeSpeedParaPasarAIdle); 

        ChangeMovement();
    }

    public override void Exit () {
        base.Exit();
    }

    public override void LogicUpdate () {
        base.LogicUpdate();

        if((DistanceToBasePosition()) > (entity.entityData.baseRadius)){
            ChangeMovement();
        }else if (CanChangeMovement()){
            ChangeMovement();
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        
        isMoving = entity.TryMovingAllDirections (entity.Direction, moveSpeed) ;
    }

    // ---

    private float DistanceToBasePosition(){
        Vector2 distace;
        distace.x = Mathf.Abs(entity.aliveGOStartPosition.x - entity.aliveGO.transform.position.x);
        distace.y = Mathf.Abs(entity.aliveGOStartPosition.y - entity.aliveGO.transform.position.y);
        //Debug.Log("entity.aliveGOStartPosition.x: "+entity.aliveGOStartPosition.x+"entity.transform.position.x: "+entity.aliveGO.transform.position.x);
        //Debug.Log("entity.aliveGOStartPosition.y: "+entity.aliveGOStartPosition.y+"entity.transform.position.y: "+entity.aliveGO.transform.position.y);

        //Debug.Log("distace.x: "+distace.x+"distace.y: "+distace.y);
        float linearDistance = Mathf.Sqrt(Mathf.Pow(distace.x,2)+Mathf.Pow(distace.y,2)); 
        //Debug.Log("linearDistance: "+linearDistance+", entity.entityData.baseRadius: "+entity.entityData.baseRadius);
        return linearDistance;
        
    }


    // ---

    private bool CanChangeMovement(){
        tiempoConElMismoMovimiento+=Time.deltaTime;
        return tiempoConElMismoMovimiento>=tiempoNecesarioParaCambiarDeMovimiento;
    }

    private void ChangeMovement(){
        tiempoConElMismoMovimiento=0.0f;
        tiempoNecesarioParaCambiarDeMovimiento = 5.0f;
        tiempoNecesarioParaCambiarDeMovimiento = Random.Range(stateData.minTiempoNecesarioParaCambiarDeMovimiento,stateData.maxTiempoNecesarioParaCambiarDeMovimiento); 

        entity.Direction = RandomOriginDireccion(); 
        moveSpeed = MoveSpeed();
    }

    // ---
    
    private float MoveSpeed(){
        cantCambiosDeSpeed++;
        /*if (cantCambiosDeSpeed >= cambiosDeSpeedParaPasarAIdle) {
            return 0;    
        } 
        else {*/
            return Random.Range(stateData.minMovementSpeed,stateData.maxMovementSpeed);   
        //}
    }

    // ---

    private Vector2 RandomOriginDireccion(){  
        Vector2 newPosition = RandomPosition(-entity.entityData.baseRadius,entity.entityData.baseRadius); // elije una posicion (un punto) de la zona de inicio
        return CalculateDirection(newPosition); // calcula la direccion necesaria para ir a ese punto
    }


    // HACER: a esto le falta tener en cuenta la posicion de origen (posicion de inicio, en donde se instancio)
    private Vector2 RandomPosition (float LeftAndDownRangeLimit, float RightAndUpRangeLimit){
        return RandomVector2 (LeftAndDownRangeLimit, RightAndUpRangeLimit, LeftAndDownRangeLimit, RightAndUpRangeLimit) + entity.aliveGOStartPosition;
    }

    private Vector2 RandomVector2(float minX, float maxX, float minY, float maxY){
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        return new Vector2 (x,y);
    }
    
    private Vector2 CalculateDirection (Vector2 positionToGoTo){
        //Debug.Log("positionToGoTo: "+positionToGoTo+", entity.transform.position: "+ entity.aliveGO.transform.position);
        Vector2 directionToGoTo = new Vector2((positionToGoTo.x - entity.aliveGO.transform.position.x), (positionToGoTo.y - entity.aliveGO.transform.position.y));
        directionToGoTo.Normalize(); // When normalized, a vector keeps the same direction but its length is 1.0
        return directionToGoTo;
    }

    // ---

}
