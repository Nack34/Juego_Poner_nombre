using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Entity),true)]
[CanEditMultipleObjects]
public class FOV_Editor : Editor {
    public void OnSceneGUI() {
        Entity entity = target as Entity;   
        
        if (!entity.inicializoFOVs) return;

        for (int i=0; i < System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length; i++){  
            
            Handles.color = Color.white;
            Handles.DrawWireDisc(entity.CurrentPosition, Vector3.forward, entity.realVisionRadius[i]);

            Vector2 angle01 = entity.AngleDirectionToPositionShift(entity.LookingDirection, -entity.realVisionAngle[i]/2);
            Vector2 angle02 = entity.AngleDirectionToPositionShift(entity.LookingDirection, entity.realVisionAngle[i]/2);
            
            Handles.DrawLine(entity.CurrentPosition, entity.CurrentPosition + angle01 * entity.realVisionRadius[i]);
            Handles.DrawLine(entity.CurrentPosition, entity.CurrentPosition + angle02 * entity.realVisionRadius[i]);

            Handles.color = Color.red;
            foreach (Transform visibleOpponent in entity.visibleOpponents[i])
            {
                Handles.DrawLine(entity.CurrentPosition, visibleOpponent.position);
            }
        }
        Handles.color= Color.magenta;
        if (entity.ClosestTarget != null){
            Handles.DrawLine(entity.CurrentPosition, entity.ClosestTarget.position);
        } else{
            Handles.DrawWireDisc(entity.closestTargetLastSeenPosition, Vector3.forward, entity.currentSeachRadius);
        }
    }
}
