using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://docs.google.com/document/d/1GgesLstlyUSVTOzc55LiB-MDlqdF7ofICKHARAiuMug/edit")]
[RequireComponent(typeof(EntityStats))]
public class Entity : MonoBehaviour
{
    [Tooltip("Todos los datos de la entidad")]
    public D_Entity entityData; // set in inspector
    public FiniteStateMachine stateMachine = new FiniteStateMachine(); 

    public Rigidbody2D rb {get; private set;}
    public Animator animator {get; private set;}
    public Collider2D movementCollider {get; private set;}
    public NPCController controller {get; private set;}
    public Pathfinding.AIDestinationSetter destinationSetter {get; private set;}
    private EntityStats stats;

    [HideInInspector]
    public float[] realVisionRadius;
    [HideInInspector]
    public float[] realVisionAngle;
    [HideInInspector]
    public bool inicializoFOVs = false; // es usado por FOV_Editor y OnDrawGizmosSelected

    //[SerializeField]
    //private Vector2 movingDirection = new Vector2 (0,0);
    public Vector2 MovingDirection {
        get{ //movingDirection = controller.MovingDirection;
            return controller.MovingDirection;
        }
        set{
            controller.MovingDirection = value;
            // animator
            animator.SetFloat(AnimationStrings.XMovingDirection,controller.MovingDirection.x); // sin uso en el animator actualmente
            animator.SetFloat(AnimationStrings.YMovingDirection,controller.MovingDirection.y); // sin uso en el animator actualmente
        }
    }
    //[SerializeField]
    //private Vector2 lookingDirection= new Vector2 (0,0);
    public Vector2 LookingDirection {
        get{  //lookingDirection = controller.LookingDirection;
            return controller.LookingDirection;
        }
        set{
            // direction=value; 
            controller.LookingDirection = value;

            animator.SetFloat(AnimationStrings.XLookingDirection,controller.LookingDirection.x);
            animator.SetFloat(AnimationStrings.YLookingDirection,controller.LookingDirection.y);
        }
    }

    //private Vector2 currentPosition = new Vector2 (0,0);
    public Vector2 CurrentPosition {
        get{ //currentPosition = controller.CurrentPosition;
            return controller.CurrentPosition;
        }
    }

    //private float currentSpeed = 0.0f;
    public float CurrentSpeed {
        get{ //currentSpeed = controller.CurrentSpeed;
            return controller.CurrentSpeed;
        }
        set{
            // currentSpeed=value;
            controller.CurrentSpeed = value;
        }
    }

    public Vector2 NPCBaseCenter { // viejo NPCStartPosition
        get {
            return controller.NPCBaseCenter;
        }
    }

    public virtual void Awake() {
        // from parent
        controller = transform.parent.GetComponent<NPCController>();
        rb = controller.Rb;
        destinationSetter = controller.DestinationSetter; 

        // from this
        animator = GetComponent<Animator>();
        stats = GetComponent<EntityStats>();

        inicializoFOVs = InitializeFOVs();

        // from childs
        movementCollider = transform.Find("Colliders/MovementCollider").GetComponent<Collider2D>();
        // new
        InitializeStates();
    }

    public virtual void InitializeAllCombatSubStates(){
        InitializeAllCombatState_RandomActionSelectors ();
        combatState = new CombatState (this, stateMachine, AnimationStrings.CombatState);
        // en los hijos del script Entity se agrega la creacion de los estados
    }

    public virtual void Start(){

    }

    public virtual void OnEnable(){
        MovingDirection=MovingDirection; // actualizar animators
        LookingDirection = LookingDirection; // actualizar animators
    }

