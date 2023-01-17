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
            Handles.DrawWireDisc(entity.transform.parent.position, Vector3.forward, entity.realVisionRadius[i]);

            Vector3 angle01 = AngleDirection(entity.Direction, -entity.realVisionAngle[i]/2);
            Vector3 angle02 = AngleDirection(entity.Direction, entity.realVisionAngle[i]/2);
            
            Handles.DrawLine(entity.transform.parent.position, entity.transform.parent.position + angle01 * entity.realVisionRadius[i]);
            Handles.DrawLine(entity.transform.parent.position, entity.transform.parent.position + angle02 * entity.realVisionRadius[i]);

            Handles.color = Color.red;
            foreach (Transform visibleOpponent in entity.visibleOpponents[i])
            {
                Handles.DrawLine(entity.transform.parent.position, visibleOpponent.position);
            }
        }
    }

    private Vector2 AngleDirection(Vector2 currentdirection, float angleInDregees){
        angleInDregees+=Vector2.SignedAngle(Vector2.right, currentdirection);;
        return (new Vector2 (Mathf.Cos(angleInDregees * Mathf.Deg2Rad), Mathf.Sin(angleInDregees * Mathf.Deg2Rad))) ;

    }

}
