using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script con lógica de movimiento (Asignado a Player)
public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    public LayerMask groundLayer; //Esta variable sirve para detectar la capa del suelo
    private Rigidbody2D rigidbody;

    //En el Awake se obtienen y configuran en el away
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		//Checa si se presionó Espacio para saltar
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
	}

    //Metodo que se encarga del salto
    private void Jump()
    {
        //Da el salto en el eje Y (Un impulso hacia el eje) F = ma =====> a = F / m
        if(IsTouchingTheGround())
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    //Checa si el personaje está tocando el suelo
    private bool IsTouchingTheGround()
    {

        if (Physics2D.Raycast(this.transform.position,  //Trazamos el rayo desde la posición del jugador
            Vector2.down,                               //En dirección hacia abajo
            0.2f,                                       //Hasta un máximo de 20 cm
            groundLayer))                               //y nos encontramos con la capa del suelo...
        {
            return true;
        }
        else
            return false;
    }
}
