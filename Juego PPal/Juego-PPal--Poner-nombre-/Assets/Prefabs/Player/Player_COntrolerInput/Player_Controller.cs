using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
// Takes and handles input and movement for a player 
public class Player_Controller : MonoBehaviour
{ /*
    // esto es para mover al player    
    [SerializeField] private float collisionOffset = 0.0005f; // offset for checking collision
    public ContactFilter2D movementFilter; // posible collisions
    Vector2 movementInput; // input
    Rigidbody2D rb; // rigidbody2D
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); // list of collisions
    
    Animator animator; // animator
    AnimationSelector animationSelector; // script

    Attack attack; // script
    // Awake is called before Start 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animationSelector = GetComponent<AnimationSelector>();
        attack = gameObject.GetComponent<Attack>();
    }

    private void Start(){
        
    }

    public float MoveSpeed (){
        if (isMoving){
            if (isRunning) {
                return gameObject.GetComponent<Stats>().runSpeed;
            }
            else  {
                return gameObject.GetComponent<Stats>().walkSpeed;
            }
        } 
        else return 0;
    }

    public bool CanMove {
        get { 
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private float moveSpeed;
    private void FixedUpdate() {
        if(CanMove) {
            moveSpeed=MoveSpeed();  // TENER CUIDADO CON ESTO
            // If movement input is not 0, try to move
            if(IsMoving){
                
                bool success = TryMove(movementInput); //me muevo en diagonal

                if(!success) { //si no puedo moverme en diagonal, lo intento en un solo eje
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if(!success) {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

        }
    }

    private bool TryMove(Vector2 direction) {
        if(direction != Vector2.zero) {
            // Check for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                (moveSpeed) * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

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

    private bool isMoving=false;
    public bool IsMoving{
        set {
            isMoving=value;
            AnimationSelector.isMoving = value;
        }
        get{
            return isMoving;
        }
    }
    public void OnMove(InputAction.CallbackContext context) { //obtengo los datos de Player Input
        movementInput = context.ReadValue<Vector2>();
        IsMoving = movementInput != Vector2.zero;
        if (IsMoving)
            gameObject.GetComponent<Directions>().CheckDirection(movementInput);    
    }

    private bool isRunning=false;
    public bool IsRunning{
        set {
            isRunning=value;
            AnimationSelector.isRunning = value;
        }
        get{
            return isRunning;
        }
    }
    public void OnRun(InputAction.CallbackContext context) { //obtengo los datos de Player Input
        if (context.started)
        {
            IsRunning=true;
        } 
        else 
            if (context.canceled)
            {
                IsRunning=false;
            }
    }

    public void OnNormalAttack(InputAction.CallbackContext context) { //obtengo los datos de Player Input
        if (context.started) {
            attack.NormalAttack();
        } else if (context.canceled) {
            attack.Disparo();
        }
    }


*/
}