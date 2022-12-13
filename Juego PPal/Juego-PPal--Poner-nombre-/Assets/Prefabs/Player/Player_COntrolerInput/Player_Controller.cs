using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
// Takes and handles input and movement for a player character    // todo el codigo que esta en comentarios es codigo de animaciones (cuando las implementemos, se utilizara)
public class Player_Controller : MonoBehaviour
{
    // esto es para mover al player    
    [SerializeField] private float collisionOffset = 0.0005f;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    
    Animator animator;

    // Awake is called before Start 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
            if(movementInput != Vector2.zero){
                
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
            gameObject.GetComponent<Directions>().CheckDirection(movementInput);    
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
    

    public void OnNormalAttack(InputAction.CallbackContext context) {
        if (context.started) {
            gameObject.GetComponent<Attack>().NormalAttack();
        } else if (context.canceled) {
            gameObject.GetComponent<Attack>().Disparo();
        }
    }

/* // BORRAR TODO LO SIGUIENTE
    // lo siguiente tiene q irse a otro script ("Attacking"?), este script es solo del movimiento del player
    // lo hice yo
    public int tipoArma=0;  // estas 2 variables son modificadas desde otros scripts
    public int tipoHabilidad=0;
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
                    animator.SetTrigger(AnimationStrings.Disparo); // luego  
                }    
            }
        }*/
    //}
}