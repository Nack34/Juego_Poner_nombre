using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Controller : MonoBehaviour
{
    // usado cuando toco a player
    [SerializeField] private int damage= 3;
    [SerializeField] private string tipoDeDanio= "fisico"; // los tipos posibles son: fisico, magico, verdadero (lista actualizada el 9/12)
    

     // cosas para moverme
    Rigidbody2D rb;
    public GameObject player; // se ingresa en unity
    [SerializeField] private float velocity=1.0f;
    private float angulo;
    private Vector2 direction;

    // Awake is called when the script instance is being loaded. Before Start
    private void Awake (){ 
        
        rb = GetComponent<Rigidbody2D>(); // rb es mi propio Rigidbody2D
    }

    // Start is called before the first frame update
    private void Start()
    {
        // obtengo las dif de distancias con player
        GameObject player = GameObject.Find("Player");
        direction = new Vector2 (player.gameObject.GetComponent<Transform>().position.x - gameObject.GetComponent<Transform>().position.x, 
                                        player.gameObject.GetComponent<Transform>().position.y - gameObject.GetComponent<Transform>().position.y) ;
        // obtengo el valor de direction para que la flecha viaje de a poquito
        direction = ReducirEscala(direction); 

    }   

    private Vector2 ReducirEscala (Vector2 direction)
    {
         // obtengo el angulo (direccion a la que hay que ir para dirigirse hacia player)
        angulo = CalcularAngulo(direction);

        // obtengo los desplazamientos por unidad para mantener una velocidad constante 
        direction.x = (Mathf.Sin (angulo))*(velocity);
        if ( direction.y >= 0)
          direction.y= (Mathf.Cos (angulo))*velocity;
        else
            direction.y= -(Mathf.Cos (angulo))*velocity;
        return direction;
    }

    private float CalcularAngulo (Vector2 direction)
    {
        if ( direction.y >= 0)
            return Mathf.Atan(direction.x/direction.y); //arcotangente (adyasente/opuesto)
        else 
            return -Mathf.Atan(direction.x/direction.y); // menos arcotangente (adyasente/opuesto)
    }


    [SerializeField] private float tiempoHastaDestruirLaFlecha=10f;

    private void FixedUpdate() //es fixed ya que cambia rb, y eso tiene q ver con las fisicas
    {
        tiempoHastaDestruirLaFlecha-= Time.fixedDeltaTime; 

        if (tiempoHastaDestruirLaFlecha <= 0f) // si paso el tiempo de vida se destruye
            Destroy(gameObject);
        else 
            rb.MovePosition(rb.position + direction * velocity * Time.fixedDeltaTime); // sino, me muevo recto
    }


    private void OnTriggerEnter2D(Collider2D other) {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null )  // puede ser que lo que haya tocado no tenga que recibir danio
            damageable.RecibirDanio(damage,tipoDeDanio);
        Destroy(gameObject);
    }
}
