using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour // guarda tipos de datos
{
    public enum PosibleHabilityClass {Unarmed=0,Combate=1,Agricultura=2,Ganaderia=3,Unicas=4}
    public enum PosibleWeaponType {Dagger=0,Sword=1/*,Bow=2,,Magic=3*/} // agregar herramientas ya sea ...
                                                                        // cambiando los nombres de por ejemplo "Dagger" a ...
                                                                        // "Arma0" y despues vendria "Arma1", reemplanzando "Sword", ...
                                                                        // y de esa manera cuando PosibleHabilityClass=Agricultura ...
                                                                        // Arma0 representaria un rastrillo. La otra posibilidad es ...
                                                                        // llamar a PosibleWeaponType como "PosibleWeaponTypeCombate", y ...
                                                                        // agregar un "PosibleWeaponTypeGanaderia" y que se seleccione ...
                                                                        // el enum que se quiere seleccionar mediante PosibleHabilityClass...
                                                                        // en algun script
    public enum PosibleHabilityType {Idle=0,Walk=1,Run=2,NormalAttack=3,Hability1=4,Hability2=5}
    public enum PosibleDirections {LD=0, Down=1, RD=2, 
                                   Left=3, NotDirection=4, Right=5, 
                                   LU=6, Up=7, RU=8}         
                                    // Las animaciones de las diagonales (son las mismas que Left o Right, pero ... 
                                    // ... cambian la direccion de los colliders viculados a esa animacion).
                                    // NotDirection esta si x=0 e y=0, nunca tendria q pasar, por lo ...
                                    // ... que -> se mantiene la ultima posicion

    // todo lo de aca para arriba CREO q solo lo usa el OLD player, borrar todo luego de hacer el nuevo player 


    public enum PosibleDamageType {Fisico=0,Magico=1,Verdadero=2} // usar System.Enum.GetValues(typeof(Enums.PosibleDamageType)).GetLength(0) ...
                                                                // ... para obtener la longitud del enum (en el script Damageable se usa)

    public enum PosibleFOVRanges {FaceToFaceRange=0, ShortRange = 1, LongRange=2 }
    public enum PosibleIdleAnimations { Nothing = 0, Rascarse = 1 }
    public enum PosibleLongRangeActions { Nothing = 0, ShootProjectile = 1}
    public enum PosibleShortRangeActions { Nothing = 0, Dash = 1, Charge = 2}
    public enum PosibleFTFRangeActions { Nothing = 0, Dash = 1, NormalAttack = 2 }
}