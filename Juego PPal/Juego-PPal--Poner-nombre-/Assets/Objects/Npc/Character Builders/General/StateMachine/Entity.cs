using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(NPCStats))]
public class Entity : MonoBehaviour
{
    [Tooltip("Todos los datos de la entidad")]
    public D_Entity entityData; // set in inspector
    public FiniteStateMachine stateMachine = new FiniteStateMachine(); 

    public Rigidbody2D rb {get; private set;}
    public Animator animator {get; private set;}
    public GameObject NPC {get; private set;}
    public Vector2 NPCStartPosition {get; private set;} // traerlo del objeto padre
    public Collider2D movementCollider {get; private set;}

    private NPCStats stats;
    [SerializeField]
    private Vector2 direction= new Vector2 (0,0);
    public Vector2 Direction {
        get{
            return direction;
        }
        set{
            direction=value; // HACER: setear tmb la direccion en el objeto padre
            //Debug.Log("Direction change:"+direction);
            animator.SetFloat(AnimationStrings.Xdirection,direction.x);
            animator.SetFloat(AnimationStrings.Ydirection,direction.y);
        }
    }
    [HideInInspector]
    public float[] realVisionRadius;
    [HideInInspector]
    public float[] realVisionAngle;
    private float currentSpeed = 0.0f;
    public float CurrentSpeed {
        get{
            return currentSpeed;
        }
        set{
            currentSpeed=value; // HACER: setear tmb la velocidad en el objeto padre
        }
    }


    [HideInInspector]
    public bool inicializo = false;
    public virtual void Awake() {
        // from this
        animator = GetComponent<Animator>();
        stats = GetComponent<NPCStats>();

        int cantidadDeRangosFOVPosibles = System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length;
        hasTarget = new bool [cantidadDeRangosFOVPosibles];
        realVisionRadius = new float [cantidadDeRangosFOVPosibles];
        realVisionAngle = new float [cantidadDeRangosFOVPosibles];
        FOVArray = new FieldOfView [cantidadDeRangosFOVPosibles];
        visibleOpponents = new List<Transform> [cantidadDeRangosFOVPosibles];
        for (int i = 0; i < cantidadDeRangosFOVPosibles; i++)
        {
            visibleOpponents[i] = new List<Transform>();
        }
        inicializo = true;

        InicializarIdleStateParametros();
    }

    public virtual void Start(){

        // from parent
        NPC = transform.parent.gameObject;
        NPCStartPosition = NPC.transform.position; // esto se calcula y guarda en el objeto padre y aca solo se trae
        rb = NPC.GetComponent<Rigidbody2D>();

        // from childs
        movementCollider = transform.Find("Colliders/MovementCollider").GetComponent<Collider2D>();

/*
        ShortRangeFOV = transform.Find("Fields Of View/Short Distance FOV").GetComponent<FieldOfView>();
        LongRangeFOV = transform.Find("Fields Of View/Long Distance FOV").GetComponent<FieldOfView>();
        FaceToFaceRangeFOV = transform.Find("Fields Of View/Face-to-face Distance FOV").GetComponent<FieldOfView>();
*/

        // new
        InitializeFOVs();
        InitializeStates();


    }

    public virtual void OnEnable(){
        Direction=direction; // hacer q sea igual a la direccion del padre
        // CurrentSpeed = NPC.ScriptGlobal.currentSpeed; // hacer q sea igual a la velocidad del padre
    }


    public virtual void Update() {
        stateMachine.currentState.LogicUpdate();
        /*Debug.Log("Lista de Oponentes en vision: ");
        Debug.Log("En short range: "+ListToText(visibleOpponents[0]));
        Debug.Log("En long range: "+ListToText(visibleOpponents[1]));*/
    }

    private string ListToText(List<Transform> list)
    {
        string result = "";
        foreach(var listMember in list)
        {
            result += listMember.ToString() + "\n";
        }
        return result;
    }

    public virtual void FixedUpdate(){
        stateMachine.currentState.PhysicsUpdate();
    }

    // ---

