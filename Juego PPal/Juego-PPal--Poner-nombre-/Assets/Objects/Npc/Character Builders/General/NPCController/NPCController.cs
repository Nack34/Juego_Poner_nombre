using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ubicado en el objeto padre, se encaga de todas las fases de un npc en especifico
// los getters y setters se encuentran en los scripts hijos
public class NPCController : MonoBehaviour
{   
    public virtual Vector2 MovingDirection {  get;  set; } 
    public virtual Vector2 LookingDirection {  get;  set; } 
    public virtual float CurrentSpeed {  get;  set; } 
    public virtual Vector2 NPCBaseCenter { get; } 
    public virtual Rigidbody2D Rb { get; } 
    public virtual Vector2 CurrentPosition { get; } 
    public virtual Pathfinding.AIDestinationSetter DestinationSetter { get; } 

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }
}
