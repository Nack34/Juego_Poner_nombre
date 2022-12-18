using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D),typeof(Directions))]
public class Player_Controller : MonoBehaviour // toma los datos de entrada y se encarga de estos para manejar a player
{
    // esto es para mover al player    
    [SerializeField] private float collisionOffset = 0.0005f; // offset for checking collision
    [SerializeField] private Collider2D movementCollider; // collider to check for collisions
    [SerializeField] private ContactFilter2D movementFilter; // posible collisions
    private Vector2 movementInput; // input
    private Rigidbody2D rb; // rigidbody2D
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); // list of collisions
    
    private Animator animator; // animator

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PosibleWeaponTypes =  (Enums.PosibleWeaponType[])System.Enum.GetValues(typeof(Enums.PosibleWeaponType));
        cantArmas = PosibleWeaponTypes.Length;
        directions=gameObject.GetComponent<Directions>();
    }



    public float MoveSpeed (){
        if (IsMoving){
            if (IsRunning) {
                return 3.0f;
                //gameObject.GetComponent<Stats>().runSpeed;
            }
            else  {
                return 2.0f; 
                //gameObject.GetComponent<Stats>().walkSpeed;
            }
        } 
        else return 0;
    }

    public bool CanMove {
        get { 
            return animator.GetBool(AnimationStrings.CanMove); // devuelve false cuando se usan habilidades de no movimiento
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
            int count = movementCollider.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                (moveSpeed) * Time.fixedDeltaTime + collisionOffset, // The amount to cast equal to the movement plus an offset
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




    
    // -- PARA SELECCIONAR DIRECCION, MOVERSE (flechas o awsd) Y CORRER (shift) ----------------------------------------------------------------------------


    private bool isMoving=false;
    public bool IsMoving{
        set {
            isMoving=value;
            animator.SetBool(AnimationStrings.IsMoving,isMoving);
        }
        get{
            return isMoving;
        }
    }

    Directions directions;
    public void OnMove(InputAction.CallbackContext context) { //obtengo los datos de Player Input (fleachas o wasd)
        movementInput = context.ReadValue<Vector2>();
        IsMoving = movementInput != Vector2.zero;
        if (IsMoving) 
            directions.CheckDirection(movementInput);    
    }



    private bool isRunning=false;
    public bool IsRunning{
        set {
            isRunning=value;
            animator.SetBool(AnimationStrings.IsRunning,isRunning);
        }
        get{
            return isRunning;
        }
    }
    public void OnRun(InputAction.CallbackContext context) { // Obtengo los datos de Player Input (shift)
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



    
    // -- SELECCIONAR ARMA/HERRAMIENTA (tab) ------------------------------------------------------------------------------------------------------------
    
    private bool Unarmed {
        get{
            return CurrentHabilityClass == Enums.PosibleHabilityClass.Unarmed;
        }
    }

    [SerializeField] private float tiempoManteniendoApretadoElTab=0; 
    [SerializeField] private float tiempoNecesarioParaAbrirLaRuedita=1f; 
    private void Update(){
        tiempoManteniendoApretadoElTab+=Time.deltaTime;
    }

    private int cantArmas; // inicializado en awake
    private Enums.PosibleWeaponType [] PosibleWeaponTypes; // inicializado en awake
    [SerializeField] private CurrentWeaponStats currentWeaponStats;  // se refencia en unity
    [SerializeField] private Enums.PosibleWeaponType armaSeleccionada= Enums.PosibleWeaponType.Dagger; 
    public void OnWeaponChange (InputAction.CallbackContext context){ 
        if (context.started && Unarmed) {
            CurrentHabilityClass = Enums.PosibleHabilityClass.Combate;
        }
        else {
            if (context.started) {
                tiempoManteniendoApretadoElTab=0f;
                armaSeleccionada=PosibleWeaponTypes[SiguienteArma()]; // siguiente arma en la ruedita
            } else if (context.performed && (tiempoManteniendoApretadoElTab > tiempoNecesarioParaAbrirLaRuedita)) {
                // IMPLEMENTAR: llamar a metodo que se encarga de abrir la ruedita y dependiendo del arma elegida, armaSeleccionada toma ese valor
                // armaSeleccionada = metodo del renglon de arriba ();
                // i= (int) armaSeleccionada;
            } else if (context.canceled){
                Debug.Log(armaSeleccionada);
                currentWeaponStats.TipoArmaActual = armaSeleccionada; // aca se setea el arma actual, se cambia en CurrentStatsWeapon 
            }
        }
    }

    private int i=0;
    public int SiguienteArma(){
        i++;
        if (i>(cantArmas-1))
            i=0;
        return i;
    }


    // -- SELECCIONAR CLASE DE HABILIDAD (teclas: 0=unarmed, 1=combate, 2= ganaderia (no implementado), IMPLEMENTAR LOS DEMAS) --------------------------------------

    [SerializeField] private Enums.PosibleHabilityClass currentHabilityClass = Enums.PosibleHabilityClass.Unarmed;
    private Enums.PosibleHabilityClass CurrentHabilityClass {
        set{
            currentHabilityClass=value;
   //         animationSelector.currentHabilityClass = currentHabilityClass; // FIJARSE QUE HACER CON ESTO. Sirve de algo 
        }
        get{
            return currentHabilityClass;
        }
    }

    public void OnSelectUnarmed (InputAction.CallbackContext context){ 
        CurrentHabilityClass = Enums.PosibleHabilityClass.Unarmed;
    }

    
    public void OnSelectCombate (InputAction.CallbackContext context){ 
        CurrentHabilityClass = Enums.PosibleHabilityClass.Combate;
    }

    /*
    public void OnSelectGanaderia (InputAction.CallbackContext context){ 
        CurrentHabilityClass = Enums.PosibleHabilityClass.Unarmed;
    }
*/








    

    // ---- Pasar al script attak ----------------------------------------------------------------------------------------------------------------------------------


        // REALIZAR UN ATAQUE NORMAL (click izquierdo del mouse) // pasarlo al script Use hability

    
    /*
    public void OnNormalAttack(InputAction.CallbackContext context) { // Con el shift
        if (!Unarmed){
            if (context.started) {
                useHabilty.NormalAttack();
            } //else if (context.canceled) {  BORRAR ESTO, EL FILTRO ES HECHO DEBIDO A QUE SOLO SE ACTIVARA EL DISPARO AL TERMINAR LA ANIMACION DE APUNTADO
            // UseHabilty.Disparo();
            //}
        }
    }*/







}