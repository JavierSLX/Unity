using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaScript : MonoBehaviour
{
    private float maxSpeed = 1f;
    private float speed = 1f;
    private Rigidbody2D rigidbody;
    private bool isLeft;
    public LayerMask groundLayer;

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        isLeft = true;
	}

    private void FixedUpdate()
    {
        //Le da movimiento
        rigidbody.AddForce(Vector2.right * speed);

        //Crea una velocidad limitada
        float limitedSpeed = Mathf.Clamp(rigidbody.velocity.x, -maxSpeed, maxSpeed);
        rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);

        if(!IsTouchTheGroundFuture())
        {
            transform.localScale = new Vector3(isLeft ? -1f : 1f, 1f, 1f);
            speed = -speed;
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
            isLeft = !isLeft;
        }
    }

    //Checa si el enemigo esta tocando el piso (1 cm mas adelante)
    private bool IsTouchTheGroundFuture()
    {
        Vector3 posicionAdelantada = new Vector3(isLeft ? this.transform.position.x - 0.1f : this.transform.position.x + 0.1f, 
            this.transform.position.y, this.transform.position.z);
        return Physics2D.Raycast(posicionAdelantada, Vector2.down, 0.2f, groundLayer);
    }
}
