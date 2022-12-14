using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controler : MonoBehaviour
{
    
    [SerializeField] private float collisionOffset = 0.0005f;
    public ContactFilter2D movementFilter;
    public Vector2 direction;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    
//    Animator animator;

//    Attack attack;
    // Awake is called before Start 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
//        animator = GetComponent<Animator>();
//        attack = gameObject.GetComponent<Attack>();
    }    

    public bool CanMove {
        get { return true;
//            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive {
        get { return true;
//            return animator.GetBool(AnimationStrings.isAlive);
        }
    }
    
    private bool isMoving=false;
    public bool IsMoving{
        set {
            isMoving=value;
//            animator.SetBool(AnimationStrings.isMoving,value);
        }
        get{
            return isMoving;
        }
    }

    public Vector2 posObjetivo;
    public bool tieneObjetivo= false;
    private void FixedUpdate() {
        
        if (IsAlive){         // CHEQUEAR QUE HACER CON ESTA CONDICION, la puse yo, no se si esta bien o no. La traido del animator? De las stats? sirve de algo? 
            bool success = false;
            // tieneObjetivo si es daniado o en onTriggerEnter (y mientras se quede adentro) de la DetectionZone se pone (tieneObjetivo) en true y la posicion del objetivo en posObjetivo. En onTriggerExit, se seteara el tieneObjetivo en false
            if (tieneObjetivo){  //  aca dentro tiene que ir, o calcular las distancias y instantiante(Flecha), pasandole como parametros (COMO??) la distancia con el objetivo
                /*  direction = posObjetivo;
                    IsMoving = direction != Vector2.zero;
                    if (IsMoving)
                        gameObject.GetComponent<Directions>().CheckDirection(direction);
                    success = TryMovingAllDirections(direction); // <- va esto en realidad, pero necesito primero implementar posObjetivo
                */ 
                Debug.Log("sin implementar ir a un enemigo, asi q me muevo random");
            }

            if(!tieneObjetivo || !success) { // si !tieneObjetivo tiene q moverse random, si tieneObjetivo pero !success en ir a donde queria, se mueve random (esto ultimo es para intentar evitar que se quede atascado en una pared) 
                direction = CalculateDirection(direction); 
//               Debug.Log("direction x: "+direction.x+", direction y: "+ direction);
                IsMoving = direction != Vector2.zero;
//                Debug.Log("IsMoving: "+ IsMoving);
                if (IsMoving)
                {
//                    gameObject.GetComponent<Directions>().CheckDirection(direction);
                }
                TryMovingAllDirections(direction);
            }
            
        }
        
    }

    [SerializeField] private float tiempoEnLaMismaDireccion=0;
    [SerializeField] private float tiempoNecesarioParaCambiarDeDireccion=2f; 
    private Vector2 CalculateDirection(Vector2 currentDirection)
    {
        tiempoEnLaMismaDireccion+=Time.fixedDeltaTime;
//        Debug.Log("tiempoEnLaMismaDireccion: "+ tiempoEnLaMismaDireccion);
//        Debug.Log("tiempoEnLaMismaDireccion>=tiempoNecesarioParaCambiarDeDireccion: "+ (tiempoEnLaMismaDireccion>=tiempoNecesarioParaCambiarDeDireccion));
        if (tiempoEnLaMismaDireccion>=tiempoNecesarioParaCambiarDeDireccion){ // solo quiero que cambie de direccion si pasaron 2 segundos y sigue en esa direccion
            tiempoEnLaMismaDireccion=0f;
            return RandomDireccion();
        }
        else {
            return currentDirection;
        }
    }

    private Vector2 RandomDireccion(){
        int x = Random.Range(-1, 2); // 3 posibilidades: -1, 0 o 1
        int y = Random.Range(-1, 2);
        return new Vector2 (x,y);
    }

    [SerializeField] private float moveSpeed;
    private float MoveSpeed (){ // es metodo y no propiedad porque quiero que el valor se mantenga igual entre los "TryMove" 
        if (isMoving){
            if (isRunning) { return 1f;
//                return gameObject.GetComponent<Stats>().runSpeed;
            }
            else  { return 0.3f;
//                return gameObject.GetComponent<Stats>().walkSpeed;
            }
        } 
        else return 0;
    }

    private bool TryMovingAllDirections(Vector2 direction) {
        if(CanMove) {
            moveSpeed=MoveSpeed();  // TENER CUIDADO CON ESTO
            // If movement input is not 0, try to move
            if(IsMoving){
                bool success = TryMove(direction); //me muevo en diagonal

                if(!success) { //si no puedo moverme en diagonal, lo intento en un solo eje
                    success = TryMove(new Vector2(direction.x, 0));
                }

                if(!success) {
                    success = TryMove(new Vector2(0, direction.y));
                }
                return success;
            } else return false;
        } else return false;

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

   
    private bool isRunning=false;
    public bool IsRunning{
        set {
            isRunning=value;
//            animator.SetBool(AnimationStrings.isRunning,value);
        }
        get{
            return isRunning;
        }
    }
    
    // implementar todo lo siguiente
    /*public void OnRun(InputAction.CallbackContext context) { // los enemigos corren y caminan o solo tienen una velocidad especifica?
        if (context.started)
        {
            IsRunning=true;
        } 
        else 
            if (context.canceled)
            {
                IsRunning=false;
            }
    } */ 

    /* public void OnNormalAttack(InputAction.CallbackContext context) { // esto iria en el la detectionZone de pegar
        if (context.started) {
            attack.NormalAttack();
        } else if (context.canceled) {
            attack.Disparo();
        }
    } */



}
