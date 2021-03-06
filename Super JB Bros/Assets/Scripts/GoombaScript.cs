﻿using System.Collections;
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

        //Si está tocando el piso y no tocará el piso en unos 20 cm
        if(IsTouchTheGround() && !IsTouchTheGroundFuture())
        {
            transform.localScale = new Vector3(isLeft ? -1f : 1f, 1f, 1f);
            speed = -speed;
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
            isLeft = !isLeft;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checa si el collider es isTrigger
        if(collision.isTrigger)
        {
            //Checa que el objeto del collider no haya colisionado con la mascara del piso
            if(collision.gameObject.layer != groundLayer)
            {
                //Checa que no sea el jugador
                if(collision.tag != "Player")
                {
                    transform.localScale = new Vector3(isLeft ? -1f : 1f, 1f, 1f);
                    speed = -speed;
                    rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
                    isLeft = !isLeft;
                }
            }
        }

        if(collision.tag == "Player")
        {
            MarioController controller = MarioController.getInstance;
            
            //Valor para la colision vertical (es la diferencia de distancias de y del jugador y de donde se colisionara el enemigo)
            float yOffset = 0.2f;

            //El personaje colisionó por la parte de arriba
            if ((transform.position.y + yOffset) < collision.transform.position.y)
            {
                Hide();
                controller.JumpByObject();
            }
            else
            {
                //Le provoca daño al personaje
                controller.SetHealth(controller.GetHealth() - 10);
                controller.EnemyKnockBack(transform.position.x);
            }
        }
    }

    //Esconde al enemigo simulando que lo destruye
    private void Hide()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;

        CircleCollider2D[] circles = this.GetComponents<CircleCollider2D>();
        foreach (CircleCollider2D circle in circles)
            circle.enabled = false;
    }

    //Checa si el enemigo está tocando el piso
    private bool IsTouchTheGround()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, groundLayer);
    }

    //Checa si el enemigo estara tocando el piso (2 cm mas adelante)
    private bool IsTouchTheGroundFuture()
    {
        Vector3 posicionAdelantada = new Vector3(isLeft ? this.transform.position.x - 0.2f : this.transform.position.x + 0.2f, 
            this.transform.position.y, this.transform.position.z);
        return Physics2D.Raycast(posicionAdelantada, Vector2.down, 0.2f, groundLayer);
    }
}
