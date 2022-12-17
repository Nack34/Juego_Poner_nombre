using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    Damageable damageable;

    private float tiempoSinRecibirDanio=999f;   
    public float TiempoSinRecibirDanio {
        set {
            tiempoSinRecibirDanio=value;
        }
        get {
            return tiempoSinRecibirDanio;
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
                if (damageable != null ) /* esta condicion no se si hace falta. Osea, la pregunta es, que objeto podria recibir 
                                            danio de otro lado que no se damageable? (si la vida bajo a 0, significa que recibio 
                                            danio. Y si recibio danio, tendria q haberlo recibido por medio de damageable, no?)*/
                    damageable.Muerte();
            }
        }
        get {
            return vida;
        }
    }
    
    // DEFENSAS
    public int [] vectorDeDefensas = new int [System.Enum.GetValues(typeof(Enums.PosibleDamageType)).GetLength(0)];
    [SerializeField] private int defensaFisica =1;
    [SerializeField] private int defensaMagica =1;
    [SerializeField] private int defensaVerdadera =0; // mantener siempre en 0
    public int DefensaFisica{
        set {
            defensaFisica=value;
            vectorDeDefensas[(int)Enums.PosibleDamageType.Fisico] = defensaFisica;
        }
        get{
            return defensaFisica;
        }
    }
    public int DefensaMagica{
        set {
            defensaMagica=value;
            vectorDeDefensas[(int)Enums.PosibleDamageType.Magico] = defensaMagica;
        }
        get{
            return defensaMagica;
        }
    }
    public int DefensaVerdadera{
        set {
            defensaVerdadera=value;
            vectorDeDefensas[(int)Enums.PosibleDamageType.Verdadero] = defensaVerdadera;
        }
        get{
            return defensaVerdadera;
        }
    }


    public int danioBase =1; // danio base de todas las armas, es sumado(?) por el danio de arma para obtener el danio total 
    public int stamina =5;  // utitlizado para correr, trabajar, etc
    public int percepcion =1; // algunas cosas (objetos) del mundo solo pueden verse con cierto puntaje de esto, de tenerlo demasiado bajo, estas cosas seran invicibles o se veran de otra manera
    public int carisma =1; // para relacionearte mejor con los npcs
    public int inteligencia =1; // sirve para resolver puzzles o trabajar en investigacion
    public float walkSpeed =1; // velocidad de caminar
    public float runSpeed =1; // velocidad de correr


    void Awake()
    {
        damageable = gameObject.GetComponent<Damageable>();
        // inicializacion del vector de defensas
        DefensaFisica=DefensaFisica; 
        DefensaMagica=DefensaMagica;
        DefensaVerdadera=DefensaVerdadera;
    }
    

    void FixedUpdate(){
        TiempoSinRecibirDanio+=Time.fixedDeltaTime;
    }




}
