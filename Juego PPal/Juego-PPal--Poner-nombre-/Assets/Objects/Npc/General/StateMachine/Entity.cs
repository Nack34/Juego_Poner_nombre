using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(NPCStats))]
public class Entity : MonoBehaviour
{
    public D_Entity entityData;
    public FiniteStateMachine stateMachine = new FiniteStateMachine(); 

    public Rigidbody2D rb {get; private set;}
    public Animator animator {get; private set;}
    public GameObject NPC {get; private set;}
    public Vector2 NPCStartPosition {get; private set;} // traerlo del objeto padre
    public Collider2D movementCollider {get; private set;}
    public Collider2D DetectionZone {get; private set;}// no es un collider CAMBIAR
    

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
    }

    public virtual void Start(){

        // from parent
        NPC = transform.parent.gameObject;
        NPCStartPosition = NPC.transform.position; // esto se calcula y guarda en el objeto padre y aca solo se trae
        rb = NPC.GetComponent<Rigidbody2D>();

        // from childs
        movementCollider = transform.Find("Colliders/MovementCollider").GetComponent<Collider2D>();

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
        Gizmos.DrawRay(transform.position, new Vector3 (direction.x * CurrentSpeed , direction.y * CurrentSpeed, 0) );
    }
}