    public virtual void Update() {
        stateMachine.currentState.LogicUpdate();
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
        if (inicializoFOVs){
            Gizmos.color = Color.green; 
            Gizmos.DrawWireSphere(NPCBaseCenter, entityData.speciesData.baseRadius);
            
            // movimiento
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, new Vector3 (MovingDirection.x * CurrentSpeed , MovingDirection.y * CurrentSpeed, 0));
        }
    }



    // --- Llamado por States



    public Vector2 CalculateDirection (Vector2 positionToGoTo){ // calcula la direccion necesaria para ir a ese punto
        Vector2 directionToGoTo = new Vector2((positionToGoTo.x - CurrentPosition.x), (positionToGoTo.y - CurrentPosition.y));
        directionToGoTo.Normalize(); // When normalized, a vector keeps the same direction but its length is 1.0
        return directionToGoTo;
    }

    public Vector2 AngleDirectionToPositionShift(Vector2 currentdirection, float angleInDregees){
        angleInDregees+=Vector2.SignedAngle(Vector2.right, currentdirection);;
        return (new Vector2 (Mathf.Cos(angleInDregees * Mathf.Deg2Rad), Mathf.Sin(angleInDregees * Mathf.Deg2Rad))) ;
    }

    public void StartComplexMovement(){ // buscar alguna manera de hacerlo mejor
        //if (destinationSetter.ai.isStopped){
        //    destinationSetter.ai.isStopped = false;
        //}
        if (!destinationSetter.ai.canMove){
            destinationSetter.ai.canMove = true;
        }
    }

    public void StopComplexMovement(){ // buscar alguna manera de hacerlo mejor
        //if (!destinationSetter.ai.isStopped){
        //    destinationSetter.ai.isStopped = true;
        //}
        if (destinationSetter.ai.canMove){
            destinationSetter.ai.canMove = false;
        }
    }
    
    public float ChangeLookingDirection (float lastLookingDirectionChangeTime, float tiempoEntreCambios, float maxGradosDeMovimientoDeMira){
        if (Time.time > lastLookingDirectionChangeTime + tiempoEntreCambios) {
            LookingDirection = SlightRandomLookingDirectionChange(LookingDirection, maxGradosDeMovimientoDeMira);
            return Time.time; 
        } else {
            return lastLookingDirectionChangeTime;
            }
    }
    private Vector2 SlightRandomLookingDirectionChange (Vector2 currentLookingDirection, float maxGradosDeMovimientoDeMira){
        return AngleDirectionToPositionShift(currentLookingDirection, Random.Range(-maxGradosDeMovimientoDeMira/2, maxGradosDeMovimientoDeMira/2)) ; //La idea es q sea un...
                                        //... movimiento dinamico de la cabeza, a la izquierda o derecha.
    }


    public Vector2 RandomPosition (float LeftAndDownRangeLimit, float RightAndUpRangeLimit){ // elije una posicion (un punto) random de la zona indicada
        return RandomVector2 (LeftAndDownRangeLimit, RightAndUpRangeLimit, LeftAndDownRangeLimit, RightAndUpRangeLimit);
    }
    private Vector2 RandomVector2(float minX, float maxX, float minY, float maxY){
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        return new Vector2 (x,y);
    }
    

    public float LinearDistanceTo(Vector2 placeToGo){ // devuelve la distancia lineal a el punto indicado
        Vector2 distace;
        distace.x = Mathf.Abs(placeToGo.x - CurrentPosition.x);
        distace.y = Mathf.Abs(placeToGo.y - CurrentPosition.y);
        
        float linearDistance = Mathf.Sqrt(Mathf.Pow(distace.x,2)+Mathf.Pow(distace.y,2)); 
        return linearDistance;
    }

    // --- Para fields of view

    public float currentSeachRadius;

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
    
    [HideInInspector]
    public Vector2 closestTargetLastSeenPosition;
    [SerializeField]
    private Transform closestTarget;
    public Transform ClosestTarget {
        get{
            return closestTarget;
        }
        set{
            if (value == null) {
                closestTargetLastSeenPosition = closestTarget.position;
            }
            closestTarget = value;
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



    // --- Para creacion de STATES



    public MoveState moveState {get; private set;}
    public IdleState idleState {get; private set;}
    public CombatState combatState {get; private set;} 
    public OpponentSearchState opponentSearchState {get; private set;}

    // llamado en Awake
    private void InitializeStates(){
        randomIdleActionSelector = InitializeIdleState_RandomActionSelector();

        moveState = new MoveState (this, stateMachine, AnimationStrings.MoveState);
        idleState = new IdleState (this, stateMachine, AnimationStrings.IdleState, randomIdleActionSelector);
        InitializeAllCombatSubStates();
        opponentSearchState = new OpponentSearchState (this, stateMachine, AnimationStrings.OpponentSearchState, entityData.speciesData.maxTiempoDeBusqueda);


        // primer estado:
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
