using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;    //La velocidad máxima a la que se puede mover el personaje
    public float speed = 2f;    //Velocidad a la que se moverá el personaje horizontalmente
    private Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start ()
    {
        //Obtenemos el Rigidbody del personaje
        rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void FixedUpdate()
    {
        //Cuando se presiona una tecla que haga mover en horizontal al personaje (Negativo a la izquierda, positivo a la derecha)
        float horizontal = Input.GetAxis("Horizontal");

        //Realiza el movimiento del personaje
        rigidbody2D.AddForce(Vector2.right * speed * horizontal);

        //Debug.Log(rigidbody2D.velocity.x);

        //Restringe la velocidad máxima creando un vector que rectifica (Cuando se mueve a la derecha)
        if (rigidbody2D.velocity.x > maxSpeed)
            rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);

        //Restringe la velocidad máxima creando un vector que rectifica (Cuando se mueve a la derecha)
        if (rigidbody2D.velocity.x < -maxSpeed)
            rigidbody2D.velocity = new Vector2(-maxSpeed, rigidbody2D.velocity.y);
    }
}
