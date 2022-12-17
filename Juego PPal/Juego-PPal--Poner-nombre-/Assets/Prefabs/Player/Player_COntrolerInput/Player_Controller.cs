using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Controller : MonoBehaviour // toma los datos de entrada y se encarga de estos para manejar a player
{
    // esto es para mover al player    
    [SerializeField] private float collisionOffset = 0.0005f; // offset for checking collision
    public ContactFilter2D movementFilter; // posible collisions
    Vector2 movementInput; // input
    Rigidbody2D rb; // rigidbody2D
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); // list of collisions
    
    Animator animator; // animator
    AnimationSelector animationSelector; // script

    UseHabilty useHabilty; // script
    // Awake is called before Start 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animationSelector = GetComponent<AnimationSelector>();
        useHabilty = gameObject.GetComponent<UseHabilty>();
        PosibleWeaponTypes =  (Enums.PosibleWeaponType[])System.Enum.GetValues(typeof(Enums.PosibleWeaponType));
        cantArmas = PosibleWeaponTypes.Length;
    }

    private void Start(){
        
    }

        // PARA MOVERSE (flechas o awsd) Y CORRER (shift)

    public float MoveSpeed (){
        if (IsMoving){
            if (IsRunning) {
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
            return animator.GetBool(AnimationStrings.CanMove); // devuelve false cuando se usan habilidades de no movimiento
        }
    }

    private float moveSpeed;
    private void FixedUpdate() {
        if(CanMove && !IsUsingHability) {
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
            if (!IsUsingHability){
                isMoving=value;
                if (isMoving && !IsRunning) // la animacion de correr tiene prioridad
                    animationSelector.currentHabilityType = Enums.PosibleHabilityType.Walk;
                else if (!isMoving && !IsRunning) // las animaciones de correr y caminar tienen prioridad
                    animationSelector.currentHabilityType = Enums.PosibleHabilityType.Idle;
            } else isMoving=false;
        }
        get{
            return isMoving;
        }
    }
    [SerializeField] private bool isUsingHability=false;
    public bool IsUsingHability{
        get{
            isUsingHability = gameObject.GetComponent<UseHabilty>().isUsingHability;
            return isUsingHability;
        }
    }

    public void OnMove(InputAction.CallbackContext context) { //obtengo los datos de Player Input (fleachas o wasd)
        if (!IsUsingHability)
            movementInput = context.ReadValue<Vector2>();
        else movementInput = Vector2.zero;
        IsMoving = movementInput != Vector2.zero;
        gameObject.GetComponent<Directions>().CheckDirection(movementInput);    
    }

    private bool isRunning=false;
    public bool IsRunning{
        set {
            if (!IsUsingHability)
                isRunning=value;
            else isRunning=false;
            if (isRunning) // si esta corriendo es la habilidad ppal, puede pisar las de caminar o correr
                animationSelector.currentHabilityType = Enums.PosibleHabilityType.Run;
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


        // REALIZAR UN ATAQUE NORMAL (click izquierdo del mouse)

    private bool Unarmed {
        get{
            return CurrentHabilityClass == Enums.PosibleHabilityClass.Unarmed;
        }
    }

    public void OnNormalAttack(InputAction.CallbackContext context) { // Con el shift
        if (!Unarmed){
            if (context.started) {
                useHabilty.NormalAttack();
            } //else if (context.canceled) {  BORRAR ESTO, EL FILTRO ES HECHO DEBIDO A QUE SOLO SE ACTIVARA EL DISPARO AL TERMINAR LA ANIMACION DE APUNTADO
            // UseHabilty.Disparo();
            //}
        }
    }

    
       // SELECCIONAR ARMA/HERRAMIENTA (tab)
    
    [SerializeField] private float tiempoManteniendoApretadoElTab=0; 
    [SerializeField] private float tiempoNecesarioParaAbrirLaRuedita=1f; 
    private void Update(){
        tiempoManteniendoApretadoElTab+=Time.deltaTime;
    }

    private int cantArmas; // inicializado en awake
    private Enums.PosibleWeaponType [] PosibleWeaponTypes; // inicializado en awake
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
                gameObject.GetComponent<CurrentWeaponStats>().TipoArmaActual = armaSeleccionada; // aca se setea el arma actual, se cambia en CurrentStatsWeapon 
            }
        }
    }

    private int i=0;
    public int SiguienteArma(){
        i++;
        if (i>(cantArmas-1))
            i=0;
        Debug.Log(i);
        return i;
    }


        //SELECCIONAR CLASE DE HABILIDAD (teclas: 0=unarmed, 1=combate, 2= ganaderia (no implementado), IMPLEMENTAR LOS DEMAS)

    [SerializeField] private Enums.PosibleHabilityClass currentHabilityClass = Enums.PosibleHabilityClass.Unarmed;
    private Enums.PosibleHabilityClass CurrentHabilityClass {
        set{
            currentHabilityClass=value;
            animationSelector.currentHabilityClass = currentHabilityClass;
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
}