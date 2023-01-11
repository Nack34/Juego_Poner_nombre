using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(NPCStats))]
public class Entity : MonoBehaviour
{
    public D_Entity entityData;
    public FiniteStateMachine stateMachine;

    public Rigidbody2D rb {get; private set;}
    public Animator animator {get; private set;}
    public GameObject NPC {get; private set;}
    public Vector2 NPCStartPosition {get; private set;}
    public Collider2D movementCollider {get; private set;}
    public Collider2D DetectionZone {get; private set;}// no es un collider CAMBIAR
    
    [SerializeField]
    private NPCStats stats;
    [SerializeField]
    private Vector2 direction= new Vector2 (0,0);
    public Vector2 Direction {
        get{
            return direction;
        }
        set{
            direction=value;
            animator.SetFloat(AnimationStrings.Xdirection,direction.x);
            animator.SetFloat(AnimationStrings.Ydirection,direction.y);
        }
    }

    public virtual void Start(){
        // from parent
        NPC = transform.parent.gameObject;
        NPCStartPosition = NPC.transform.position;
        rb = NPC.GetComponent<Rigidbody2D>();

        // from this
        animator = GetComponent<Animator>();
        stats = GetComponent<NPCStats>();

        // from childs
        movementCollider = transform.GetChild(0).GetComponent<Collider2D>();

        // new
        stateMachine = new FiniteStateMachine(); 
    }


    public virtual void Update() {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate(){
        stateMachine.currentState.PhysicsUpdate();
    }

    // ---

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(NPCStartPosition, entityData.speciesData.baseRadius);
    }
}
