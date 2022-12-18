using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStrings : MonoBehaviour // guarda datos
{
    public static string CanMove = "CanMove";
    public static string IsAlive = "IsAlive"; 
    public static string Hitted = "Hitted"; 
    public static string IsUsingHability = "IsUsingHability"; 
    public static string IsRunning = "IsRunning";
    public static string IsMoving = "IsMoving";
    public static string DirectionX = "DirectionX";
    public static string DirectionY = "DirectionY";  

    public static string Revivir = "Revivir"; // IMPLEMENTAR

    public static string [] PosibleDirections = {"LD", "Down", "RD", 
                                                "Left", "NotDirection", "Right", 
                                                "LU", "Up", "RU"};         
                                                // Las animaciones de las diagonales (son las mismas que Left o Right, pero ... 
                                                // ... cambian la direccion de los colliders viculados a esa animacion).
                                                // NotDirection esta si x=0 e y=0, nunca tendria q pasar, por lo ...
                                                // ... que -> IMPLEMENTAR: que salte mensaje avisando
}
