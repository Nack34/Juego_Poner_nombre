using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// este estado hace que la entidad se mantenga en movimiento random dentro de una zona determinada
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

    public override void LogicUpdate () { // mantiene a la entidad en la zona de inicio y con un movimiento dinamico
        base.LogicUpdate();

        if((DistanceToBasePosition()) > (entity.entityData.speciesData.baseRadius)){
            ChangeMovement();
        }else if (CanChangeMovement()){
            ChangeMovement();
        }
    }

    public override void PhysicsUpdate() { // intenta moverse con la direccion seleccionada
        base.PhysicsUpdate();
        
        isMoving = TryMovingAllDirections (entity.Direction, moveSpeed) ;
    }

    // ---

    private float DistanceToBasePosition(){ // devuelve la distancia lineal a el punto en donde se instancio
        Vector2 distace;
        distace.x = Mathf.Abs(entity.NPCStartPosition.x - entity.NPC.transform.position.x);
        distace.y = Mathf.Abs(entity.NPCStartPosition.y - entity.NPC.transform.position.y);
        
        float linearDistance = Mathf.Sqrt(Mathf.Pow(distace.x,2)+Mathf.Pow(distace.y,2)); 
        return linearDistance;
    }


    // ---

    private bool CanChangeMovement(){ // cada cierto tiempo sin cambiar el movimiento hay que cambiarlo
        tiempoConElMismoMovimiento+=Time.deltaTime;
        return tiempoConElMismoMovimiento>=tiempoNecesarioParaCambiarDeMovimiento;
    }

    private void ChangeMovement(){ // selecciona otro movimiento random dentro de la zona de inicio
        tiempoConElMismoMovimiento=0.0f;
        tiempoNecesarioParaCambiarDeMovimiento = 5.0f;
        tiempoNecesarioParaCambiarDeMovimiento = Random.Range(stateData.minTiempoNecesarioParaCambiarDeMovimiento,stateData.maxTiempoNecesarioParaCambiarDeMovimiento); 

        entity.Direction = RandomOriginDireccion(); 
        moveSpeed = MoveSpeed();
    }

    // ---
    
    private float MoveSpeed(){ // devuelve una velocidad random. Si ya paso bastante sin ir a IdleState, fuerza el cambio
        cantCambiosDeSpeed++;
        /*if (cantCambiosDeSpeed >= cambiosDeSpeedParaPasarAIdle) {
            return 0;    
        } 
        else {*/
            return Random.Range(entity.entityData.speciesData.minMovementSpeed,entity.entityData.speciesData.maxMovementSpeed);
        //}
    }

    // ---

    private Vector2 RandomOriginDireccion(){ // devuelve una la direccion necesaria para ir a una posicion random de la zona de inicio
        Vector2 newPosition = RandomPosition(-entity.entityData.speciesData.baseRadius,entity.entityData.speciesData.baseRadius); 
        return CalculateDirection(newPosition); 
    }

    private Vector2 RandomPosition (float LeftAndDownRangeLimit, float RightAndUpRangeLimit){ // elije una posicion (un punto) random de la zona de inicio
        return RandomVector2 (LeftAndDownRangeLimit, RightAndUpRangeLimit, LeftAndDownRangeLimit, RightAndUpRangeLimit) + entity.NPCStartPosition;
    }

    private Vector2 RandomVector2(float minX, float maxX, float minY, float maxY){
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        return new Vector2 (x,y);
    }
    
    private Vector2 CalculateDirection (Vector2 positionToGoTo){ // calcula la direccion necesaria para ir a ese punto
        Vector2 directionToGoTo = new Vector2((positionToGoTo.x - entity.NPC.transform.position.x), (positionToGoTo.y - entity.NPC.transform.position.y));
        directionToGoTo.Normalize(); // When normalized, a vector keeps the same direction but its length is 1.0
        return directionToGoTo;
    }


    // --- Lo siguiente no se si dejarlo aca o pasarlo a entity. Si algun otro State utiliza tryMove, CAMBIAR A ENTITY

    
    public bool TryMovingAllDirections(Vector2 direction, float moveSpeed) { // intenta moverse hacia la direccion indicada tratando de evadir obstaculos
        Debug.Log("moveSpeed: "+moveSpeed+", direccion: "+direction);
        // If movement input is not 0, try to move
        if(moveSpeed>0){
            bool success = TryMove(direction, moveSpeed); //me muevo en diagonal

            if(!success) { //si no puedo moverme en diagonal, lo intento en un solo eje
                success = TryMove(new Vector2(direction.x, 0), moveSpeed);
            }

            if(!success) {
                success = TryMove(new Vector2(0, direction.y), moveSpeed);
            }
            return success;
        } else return false;
    }

    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); // list of collisions

    private bool TryMove (Vector2 direction, float moveSpeed) { // chequea por colisiones e intenta moverse con la direccion y velocidad indicada
        if(IsMoving(direction,moveSpeed)) {
            // Check for potential collisions
            int count = entity.movementCollider.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                entity.entityData.npcData.movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                (moveSpeed) * Time.fixedDeltaTime + entity.entityData.npcData.collisionOffset, // The amount to cast equal to the movement plus an offset
                true); // ignoreSiblingColliders = true: Determines whether the cast should ignore Colliders attached to the same Rigidbody2D (known as sibling Colliders))

            if(count == 0){
                entity.rb.MovePosition(entity.rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }
        } else {
            // Can't move if there's no direction to move in
            return false;
        }
    }

    private bool IsMoving(Vector2 direction, float moveSpeed){ // se fija si la velocidad lineal actual es mayor o igual a la minima
        return (direction != Vector2.zero) && (CurrentSpeed(direction,moveSpeed) >= stateData.velocidadMinimaDeMovimiento);
    }
    
    private float CurrentSpeed(Vector2 direction, float moveSpeed){ // Calcula la velocidad lineal actual real
        Vector2 speedOnAxes = direction * moveSpeed;
        float linearSpeed = Mathf.Sqrt(Mathf.Pow(speedOnAxes.x,2)+Mathf.Pow(speedOnAxes.y,2));  
        Debug.Log("trying Direction: "+direction.x+", "+direction.y);
        Debug.Log("trying Speed: "+linearSpeed);
        return linearSpeed;
    }

}
