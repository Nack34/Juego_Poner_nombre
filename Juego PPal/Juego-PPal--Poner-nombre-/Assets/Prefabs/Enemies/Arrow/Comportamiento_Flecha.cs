using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comportamiento_Flecha : MonoBehaviour
{
    float dirX, dirY;
    float angulo;
    public int damage= 3;
    public string tipoDeDanio= "fisico";

    public GameObject player;

    Rigidbody2D rb;
    public float velocity=1.0f;
    private Vector2 direction;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float collisionOffset = 0.05f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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

    private void FixedUpdate()
    {
        tiempoHastaDestruirLaFlecha-= Time.fixedDeltaTime; 

        if (tiempoHastaDestruirLaFlecha <= 0f) // si paso el tiempo de vida se destruye
            Destroy(gameObject);
        else 
            rb.MovePosition(rb.position + direction * velocity * Time.deltaTime); // sino, me muevo recto
    }


    private void OnTriggerEnter2D(Collider2D other) {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null ) 
            damageable.RecibirDanio(damage,tipoDeDanio);
        Destroy(gameObject);
    }






}
