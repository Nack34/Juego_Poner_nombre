using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // ACLARACION: SOLO SE ACTIVARA EL LA HABILIDAD DE DISPARO AL TERMINAR LA ANIMACION DE APUNTADO, ESA HABILIDAD NO SE ENCUENTRA ACA

public class UseHabilty : MonoBehaviour // distribulle las seniales de acto dependiendo de la clase de habilidad actual 
{
    AnimationSelector animationSelector;

    private void Awake()
    {
        animationSelector = GetComponent<AnimationSelector>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private float temporizadorDeisUsingHability=0.5f, tiempoQueisUsingHability=0f;
    public bool isUsingHability=false;
    void Update()
    {
        tiempoQueisUsingHability+=Time.deltaTime;
        if (tiempoQueisUsingHability>=temporizadorDeisUsingHability){
            tiempoQueisUsingHability=0f;
            isUsingHability = false;        
        }
    }

    [SerializeField] private Enums.PosibleHabilityType tipoHabilidad; 
    private Enums.PosibleHabilityType TipoHabilidad{
        get{
            return tipoHabilidad;
        }
        set{
                tipoHabilidad = value;
                animationSelector.currentHabilityType = tipoHabilidad;
                isUsingHability = true;    
                tiempoQueisUsingHability=0f;
        }
    }

    public void NormalAttack() // clic izquierdo de mouse
    { // ACLARACION: las animaciones activan el collider del arma
        TipoHabilidad = Enums.PosibleHabilityType.NormalAttack;
    }

}
