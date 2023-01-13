using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float visionRadius; 
    [Range(0,360)]
    public float visionAngle;
    public LayerMask targetFilter;
    public LayerMask NSTObstaclesFilter;
    public Entity entity;
    public Enums.PosibleFOVRanges typeOfFOV;
    public bool doFOVCheck = false;
    
    [SerializeField]
    private float timeBetweenFOVChecks = 0.2f;


    private void Start() {
        
        entity = transform.parent.parent.GetComponent<Entity>();
        
        // coroutine 
        while (!doFOVCheck){} // espera
        
        FOVCheck(timeBetweenFOVChecks);
    }


    IEnumerator FOVCheck(float waitTime = 0.2f){
        WaitForSeconds wait = new WaitForSeconds (waitTime);

        while (true){
            yield return wait;
            FOV();
        }
    }

    private void FOV (){  
        entity.visibleOpponents[(int)typeOfFOV].Clear();

        // primero obtengo todos los que esten en el posible rango de vision
        Collider2D[] tangetsInRange = Physics2D.OverlapCircleAll(entity.NPC.transform.position, visionRadius, targetFilter);
        
        for (int i=0; i < tangetsInRange.Length; i++){
        
            Transform target = tangetsInRange[i].transform;
            Vector2 directionToTarget = (target.position - entity.NPC.transform.position).normalized;

            // me fijo si realmente estan en el angulo de vision
            if (Vector2.Angle(entity.NPC.transform.up, directionToTarget) < visionAngle/2){
                
                float distanceToTarget = Vector2.Distance(entity.NPC.transform.position, target.position);

                // si esta en en algulo de vision, me fijo si tiene algun obstaculo en el medio
                if (!Physics2D.Raycast(entity.NPC.transform.position, directionToTarget, distanceToTarget, NSTObstaclesFilter)){
                    entity.visibleOpponents[(int)typeOfFOV].Add (target);
                }
            }
        }
        entity.hasTarget[(int)typeOfFOV] = entity.visibleOpponents[(int)typeOfFOV].Count > 0;
    }

}