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
            Debug.Log(limitedSpeed);
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
        }

        //Voltea la animación
        if (limitedSpeed < 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (limitedSpeed > 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
	}
}
