using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(NPCStats))]
public class Entity : MonoBehaviour
{
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
    public float[] realVisionRadius = new float [System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length];
    public float[] realVisionAngle = new float [System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length];
    private float currentSpeed = 0.0f;
    public float CurrentSpeed {
        get{
            return currentSpeed;
        }
        set{
            currentSpeed=value; // HACER: setear tmb la velocidad en el objeto padre
        }
    }

    public virtual void Awake() {
        // from this
        animator = GetComponent<Animator>();
        stats = GetComponent<NPCStats>();
        hasTarget = new bool [System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length];
    }

    public virtual void Start(){

        // from parent
        NPC = transform.parent.gameObject;
        NPCStartPosition = NPC.transform.position; // esto se calcula y guarda en el objeto padre y aca solo se trae
        rb = NPC.GetComponent<Rigidbody2D>();

        // from childs
        movementCollider = transform.Find("Colliders/MovementCollider").GetComponent<Collider2D>();
        ShortRangeFOV = transform.Find("Fields Of View/Short Distance FOV").GetComponent<FieldOfView>();
        LongRangeFOV = transform.Find("Fields Of View/Long Distance FOV").GetComponent<FieldOfView>();
        // new
        Debug.Log("Llegue a 1");
        InitializeFOVs();

    }

    public virtual void OnEnable(){
        Direction=direction; // hacer q sea igual a la direccion del padre
        // CurrentSpeed = NPC.ScriptGlobal.currentSpeed; // hacer q sea igual a la velocidad del padre
    }


    public virtual void Update() {
        stateMachine.currentState.LogicUpdate();
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

    public List<Transform>[] visibleOpponents = new List<Transform> [System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length];
    public List<Transform> ShortRangevisibleOpponents{
        get{
            return visibleOpponents[(int)Enums.PosibleFOVRanges.ShortRange];
        }
        set{
            visibleOpponents[(int)Enums.PosibleFOVRanges.ShortRange] = value;
        }
    }
    public List<Transform> LongRangevisibleOpponents{
        get{
            return visibleOpponents[(int)Enums.PosibleFOVRanges.LongRange];
        }
        set{
            visibleOpponents[(int)Enums.PosibleFOVRanges.LongRange] = value;
        }
    }
    
    public bool[] hasTarget;
    public bool HasTargetInShortRange {
        get{
            return hasTarget[(int)Enums.PosibleFOVRanges.ShortRange];
        }
        set{
            hasTarget[(int)Enums.PosibleFOVRanges.ShortRange] = value;
        }
    }
    public bool HasTargetInLongRange {
        get{
            return hasTarget[(int)Enums.PosibleFOVRanges.LongRange];
        }
        set{
            hasTarget[(int)Enums.PosibleFOVRanges.LongRange] = value;
        }
    }
    
    public FieldOfView[] FOVArray = new FieldOfView [System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length];
    public FieldOfView ShortRangeFOV {
        get{
            return FOVArray[(int)Enums.PosibleFOVRanges.ShortRange];
        }
        set{
            FOVArray[(int)Enums.PosibleFOVRanges.ShortRange] = value;
        }
    }
    public FieldOfView LongRangeFOV {
        get{
            return FOVArray[(int)Enums.PosibleFOVRanges.LongRange];
        }
        set{
            FOVArray[(int)Enums.PosibleFOVRanges.LongRange] = value;
        }
    }
 
    private void InitializeFOVs(){
        Debug.Log("Llegue a 2, el bool = "+System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length);
        for (int i=0; i < System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length; i++){
            Debug.Log("Llegue a 3");
            realVisionRadius[i] = entityData.speciesData.visionRadius[i] * entityData.typeData.visionRadiusMultiplier;
            realVisionAngle [i] = entityData.speciesData.visionAngle[i] * entityData.typeData.visionAngleMultiplier;
            if (realVisionRadius[i]  <= 0.0f || realVisionAngle[i]  <= 0.0f)  {
                Debug.Log("NO SE CARGARON LOS DATOS DE FIELD OF VIEW EN "+entityData.entityName+", NO SE CREARAN LOS FOV");
            } else {
                Debug.Log("Llegue a 4");
            visibleOpponents [i] = new List<Transform> ();
            Debug.Log("i: "+i+", hasTarget.Length: "+hasTarget.Length);
            hasTarget [i] = false;
            SetValuesFOV(FOVArray[i], realVisionRadius[i], realVisionAngle[i], entityData.bandoData.oponentFilter, entityData.npcData.nonSeeThroughObstaclesFilter,(Enums.PosibleFOVRanges) i);
            }
        }
    }

    private void SetValuesFOV (FieldOfView FOV, float visionRadius, float visionAngle, LayerMask targetFilter, LayerMask NSTObstaclesFilter, Enums.PosibleFOVRanges identifier){
        Debug.Log("Se cargan los datos");
        FOV.visionRadius=visionRadius;
        FOV.visionAngle=visionAngle;
        FOV.targetFilter=targetFilter;
        FOV.NSTObstaclesFilter=NSTObstaclesFilter;
        FOV.typeOfFOV=identifier;

        // coroutine 
        FOV.doFOVCheck= true;
    }

    /* // esto va en Field of View
    public IEnumerator FOVCheck(float waitTime = 0.2f){
        WaitForSeconds wait = new WaitForSeconds (waitTime);

        while (true){
            yield return wait;
            for (int i=0; i > System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length; i++){
                entity.FOVArray[i].FOV();
            }
        }
    }
    */

}
