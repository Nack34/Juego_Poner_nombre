using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStrings : MonoBehaviour // guarda datos
{
    public static string CanMove = "CanMove";
    public static string Death = "Death"; 
    public static string Hitted = "Hitted"; 
    public static string Armed = "Armed";
    public static string Unarmed = "Unarmed";
    public static string Revivir = "Revivir"; 
    public static string SeguirAnimando = "SeguirAnimando";
    public static string DejarDeAnimar = "DejarDeAnimar";

    public static string [] PosibleHabilityClass = {"Unarmed","Combate","Agricultura","Ganaderia","Unicas"};
    public static string [] PosibleWeaponType = {"Dagger","Sword","Bow","Magic"}; 
    public static string [] PosibleHabilityType = {"Idle","Walk","Run","NormalAttack","Hability1","Hability2"};
    public static string [] PosibleDirections = {"LD", "Down", "RD", 
                                                "Left", "NotDirection", "Right", 
                                                "LU", "Up", "RU"};         
                                                // Las animaciones de las diagonales (son las mismas que Left o Right, pero ... 
                                                // ... cambian la direccion de los colliders viculados a esa animacion).
                                                // NotDirection esta si x=0 e y=0, nunca tendria q pasar, por lo ...
                                                // ... que -> IMPLEMENTAR: que salte mensaje avisando
}
