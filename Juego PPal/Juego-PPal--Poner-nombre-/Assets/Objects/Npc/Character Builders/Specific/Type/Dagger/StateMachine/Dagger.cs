using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Entity
{
    /*
    public Dagger_ActionState actionState {get; private set;}
    public CloseRangeNPC_FaceToFaceRangeActionState faceToFaceRangeActionState {get; private set;}
    public CloseRangeNPC_ShortRangeActionState shortRangeActionState {get; private set;}
    public CloseRangeNPC_LongRangeActionState longRangeActionState {get; private set;}
    */

    /*
    [SerializeField]
    private D_ActionState actionStateData;
    [SerializeField]
    private D_FaceToFaceRangeActionState faceToFaceRangeActionStateData;
    [SerializeField]
    private D_ShortRangeActionState shortRangeActionStateData;
    [SerializeField]
    private D_LongRangeActionState longRangeActionStateData;
    */
    
    //private RandomAnimationSelector IdleStaterandomAnimatorSelector; (lo q le dije a alvaro) 
    //private RandomAnimationSelector IdleStaterandomAnimatorSelector; (lo q le dije a alvaro) 
    //private RandomAnimationSelector IdleStaterandomAnimatorSelector; (lo q le dije a alvaro) 
    //private RandomAnimationSelector IdleStaterandomAnimatorSelector; (lo q le dije a alvaro) 

     
    public override void Awake() {
        base.Awake();
    }

    public override void Start(){
        base.Start();

        /*
            en este start inicializar los States de ataque

        */

        // esto esta aca ya que se tiene que hacer luego de inicializar todos los States
        stateMachine.Initialize(moveState);
    }







    
}
