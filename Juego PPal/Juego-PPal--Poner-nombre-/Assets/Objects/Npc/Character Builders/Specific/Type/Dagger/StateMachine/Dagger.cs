using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Entity
{
    public Dagger_MoveState moveState {get; private set;}
    public Dagger_IdleState idleState {get; private set;}

    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    

    private List<string> posibleIdleAnimations = new  List<string>(); // llenarlo en start (cortar (control X) los metodos que estan en idleState a este script)


    public override void Start(){
        base.Start();

        moveState = new Dagger_MoveState (this, stateMachine, AnimationStrings.MoveState, moveStateData, this);
        idleState = new Dagger_IdleState (this, stateMachine, AnimationStrings.IdleState, idleStateData, posibleIdleAnimations, this);

        stateMachine.Initialize(moveState);
    }
    
}
