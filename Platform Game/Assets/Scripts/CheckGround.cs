using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script del colisionador circular
public class CheckGround : MonoBehaviour
{
    private PlayerController player;    //Para poder obtener referencias del Script

	// Use this for initialization
	void Start ()
    {
        //Obtenemos el script del padre
        player = GetComponentInParent<PlayerController>();
	}

    //Comprueba si se esta chocando con algo
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Checa si es el suelo
        if(collision.gameObject.tag == "Ground")
            player.grounded = true;
    }

    //Cuando se sale de una colision
    private void OnCollisionExit(Collision collision)
    {
        //Checa si es el suelo
        if (collision.gameObject.tag == "Ground")
            player.grounded = false;
    }
}
