using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// guarda las variables y sus getters y setters. Los hijos de este script modifican los metodos
[RequireComponent(typeof(Rigidbody2D),typeof(Pathfinding.AIDestinationSetter))]
public class GlobalController : NPCController
{
    [SerializeField]
    protected Vector2 movingDirection = Vector2.zero;
    [SerializeField]
    protected Vector2 lookingDirection = Vector2.zero;
    [SerializeField]
    protected float currentSpeed = 0.0f;
    [SerializeField]
    protected Vector2 npcBaseCenter = Vector2.zero; // en los enemigos se queda estatica en awake, en los aliados ademas de en ...
                                                        // awake, en update se actualiza
    [SerializeField]
    protected Rigidbody2D rb; // en awake
    [SerializeField]
    protected Vector2 currentPosition = Vector2.zero;
    [SerializeField]
    protected Pathfinding.AIDestinationSetter destinationSetter;
    private bool DestinationSetterInicializado = false;
    private bool RbInicializado = false;
    private bool NPCBaseCenterInicializado = false;

    public override Vector2 MovingDirection {
         get{ 
            return movingDirection;
        }
         set{
            movingDirection=value; 
        }
    } 
    public override Vector2 LookingDirection {
         get{ 
            return lookingDirection;
        }
         set{
            lookingDirection=value; 
        }
    }
    public override float CurrentSpeed {
         get{ 
            return currentSpeed;
        }
         set{
            currentSpeed=value; 
        }
    }
    public override Vector2 NPCBaseCenter {
        get{ 
            if (!NPCBaseCenterInicializado){
                npcBaseCenter = transform.position;
                NPCBaseCenterInicializado = true;
            }
            return npcBaseCenter;
        }
    }
    public override Rigidbody2D Rb {
         get{ 
            if (!RbInicializado){
                rb = transform.GetComponent<Rigidbody2D>();
                RbInicializado = true;
            }
            return rb;
        }
    }
    public override Vector2 CurrentPosition {
         get{ 
            currentPosition = transform.position;
            return currentPosition;
        }
    }
    public override Pathfinding.AIDestinationSetter DestinationSetter {
         get{ 
            if (!DestinationSetterInicializado){
                destinationSetter = transform.GetComponent<Pathfinding.AIDestinationSetter>();
                DestinationSetterInicializado = true;
            }
            return destinationSetter;
        }
    }

    protected override void Awake() 
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
