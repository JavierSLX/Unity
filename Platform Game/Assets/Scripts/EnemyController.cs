using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxSpeed = 1f;
    public float speed = 1f;
    private Rigidbody2D rigidbody; //Gestiona la física del enemigo

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //Le da movimiento dependiendo si la velocidad es positiva o negativa
        rigidbody.AddForce(Vector2.right * speed);

        //Crea una velocidad limitada
        float limitedSpeed = Mathf.Clamp(rigidbody.velocity.x, -maxSpeed, maxSpeed);
        rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);

        //Cuando el enemigo choca en algun lado cambia dependiendo de su velocidad limitada
        if (limitedSpeed > -0.01f && limitedSpeed < 0.01f)
        {
            speed = -speed;
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
        }

        //Voltea la animación
        if (limitedSpeed < 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (limitedSpeed > 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
	}

    //Cuando se colisiona con el protagonista
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            float yOffset = 0.2f;   //Valor para la colision vertical (es la diferencia de distancias de y del jugador y de donde se colisionara el enemigo)
            //Checa si la colision se está dando por arriba del enemigo
            if ((transform.position.y + yOffset) < collision.transform.position.y)
            {
                //Destruye al mismo enemigo cuando colisiona con el jugador
                Destroy(gameObject);

                //Hace que el personaje salte (Llama al metodo que se encuentra en el Script PlayerController)
                collision.SendMessage("EnemyJump");
            }
            else
                collision.SendMessage("EnemyKnockBack", transform.position.x);
        }
    }
}
