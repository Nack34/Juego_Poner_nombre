using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubGlobalController : NPCController
{
    private bool superControllerInicializado = false;
    protected NPCController superController;
    public virtual NPCController SuperController {
         get{ 
            if (!superControllerInicializado){
                superController = transform.parent.GetComponent<NPCController>();
                superControllerInicializado = true;
            }
            return superController;
        }
    } 

    public override Vector2 MovingDirection {
         get{ 
            return SuperController.MovingDirection;
        }
         set{
            SuperController.MovingDirection=value; 
        }
    } 
    public override Vector2 LookingDirection {
         get{ 
            return SuperController.LookingDirection;
        }
         set{
            SuperController.LookingDirection=value; 
        }
    }
    public override float CurrentSpeed {
         get{ 
            return SuperController.CurrentSpeed;
        }
         set{
            SuperController.CurrentSpeed=value; 
        }
    }
    public override Vector2 NPCBaseCenter {
        get{ 
            return SuperController.NPCBaseCenter;
        }
    }
    public override Rigidbody2D Rb {
         get{ 
            return SuperController.Rb;
        }
    }
    public override Vector2 CurrentPosition {
         get{ 
            return SuperController.CurrentPosition;
        }
    }
    public override Pathfinding.AIDestinationSetter DestinationSetter {
         get{ 
            return SuperController.DestinationSetter;
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
