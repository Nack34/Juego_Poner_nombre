using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_Flecha : MonoBehaviour
{
    float dirX, dirY;
    float angulo;
    public float velocity=1.0f;
    // public float danio= 3f;

    // Start is called before the first frame update
    void Start()
    {
        // me ubico en Pos Inicial (el arquero)
        GameObject Archer = GameObject.Find("Archer");
        gameObject.GetComponent<Transform>().position = new Vector3(Archer.gameObject.GetComponent<Transform>().position.x, Archer.gameObject.GetComponent<Transform>().position.y);

        // obtengo las dif de distancias con player
        GameObject player = GameObject.Find("Player");
        dirX = player.gameObject.GetComponent<Transform>().position.x - gameObject.GetComponent<Transform>().position.x;
        dirY = player.gameObject.GetComponent<Transform>().position.y - gameObject.GetComponent<Transform>().position.y;


        // obtengo el angulo (direccion a la que hay que ir para dirigirse hacia player)
        angulo = /*Mathf.Atan(dirX/dirY); */CalcularAngulo(dirX,dirY);
        

        Debug.Log("Angulo: "+angulo);
        Debug.Log("Sin (altura): "+Mathf.Sin (angulo));
        Debug.Log("Cos (base): "+Mathf.Cos (angulo));

        // obtengo los desplazamientos por unidad para mantener una velocidad constante 
        dirX = (Mathf.Sin (angulo))*(velocity);
        if ( dirY >= 0)
          dirY = (Mathf.Cos (angulo))*velocity;
        else
            dirY = -(Mathf.Cos (angulo))*velocity;
    }   


    // Update is called once per frame
    void Update()
    {
        //Me muevo recto 
        gameObject.transform.Translate(dirX * Time.deltaTime , dirY * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
            
        Debug.Log("Collision");
        if (choqueConPlayerOLimites(other)) //solo chequea player, arreglar colisiones 
        {
            Destroy(gameObject);
            // player.restarVida(danio);
        }
    }


    private bool choqueConPlayerOLimites (Collision2D other){
        return ( other.collider.CompareTag("Player") /*|| 
                other.collider.CompareTag("LeftLimit") || 
                other.collider.CompareTag("RightLimit") || 
                other.collider.CompareTag("UpLimit") || 
                other.collider.CompareTag("DownLimit") */ );
    }


    /*private float CalcularAngulo (float dirX , float dirY)
    {
        if ( dirY >= 0)
            if (dirX >= 0)
                return Mathf.Atan(dirX/dirY); //arcotangente (adyasente/opuesto)
            else
                return Mathf.Acos(dirX / (dirY*dirY + dirX*dirX) /*(dividido hipotenusa*//*)); // arcocoseno (adyasente/hipotenusa)
        else 
            if (dirX >= 0)
                return -Mathf.Atan(dirX/dirY); // menos arcotangente (adyasente/opuesto)
            else
                return -Mathf.Acos(dirX / (dirY*dirY + dirX*dirX) /*(dividido hipotenusa)*//*); // menos arcocoseno (adyasente/hipotenusa)
    }*/


    private float CalcularAngulo (float dirX , float dirY)
    {
        if ( dirY >= 0)
                return Mathf.Atan(dirX/dirY); //arcotangente (adyasente/opuesto)
        else 
                return -Mathf.Atan(dirX/dirY); // menos arcotangente (adyasente/opuesto)
    }
}
