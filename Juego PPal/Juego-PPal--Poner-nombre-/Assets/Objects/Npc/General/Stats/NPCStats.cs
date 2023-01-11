using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStats : MonoBehaviour
{
    [SerializeField]
    private Entity entity;
    private Curable curable;
    public Damageable damageable {get; private set;}
    
    
    private void Start()
    {
        entity = GetComponent<Entity>();
        
        vidaMax = (int)(entity.entityData.speciesData.vidaMaxBase * entity.entityData.typeData.multiplicadorDeVidaMax);
        Vida = vidaMax;

        curable = new Curable (this, 
                                entity.entityData.speciesData.segundosNecesariosParaCurarse, 
                                entity.entityData.speciesData.cantPorcentajeCuracionXSegundo);

        damageable = new Damageable (this);

    }

    private float tiempoSinRecibirDanio = 99.0f;

    void Update()
    {
        tiempoSinRecibirDanio+=Time.fixedDeltaTime;
        curable.LogicUpdate(tiempoSinRecibirDanio);
    }


    // STATS -----------
    
    
    public int vidaMax;
    [SerializeField] private int vida;  // vida actual
    public int Vida {
        set {
            vida=value;
            if (vida<=0){ 
                Muerte(); 
            }
        }
        get { return vida; }
    }

    private bool isAlive=true; // utilizado para condiciones y la animacion de morir
    public bool IsAlive{
        set{
            isAlive=value;
            entity.animator.SetBool(AnimationStrings.IsAlive,value);
        }
        get{
            return isAlive;
        }
    }


    // METODOS ---------------

    // IMPLEMENTAR: SI LA ENTIDAD MUERE, SE DESACTIVA, NO SE DESTRUYE. Fijarse si es posible usar la pileta de spawneo esa (del video)
    public void Muerte (){ 
        Debug.Log("muerte de la entidad: "+ entity.entityData.entityName);
        IsAlive=false; // (modifica la variable isAlive y el animator)
        // instanciar particulas
        //Destroy(gameObject);
    }
}
