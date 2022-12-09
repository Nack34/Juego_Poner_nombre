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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // obtengo las dif de distancias con player
        GameObject player = GameObject.Find("Player");
        direction = new Vector2 (player.gameObject.GetComponent<Transform>().position.x - gameObject.GetComponent<Transform>().position.x, 
                                        player.gameObject.GetComponent<Transform>().position.y - gameObject.GetComponent<Transform>().position.y) ;
        // obtengo el valor de direction para que la flecha viaje de a poquito
        direction = ReducirEscala(direction); 
    }   

    public float tiempoRestante=10f;

    // Update is called once per frame
    void FixedUpdate()
    {
        tiempoRestante = tiempoRestante - Time.fixedDeltaTime; 

        if (tiempoRestante <= 0f)
            Destroy(gameObject);
        else 
        {
            int count = rb.Cast(
                    direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                    movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                    castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                    velocity * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

                if(count == 0)
                    //Me muevo recto 
                    rb.MovePosition(rb.position + direction * velocity * Time.fixedDeltaTime);
                else 
                { //Debug.Log (castCollisions[0].collider.name);
                    if (castCollisions[0].collider.name == "Player")
                    {
                        //Debug.Log("Hago danio");
                        player.GetComponent<Stats_Player>().RecibirDanio(damage,tipoDeDanio);
                    } // le resta vida al player <---------------------------------------------------------------------------
                    Destroy(gameObject);
                }
        }
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
}
