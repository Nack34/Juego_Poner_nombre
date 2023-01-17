using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStrings : MonoBehaviour // guarda datos
{
    // Bools cambios de State
    public static string MoveState = "move";
    public static string IdleState = "idle"; 
    public static string CombatState = "move"; 
    public static string CombatSubState = "move"; 

    // Floats cambios en la direccion
    public static string Xdirection = "Xdirection";
    public static string Ydirection = "Ydirection";

    // Ints cambios de animacion en el mismo state (IMPORTANTE NO OLVIDAR: Estas animaciones llevan un evento...
    //... al final que avisa de su finalizacion)
    public static string IdleAction = "idleAction";
    public static string CombatAction = "combatAction";















    // de aca para abajo es todo viejo (lo usa el OLD player), cuando cambie lo del player hay q borralo
    public static string CanMove = "CanMove";
    public static string IsAlive = "IsAlive"; 
    public static string Hitted = "Hitted"; 
    public static string IsUsingHability = "IsUsingHability"; 
    public static string IsRunning = "IsRunning";
    public static string IsMoving = "IsMoving";
    public static string DirectionX = "DirectionX";
    public static string DirectionY = "DirectionY";  

    public static string Revivir = "Revivir"; 

    public static string [] PosibleDirections = {"LD", "Down", "RD", 
                                                "Left", "NotDirection", "Right", 
                                                "LU", "Up", "RU"};         
                                                // Las animaciones de las diagonales (son las mismas que Left o Right, pero ... 
                                                // ... cambian la direccion de los colliders viculados a esa animacion).
                                                // NotDirection esta si x=0 e y=0, nunca tendria q pasar, por lo ...
                                                // ... que -> IMPLEMENTAR: que salte mensaje avisando
}
