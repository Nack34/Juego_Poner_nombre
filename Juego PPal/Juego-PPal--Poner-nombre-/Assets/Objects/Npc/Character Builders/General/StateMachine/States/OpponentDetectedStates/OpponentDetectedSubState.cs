using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Usa moverse sin chocarse que tiene que estar en entity
// renombrar a OpponentDetectedSubState. IMPLEMENTAR
public class OpponentDetectedSubState : State
{
    protected bool canChangeMovement;

    protected float LastAttackTime {
        get{
            return entity.opponentDetectedState.lastAttackTime;
        }
        set {
            entity.opponentDetectedState.lastAttackTime = value;            
        }
    }
    protected bool isInAttackRange{
        get{
            return entity.opponentDetectedState.isInAttackRange;
        }
        set {
            entity.opponentDetectedState.isInAttackRange = value;            
        }
    }

    // constructor
    public OpponentDetectedSubState (Entity entity, FiniteStateMachine stateMachine, string animationName) : base(entity, stateMachine, animationName){
        
        // stateMachine al igual que animation no se usa. No se puede sacar de alguna manera?
    } 

    public override void Enter () {
        canChangeMovement = true;
        LastAttackTime = Time.time;
        SetClosestTarget();
        entity.StartComplexMovement();
    }

    public override void Exit () {
        SetSearchLevel();
    }

    public override void LogicUpdate () { 

        for (int i = 0; i < System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length; i++)
        {
            entity.visibleOpponents[i].UpdatePositions(); // funcion de KdTree
        }
        
        SetClosestTarget();
    
        isInAttackRange = IsInAttackRange();
        if (isInAttackRange){
            NearBehaviour();
        } else {
            FarBehaviour();
        }
    }
    
    private void SetClosestTarget () {
        entity.ClosestTarget = ClosestTargetInCurrentRange();
        if (entity.ClosestTarget != null){
            entity.LookingDirection = entity.CalculateDirection(entity.ClosestTarget.position);
        }  
    } 



    // --- usado por hijos

    protected Vector2 positionToGoTo;

    // angulo de movimiento
    protected int maxGradosDeMovimientoFromTargetPOV = 20;
    
    protected virtual Vector2 RandomNearPositionInCircle (float radius){
        // direccion a la que se tiene q mover el oponente hacia mi
        Vector2 directionFromTargetPOV = entity.CalculateDirection(entity.ClosestTarget.position) * -1;  

        // sumar angulos y obtener direccion 
        Vector2 positionTogoTo = (Vector2)entity.ClosestTarget.position + radius * 
            entity.AngleDirectionToPositionShift(directionFromTargetPOV, 
            Random.Range(-maxGradosDeMovimientoFromTargetPOV/2, maxGradosDeMovimientoFromTargetPOV/2));
        
        return positionTogoTo;
    }



    // --- codigo en hijos



    protected virtual Transform ClosestTargetInCurrentRange(){
        Debug.LogError("El Combat SubState: "+this.ToString()+", en la entidad: "+entity.entityData.entityName+"ejecuto el ClosestTargetInCurrentRange del padre");
        return null;
        // codigo en hijo
    }

    protected virtual bool IsInAttackRange(){
        Debug.LogError("El Combat SubState: "+this.ToString()+", en la entidad: "+entity.entityData.entityName+"ejecuto el IsInAttackRange del padre");
        return false;
        // codigo en hijo
    }
    
    protected virtual void SetSearchLevel(){
        // codigo en hijo  
    }

    protected virtual void FarBehaviour(){
        // codigo en hijo de hijo
    }

    protected virtual void NearBehaviour(){
        // codigo en hijo de hijo
    }







/* en attack state


    protected RandomActionSelector randomCombatActionSelector;
    protected AttackDetails attackDetails;
    protected string currentTypeOfWeaponAttack {
        get {
            return entity.currentTypeOfWeaponAttack;
        }
    }


    public override void TriggerAttack(){ // es llamada por el script nexo animacion-estado. FALTA IMPLEMENTAR (solo se usa en Combat animations)
        base.TriggerAttack();
        attackDetails = entity.GetAttackDetails();
        //Debug.Log("TriggerAttack, currentTypeOfWeaponAttack : "+ currentTypeOfWeaponAttack);

        // aca se chequean los oponentes golpeados y se envia el daño, mejorarlo. IMPLEMENTAR


        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(entity.CurrentPosition + 
                                                        (entity.LookingDirection*entity.entityData.typeData.attackDistace), 
                                                        entity.entityData.typeData.attackRadius, entity.entityData.bandoData.oponentFilter);
        
        foreach (Collider2D collider in detectedObjects)
        {
            
            Debug.Log("entity dice: collider.transform.parent.parent.ToString(): "+collider.transform.parent.parent.ToString());
            //collider.transform.SendMessage("Damage", attackDetails);
        }


    }
    public override void AnimationEnding(){ // es llamada por el script nexo animacion-estado. FALTA IMPLEMENTAR
        //Debug.Log("AnimationEnding, currentTypeOfWeaponAttack : "+ currentTypeOfWeaponAttack);

        entity.animator.SetBool(currentTypeOfWeaponAttack, false);
        entity.animator.SetBool(AnimationStrings.opponentDetectedState, true);
        
        LastAttackTime = Time.time;
        base.AnimationEnding();
        entity.StartComplexMovement();
    }

    protected virtual void Attack(string actionName, int actionSelected) { 
        entity.StopComplexMovement();
        Debug.Log("Attack, currentTypeOfWeaponAttack : "+ currentTypeOfWeaponAttack);

        entity.animator.SetBool(AnimationStrings.opponentDetectedState, false);
        entity.animator.SetBool(currentTypeOfWeaponAttack, true);
        TriggerAction(actionName, actionSelected); // selecciona en el animator la prox animacion

    }
*/





}
