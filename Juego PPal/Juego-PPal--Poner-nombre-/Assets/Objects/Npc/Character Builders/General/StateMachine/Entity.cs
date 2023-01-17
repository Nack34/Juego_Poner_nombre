using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://docs.google.com/document/d/1GgesLstlyUSVTOzc55LiB-MDlqdF7ofICKHARAiuMug/edit")]
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
    public bool inicializoFOVs = false; // es usado por FOV_Editor
    public virtual void Awake() {
        // from this
        animator = GetComponent<Animator>();
        stats = GetComponent<NPCStats>();

        inicializoFOVs = InitializeFOVs();

        // from parent
        NPC = transform.parent.gameObject;
        NPCStartPosition = NPC.transform.position; // esto se calcula y guarda en el objeto padre y aca solo se trae
        rb = NPC.GetComponent<Rigidbody2D>();

        // from childs
        movementCollider = transform.Find("Colliders/MovementCollider").GetComponent<Collider2D>();

        // new
        InitializeStates();
    }

    public virtual void InitializeAllCombatSubStates(){
        InitializeAllCombatState_RandomActionSelectors ();
        combatState = new CombatState (this, stateMachine, AnimationStrings.CombatState, combatStateData);
        // en los hijos del script Entity se agrega la creacion de los estados
    }

    public virtual void Start(){

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



    public KdTree<Transform>[] visibleOpponents;
    public KdTree<Transform> FaceToFaceRangeVisibleOpponents{
        get{
            return visibleOpponents[(int)Enums.PosibleFOVRanges.FaceToFaceRange];
        }
    }
    public KdTree<Transform> ShortRangeVisibleOpponents{
        get{
            return visibleOpponents[(int)Enums.PosibleFOVRanges.ShortRange];
        }
    }
    public KdTree<Transform> LongRangeVisibleOpponents{
        get{
            return visibleOpponents[(int)Enums.PosibleFOVRanges.LongRange];
        }
    }
    
    public Transform closestTarget;

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
 
    private bool InitializeFOVs(){
        
        int cantidadDeRangosFOVPosibles = System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length;
        hasTarget = new bool [cantidadDeRangosFOVPosibles];
        realVisionRadius = new float [cantidadDeRangosFOVPosibles];
        realVisionAngle = new float [cantidadDeRangosFOVPosibles];
        FOVArray = new FieldOfView [cantidadDeRangosFOVPosibles];
        visibleOpponents = new KdTree<Transform> [cantidadDeRangosFOVPosibles];


        for (int i=0; i < cantidadDeRangosFOVPosibles; i++){
            hasTarget [i] = false;
            realVisionRadius[i] = entityData.speciesData.visionRadius[i] * entityData.typeData.visionRadiusMultiplier;
            realVisionAngle [i] = entityData.speciesData.visionAngle[i] * entityData.typeData.visionAngleMultiplier;
            FOVArray[i] = transform.Find("Fields Of View").GetChild(i).GetComponent<FieldOfView>();
            if (realVisionRadius[i]  <= 0.0f || realVisionAngle[i]  <= 0.0f)  {
                Debug.LogError("NO SE CARGARON LOS DATOS DE FIELD OF VIEW EN "+entityData.entityName+", NO SE CREARAN LOS FOV");
            } else {
                SetValuesFOV(FOVArray[i], realVisionRadius[i], realVisionAngle[i], entityData.bandoData.oponentFilter, entityData.npcData.nonSeeThroughObstaclesFilter,(Enums.PosibleFOVRanges) i);
                visibleOpponents [i] = new KdTree<Transform>();
            }
        }

        return true;
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
    public CombatState combatState {get; protected set;}

    [Header("States Data: ")]
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    protected D_CombatStates combatStateData;

    // llamado en Awake
    private void InitializeStates(){
        randomIdleActionSelector = InitializeIdleState_RandomActionSelector();

        moveState = new MoveState (this, stateMachine, AnimationStrings.MoveState, moveStateData);
        idleState = new IdleState (this, stateMachine, AnimationStrings.IdleState, idleStateData, randomIdleActionSelector);
        InitializeAllCombatSubStates();


        stateMachine.Initialize(moveState);
    }


    // --- Para Random Action Selectors
    
    private RandomActionSelector randomIdleActionSelector; 

    // Combat Actions
    protected RandomActionSelector randomFTFRangeCombatActionSelector; 
    protected RandomActionSelector randomCloseRangeCombatActionSelector;
    protected RandomActionSelector randomLongRangeCombatActionSelector; 

    // llamado en Awake
    private RandomActionSelector InitializeIdleState_RandomActionSelector(){
        int[] posibleIdleActions = (int[]) System.Enum.GetValues(typeof(Enums.PosibleIdleActions));
        return InitializeRandomActionSelector( posibleIdleActions, entityData.specificNPCdata.idleActions_SeleccionProbabilities, entityData.specificNPCdata.idleActions_UseProbabilities);
    }

    // llamado en InitializeStates
    private void InitializeAllCombatState_RandomActionSelectors(){
        randomFTFRangeCombatActionSelector = InitializeCombatState_RandomActionSelector(entityData.specificNPCdata.FTFRangeCombatActions_SelectionProbabilities, entityData.specificNPCdata.FTFRangeCombatActions_UseProbabilities);
        randomCloseRangeCombatActionSelector = InitializeCombatState_RandomActionSelector(entityData.specificNPCdata.closeRangeCombatActions_SelectionProbabilities, entityData.specificNPCdata.closeRangeCombatActions_UseProbabilities);
        randomLongRangeCombatActionSelector = InitializeCombatState_RandomActionSelector(entityData.specificNPCdata.longRangeCombatActions_SelectionProbabilities, entityData.specificNPCdata.longRangeCombatActions_UseProbabilities);
    }

    private RandomActionSelector InitializeCombatState_RandomActionSelector(float[] Actions_SeleccionProbabilities, float[] Actions_UseProbabilities){
        int[] posibleCombatActions = (int[]) System.Enum.GetValues(typeof(Enums.PosibleCombatActions));
        return InitializeRandomActionSelector(posibleCombatActions, Actions_SeleccionProbabilities, Actions_UseProbabilities);
    }

    // chequear si la suma de prob. es 100 y crear cargar RandomActionSelector;
    private RandomActionSelector InitializeRandomActionSelector(int[] posibleActions, float[] actions_SeleccionProbabilities, float[] actions_UseProbabilities){
        float sumaDeProbabilidades = 0.0f;
        float sumaDeProbabilidades2 = 0.0f;

        for (int i = 0; i < posibleActions.Length; i++)
        {
            sumaDeProbabilidades += actions_SeleccionProbabilities[i];
            sumaDeProbabilidades2 += actions_UseProbabilities[i];
        }
        
        if (sumaDeProbabilidades != 100.0f || sumaDeProbabilidades2 != 100.0f)
            Debug.LogError("EN LA ENTIDAD: "+entityData.entityName+", NO SE CARGARON BIEN LAS PROBABILIDADES DE SELECCION DE ACCION");
        
        return new RandomActionSelector(posibleActions, actions_SeleccionProbabilities, actions_UseProbabilities);
    }
}
