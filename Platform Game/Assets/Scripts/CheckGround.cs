using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script del colisionador circular
public class CheckGround : MonoBehaviour
{
    private PlayerController player;    //Para poder obtener referencias del Script
    private Rigidbody2D rigidbody;  //Obtiene las fisicas del personaje

	// Use this for initialization
	void Start ()
    {
        //Obtenemos el script del padre
        player = GetComponentInParent<PlayerController>();
        rigidbody = GetComponentInParent<Rigidbody2D>();
	}

    //Cuando entra a la colision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Checa si es una plataforma movil
        if (collision.gameObject.tag == "Platform")
        {
            //Cancela la velocidad del personaje
            rigidbody.velocity = new Vector3(0f, 0f, 0f);

            //Cambia al personaje a ser hijo de la plataforma para que se muevan juntos
            player.transform.parent = collision.transform;
            player.grounded = true;
        }
    }

    //Comprueba si se esta chocando con algo
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Checa si es el suelo
        if(collision.gameObject.tag == "Ground")
            player.grounded = true;

        //Checa si es una plataforma movil
        if (collision.gameObject.tag == "Platform")
        {
            //Cambia al personaje a ser hijo de la plataforma para que se muevan juntos
            player.transform.parent = collision.transform;
            player.grounded = true;
        }
    }

    //Cuando se sale de una colision
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Checa si es el suelo
        if (collision.gameObject.tag == "Ground")
            player.grounded = false;

        //Checa si es una plataforma movil
        if (collision.gameObject.tag == "Platform")
        {
            //Cambia al personaje a no ser hijo de la plataforma para que no se muevan juntos
            player.transform.parent = null;
            player.grounded = false;
        }
    }
}
