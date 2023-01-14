using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldStats : MonoBehaviour
{
    
    [SerializeField] private Animator animator; // para IsAlive

    private float tiempoSinRecibirDanio=999f; // utilizado por curable
    public float TiempoSinRecibirDanio {
        set {
            tiempoSinRecibirDanio=value;
        }
        get {
            return tiempoSinRecibirDanio;
        }
    }  

    private bool isAlive=true; // utilizado para condiciones y la animacion de morir
    public bool IsAlive{
        set{
            isAlive=value;
            animator.SetBool(AnimationStrings.IsAlive,value);
        }
        get{
            return isAlive;
        }
    }
    
    // VIDA
    public int vidaMax=5;
    [SerializeField] private int vida=5;  // vida actual
    public int Vida {
        set {
            vida=value;
            if (vida<=0) // si tu vida llega a 0, moris
            {
                Muerte();
            }
        }
        get {
            return vida;
        }
    }
    
    // DEFENSAS
    [SerializeField] private int defensaFisicaBase =1;
    [SerializeField] private int defensaMagicaBase =1;
    [SerializeField] private int defensaVerdaderaBase =0; // mantener siempre en 0
    public int [] vectorDeDefensas = new int [System.Enum.GetValues(typeof(Enums.PosibleDamageType)).GetLength(0)];

    public int danioBase =1; // danio base de todas las armas, es sumado(?) por el danio de arma para obtener el danio total 
    public int stamina =5;  // utitlizado para correr, trabajar, etc
    public int percepcion =1; // algunas cosas (objetos) del mundo solo pueden verse con cierto puntaje de esto, de tenerlo demasiado bajo, estas cosas seran invicibles o se veran de otra manera
    public int carisma =1; // para relacionearte mejor con los npcs
    public int inteligencia =1; // sirve para resolver puzzles o trabajar en investigacion
    public float walkSpeed =1; // velocidad de caminar
    public float runSpeed =1; // velocidad de correr


    private void Awake()
    {
        // inicializacion del vector de defensas (no se me ocurrio manera de hacerlo dinamico)
        vectorDeDefensas[(int)Enums.PosibleDamageType.Fisico] = defensaFisicaBase;
        vectorDeDefensas[(int)Enums.PosibleDamageType.Magico] = defensaMagicaBase;
        vectorDeDefensas[(int)Enums.PosibleDamageType.Verdadero] = defensaVerdaderaBase;
    }
    

    private void Update(){
        TiempoSinRecibirDanio+=Time.fixedDeltaTime;
    }

    
    public void Muerte (){ // IMPLEMENTAR: SI EL PLAYER MUERE, SE DESACTIVA, NO SE DESTRUYE. Si no es el player, tratar de usar la pileta de spawneo esa (del video)
        Debug.Log("muerte de player");
        IsAlive=false; // (modifica la variable isAlive y el animator)
        //Destroy(gameObject);
    }


}
