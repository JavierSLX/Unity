using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{ 

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Cuando se dispara una colision entre 2 objects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si la colision se dio entre el jugador y quien contiene el script
        if(collision.tag == "Player")
        {
            MarioController.getInstance.Kill();
        }
    }
}
