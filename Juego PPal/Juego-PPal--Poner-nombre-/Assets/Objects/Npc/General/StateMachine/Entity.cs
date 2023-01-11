using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public D_Entity entityData;
    public FiniteStateMachine stateMachine;

    public Rigidbody2D rb {get; private set;}
    public Animator animator {get; private set;}
    public GameObject aliveGO {get; private set;}
    public Vector2 aliveGOStartPosition {get; private set;}
    
    [SerializeField]
    private Vector2 direction= new Vector2 (0,0);
    public Vector2 Direction {
        get{
            return direction;
        }
        set{
            direction=value;
            animator.SetFloat(AnimationStrings.Xdirection,direction.x);
            animator.SetFloat(AnimationStrings.Ydirection,direction.y);
        }
    }


    private Vector2 currentVelocity;

    [SerializeField]
    private Collider2D movementCollider; 
    [SerializeField]
    private Collider2D DetectionZone; // no es un collider CAMBIAR
    [SerializeField]
    private Stats stats ;

    public virtual void Start(){
        aliveGO = transform.Find("Alive").gameObject;
        aliveGOStartPosition = aliveGO.transform.position;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        animator = aliveGO.GetComponent<Animator>();
        stateMachine = new FiniteStateMachine(); 
    }


    public virtual void Update() {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate(){
        stateMachine.currentState.PhysicsUpdate();
    }

    

    public bool TryMovingAllDirections(Vector2 direction, float moveSpeed) {
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

    private bool TryMove (Vector2 direction, float moveSpeed) {
        //if(direction != Vector2.zero) { // <-- esto es viejo, BORRAR
        if(IsMoving(direction,moveSpeed)) {
            // Check for potential collisions
            int count = movementCollider.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                entityData.movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                (moveSpeed) * Time.fixedDeltaTime + entityData.collisionOffset, // The amount to cast equal to the movement plus an offset
                true); // ignoreSiblingColliders = true: Determines whether the cast should ignore Colliders attached to the same Rigidbody2D (known as sibling Colliders))

            if(count == 0){
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
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
        return (direction != Vector2.zero) && (CurrentSpeed(direction,moveSpeed) >= entityData.velocidadMinima);
    }
    
    private float CurrentSpeed(Vector2 direction, float moveSpeed){ // Calcula la velocidad lineal actual real
        Vector2 speedOnAxes = direction * moveSpeed;
        float linearSpeed = Mathf.Sqrt(Mathf.Pow(speedOnAxes.x,2)+Mathf.Pow(speedOnAxes.y,2));  
        Debug.Log("realDirection: "+direction.x+", "+direction.y);
        Debug.Log("realSpeed: "+linearSpeed);
        return linearSpeed;
    }

    // ---

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(aliveGOStartPosition, entityData.baseRadius);
    }
}
