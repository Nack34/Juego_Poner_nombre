using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
// Takes and handles input and movement for a player character    // todo el codigo que esta en comentarios es codigo de animaciones (cuando las implementemos, se utilizara)
public class Player_Controller : MonoBehaviour
{
    
    [SerializeField] private float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    //public SwordAttack swordAttack;

    // esto es para mover al player
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private bool canMove = true;
    
    Animator animator;

    // Awake is called before Start 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start(){
         // agregar lo de correr en FixedUpdate
    }


    public float MoveSpeed (){
        if (isMoving){
            if (isRunning)
            {
                return gameObject.GetComponent<Stats>().runSpeed;
            }
            else 
            {
                return gameObject.GetComponent<Stats>().walkSpeed;
            }
        } 
        else return 0;
    }
    private float moveSpeed;
    private void FixedUpdate() {
        if(canMove) {
            moveSpeed=MoveSpeed();  // TENER CUIDADO CON ESTO
            // If movement input is not 0, try to move
            if(movementInput != Vector2.zero){
                
                bool success = TryMove(movementInput); //me muevo en diagonal

                if(!success) { //si no puedo moverme en diagonal, lo intento en un solo eje
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if(!success) {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                
                //animator.SetBool("isMoving", success);
            }/* else {
                animator.SetBool("isMoving", false);
            }

            // Set direction of sprite to movement direction
            if(movementInput.x < 0) {
                spriteRenderer.flipX = true;
            } else if (movementInput.x > 0) {
                spriteRenderer.flipX = false;
            }*/
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
            animator.SetBool(AnimationStrings.isMoving,value);
        }
        get{
            return isMoving;
        }
    }
    public void OnMove(InputAction.CallbackContext context) { //obtengo los datos de Player Input
        movementInput = context.ReadValue<Vector2>();
        IsMoving = movementInput != Vector2.zero;
        if (IsMoving)
            CheckDirection(movementInput);    
    }

    private bool isRunning=false;
    public bool IsRunning{
        set {
            isRunning=value;
            animator.SetBool(AnimationStrings.isRunning,value);
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
        else if (context.canceled)
            {
                IsRunning=false;
            }
    }
    private string direction="down";
    public string Direction{
        set { 
            direction=value;
            switch (direction)
            {
                case "up": {
                    animator.SetBool(AnimationStrings.isLookingUp,true);
                    animator.SetBool(AnimationStrings.isLookingDown,false);
                    animator.SetBool(AnimationStrings.isLookingLeft,false);
                    animator.SetBool(AnimationStrings.isLookingRight,false);
                    break;
                }
                case "down": {
                    animator.SetBool(AnimationStrings.isLookingUp,false);
                    animator.SetBool(AnimationStrings.isLookingDown,true);
                    animator.SetBool(AnimationStrings.isLookingLeft,false);
                    animator.SetBool(AnimationStrings.isLookingRight,false);
                    break;
                }
                case "right": {
                    animator.SetBool(AnimationStrings.isLookingUp,false);
                    animator.SetBool(AnimationStrings.isLookingDown,false);
                    animator.SetBool(AnimationStrings.isLookingLeft,false);
                    animator.SetBool(AnimationStrings.isLookingRight,true);
                    break;
                }
                case "left": {
                    animator.SetBool(AnimationStrings.isLookingUp,false);
                    animator.SetBool(AnimationStrings.isLookingDown,false);
                    animator.SetBool(AnimationStrings.isLookingLeft,true);
                    animator.SetBool(AnimationStrings.isLookingRight,false);
                    break;
                }
            }
        }
        get {
            return direction;
        }
    }
    
    private void CheckDirection(Vector2 movementInput){ // le da prioridad al eje X
        if (movementInput.x == 0f)
            if (movementInput.y > 0f)
                Direction = "up"; 
            else Direction = "down";
        else if (movementInput.x > 0f)
            Direction = "right";
            else Direction = "left";
    }








    // lo hice yo
    public int tipoArma=0;  // estas 2 variables son modificadas desde otros scripts
    public int tipoHabilidad=0;
    public int tipoDisparo=0;
    public void OnNormalAttack(InputAction.CallbackContext context) 
    { 
        if (context.started) // si aprieto golpeo (armas cuerpo a cuerpo) o apunto (armas a distancia)
        {
            animator.SetTrigger(AnimationStrings.isAttacking); 
            animator.SetInteger(AnimationStrings.tipoArma,tipoArma);
            animator.SetInteger(AnimationStrings.tipoHabilidad,tipoHabilidad);
        } 
        /*else 
        {
            if (context.canceled) // si dejo de apretar no importa (armas cuerpo a cuerpo) o disparo (armas a distancia)
            {   
                if (// tipoHabilidad == (tipo de habilidad de disparo (a distancia)))
                {
                    animator.SetInteger(AnimationStrings.Disparo,true); // luego  
                }    
            }
        }*/
    }

    // de aca para abajo, NO lo hice yo
    /*void OnNormalAttack() {
        animator.SetTrigger("swordAttack");
    }*/

    /*public void SwordAttack() {
        LockMovement();

        if(spriteRenderer.flipX == true){
            swordAttack.AttackLeft();
        } else {
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack() {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }*/
}