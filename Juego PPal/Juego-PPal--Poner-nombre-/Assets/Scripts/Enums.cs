using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum PosibleHabilityClass {Combate=0,Agricultura=1,Ganaderia=2,Unicas=3}
    public enum PosibleWeaponType {Dagger=0,Bow=1,Sword=2,Magic=3}
    public enum PosibleHabilityType {Idle=0,Walk=1,Run=2,NormalAttack=3,Hability1=4,Hability2=5}
    public enum PosibleDirections {LD=0, Down=1, RD=2, 
                                   Left=3, NotDirection=4, Right=5, 
                                   LU=6, Up=7, RU=8}         
                                    // Las animaciones de las diagonales (son las mismas que Left o Right, pero ... 
                                    // ... cambian la direccion de los colliders viculados a esa animacion).
                                    // NotDirection esta si x=0 e y=0, nunca tendria q pasar, por lo ...
                                    // ... que -> IMPLEMENTAR: que salte mensaje avisando
}