using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newSpeciesData", menuName = "Data/Entity Data/Specific Data/Species Data")]
public class D_Species : ScriptableObject
{
    // field of view base data

    public float[] visionRadius = new float [System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length]; // rango de vision (lo lejos que mira)
    public float ShortRangeVisionRadius {
        get{
            return visionRadius[(int)Enums.PosibleFOVRanges.ShortRange];
        }
        set{
            visionRadius[(int)Enums.PosibleFOVRanges.ShortRange] = value;
        }
    }
    public float LongRangeVisionRadius {
        get{
            return visionRadius[(int)Enums.PosibleFOVRanges.LongRange];
        }
        set{
            visionRadius[(int)Enums.PosibleFOVRanges.LongRange] = value;
        }
    }

    [Range(0,360)]
    public float[] visionAngle = new float [System.Enum.GetValues(typeof(Enums.PosibleFOVRanges)).Length]; // angulo TOTAL, para un solo lado es la mitad
    public float ShortRangeVisionAngle {
        get{
            return visionAngle[(int)Enums.PosibleFOVRanges.ShortRange];
        }
        set{
            visionAngle[(int)Enums.PosibleFOVRanges.ShortRange] = value;
        }
    }
    public float LongRangeVisionAngle {
        get{
            return visionAngle[(int)Enums.PosibleFOVRanges.LongRange];
        }
        set{
            visionAngle[(int)Enums.PosibleFOVRanges.LongRange] = value;
        }
    }





    // info de la zona de inicio
    public float baseRadius = 2.0f; // (este base se refiere al radio de la zona de inicio, zona base)

    // Stats base de la entidad
    public float maxMovementSpeed = 1.2f;
    public float minMovementSpeed = 0f;


    public float segundosNecesariosParaCurarse = 10.0f;
    public float cantPorcentajeCuracionXSegundo = 10.0f;
    public float vidaMaxBase = 5; //(despues se cambia a int)

}
