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

        Debug.Log("direccion: "+entity.Direction+", moveSpeed:"+moveSpeed);

        if (CanChangeMovement()){
            ChangeMovement();
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        
        isMoving = entity.TryMovingAllDirections (entity.Direction, moveSpeed) ;
    }

    // ---

    private bool CanChangeMovement(){
        tiempoConElMismoMovimiento+=Time.deltaTime;
        return tiempoConElMismoMovimiento>=tiempoNecesarioParaCambiarDeMovimiento;
    }

    private void ChangeMovement(){
        tiempoConElMismoMovimiento=0.0f;
        tiempoNecesarioParaCambiarDeMovimiento = 5.0f;
        //tiempoNecesarioParaCambiarDeMovimiento = Random.Range(1.5f,stateData.maxTiempoNecesarioParaCambiarDeMovimiento); 

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
            return Random.Range(0,stateData.maxMovementSpeed);   
        //}
    }

    // ---

    private Vector2 RandomOriginDireccion(){  
        Vector2 newPosition = RandomPosition(-stateData.baseRadius,stateData.baseRadius); // elije una posicion (un punto) de la zona de inicio
        return CalculateDirection(newPosition); // calcula la direccion necesaria para ir a ese punto
    }


    // HACER: a esto le falta tener en cuenta la posicion de origen (posicion de inicio, en donde se instancio)
    private Vector2 RandomPosition (float LeftAndDownRangeLimit, float RightAndUpRangeLimit){
        return RandomVector2 (LeftAndDownRangeLimit, RightAndUpRangeLimit, LeftAndDownRangeLimit, RightAndUpRangeLimit) + entity.startPosition;
    }

    private Vector2 RandomVector2(float minX, float maxX, float minY, float maxY){
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        return new Vector2 (x,y);
    }
    
    private Vector2 CalculateDirection (Vector2 positionToGoTo){
        Vector2 directionToGoTo = new Vector2((int)(positionToGoTo.x - entity.transform.position.x), (int)(positionToGoTo.y - entity.transform.position.y));
        directionToGoTo.Normalize(); // When normalized, a vector keeps the same direction but its length is 1.0
        return directionToGoTo;
    }

    // ---

}