    private void OnDrawGizmosSelected() {
        // zona de inicio // poner las sig 2 lineas de cod es el objeto padre
        Gizmos.color = Color.green; 
        Gizmos.DrawWireSphere(NPCStartPosition, entityData.speciesData.baseRadius);
        
        // movimiento
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, new Vector3 (direction.x * CurrentSpeed , direction.y * CurrentSpeed, 0));
    }




    // --- Para fields of view



    public List<Transform>[] visibleOpponents;
    public List<Transform> FaceToFaceRangeVisibleOpponents{
        get{
            return visibleOpponents[(int)Enums.PosibleFOVRanges.FaceToFaceRange];
        }
    }
    public List<Transform> ShortRangeVisibleOpponents{
        get{
            return visibleOpponents[(int)Enums.PosibleFOVRanges.ShortRange];
        }
    }
    public List<Transform> LongRangeVisibleOpponents{
        get{
            return visibleOpponents[(int)Enums.PosibleFOVRanges.LongRange];
        }
    }
    
    public bool[] hasTarget;
    public bool HasTarget {
        get{
            return HasTargetInFaceToFaceRange || HasTargetInShortRange || HasTargetInLongRange;
        }
    }
    public bool HasTargetInFaceToFaceRange {
        get{
            return hasTarget[(int)Enums.PosibleFOVRanges.FaceToFaceRange];
        }
    }
    public bool HasTargetInShortRange {
        get{
            return hasTarget[(int)Enums.PosibleFOVRanges.ShortRange];
        }
    }
    public bool HasTargetInLongRange {
        get{
            return hasTarget[(int)Enums.PosibleFOVRanges.LongRange];
        }
    }
    
    [HideInInspector]
    public FieldOfView[] FOVArray;
 
    private void InitializeFOVs(){
        for (int i=0; i < System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length; i++){
            FOVArray[i] = transform.Find("Fields Of View").GetChild(i).GetComponent<FieldOfView>();

            realVisionRadius[i] = entityData.speciesData.visionRadius[i] * entityData.typeData.visionRadiusMultiplier;
            realVisionAngle [i] = entityData.speciesData.visionAngle[i] * entityData.typeData.visionAngleMultiplier;
            if (realVisionRadius[i]  <= 0.0f || realVisionAngle[i]  <= 0.0f)  {
                Debug.LogError("NO SE CARGARON LOS DATOS DE FIELD OF VIEW EN "+entityData.entityName+", NO SE CREARAN LOS FOV");
            } else {
            visibleOpponents [i] = new List<Transform> ();
            hasTarget [i] = false;
            SetValuesFOV(FOVArray[i], realVisionRadius[i], realVisionAngle[i], entityData.bandoData.oponentFilter, entityData.npcData.nonSeeThroughObstaclesFilter,(Enums.PosibleFOVRanges) i);
            }
        }
    }

    private void SetValuesFOV (FieldOfView FOV, float visionRadius, float visionAngle, LayerMask targetFilter, LayerMask NSTObstaclesFilter, Enums.PosibleFOVRanges identifier){
        // Se cargan los datos en el FOV
        FOV.visionRadius=visionRadius;
        FOV.visionAngle=visionAngle;
        FOV.targetFilter=targetFilter;
        FOV.NSTObstaclesFilter=NSTObstaclesFilter;
        FOV.typeOfFOV=identifier;

        // Se inicia la coroutine 
        FOV.doFOVCheck= true;
    }



    // --- Para STATES

    public MoveState moveState {get; private set;}
    public IdleState idleState {get; private set;}

    [Header("States Data: ")]
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    
    private void InitializeStates(){

        moveState = new MoveState (this, stateMachine, AnimationStrings.MoveState, moveStateData);
        idleState = new IdleState (this, stateMachine, AnimationStrings.IdleState, idleStateData/*, IdleStaterandomAnimatorSelector*/);

    }


    private float[] posibleIdleAnimationsProbabilities;
    private int[] posibleIdleAnimations; 
    
    // llamado en Awake
    private void InicializarIdleStateParametros(){
        
        float sumaDeProbabilidades = 0.0f;
        posibleIdleAnimationsProbabilities = entityData.specificNPCdata.posibleIdleAnimationsProbabilities;

        // desde aca (el comentario sigue abajo) ...
        int cantidadDeAnimacionesIdle = System.Enum.GetValues(typeof(Enums.PosibleIdleAnimations)).Length;
        posibleIdleAnimations = new  int[cantidadDeAnimacionesIdle];
        for (int i = 0; i < cantidadDeAnimacionesIdle; i++)
        {
            posibleIdleAnimations[i] = i;
            sumaDeProbabilidades += posibleIdleAnimationsProbabilities[i];
        }
        // ... hasta aca. No es lo mismo que poner?:  posibleIdleAnimations = (int[]) System.Enum.GetValues(typeof(Enums.PosibleIdleAnimations));

        if (sumaDeProbabilidades != 100.0f){
            Debug.LogError("EN LA ENTIDAD: "+entityData.entityName+", NO SE CARGARON BIEN LAS PROBABILIDADES DE SELECCION DE ANIMACION IDLE, NO SE CREARA EL SELECTOR DE ANIMACIONES");
        }else {
            //IdleStaterandomAnimatorSelector = new RandomAnimationSelector(posibleIdleAnimations,posibleIdleAnimationsProbabilities); // selecciona las prox animaciones a usar (no se usaran todas)
        }
    }
    
}
