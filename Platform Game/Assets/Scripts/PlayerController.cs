﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script del Player
public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;    //La velocidad máxima a la que se puede mover el personaje
    public float speed = 2f;    //Velocidad a la que se moverá el personaje horizontalmente
    public bool grounded;       //Se verifica en el Script del hijo (colisionador circular)
    public float jumpPower = 6.5f;  //La fuerza que se le dará al personaje para saltar
    private Rigidbody2D rigidbody2D;    //Para acceder a las fisicas del personaje
    private Animator animator;      //Para acceder a las animaciones y configuraciones de estas del personaje
    private bool jump;  //Para saber si saltó
    private bool doubleJump; //Permite el doble salto en el personaje
    private bool movement = true;
    private SpriteRenderer sprite;  //Obtiene el Sprite que está usando el personaje

	// Use this for initialization
	void Start ()
    {
        //Obtenemos el Rigidbody del personaje
        rigidbody2D = GetComponent<Rigidbody2D>();

        //Obtenemos las animaciones
        animator = GetComponent<Animator>();

        //Obtenemos el sprite
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Definimos uno de los parametros de la animacion
        animator.SetFloat("Speed", Mathf.Abs(rigidbody2D.velocity.x)); //Sacamos el valor sin signo

        //Definimos si toca el suelo el personaje
        animator.SetBool("Grounded", grounded);

        //Para el salto de precaución
        if(grounded)
        {
            doubleJump = true;
        }

        //Checamos si el personaje tecleo la flecha de hacia arriba y cambia de estado cuando esté tocando el suelo
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Salta y permite el doble salto
            if (grounded)
            {
                jump = true;
                doubleJump = true;
            }
            else if(doubleJump)
            {
                jump = true;
                doubleJump = false;
            }
        }

    }

    private void FixedUpdate()
    {
        //Cuando se presiona una tecla que haga mover en horizontal al personaje (Negativo a la izquierda, positivo a la derecha)
        float horizontal = Input.GetAxis("Horizontal");

        if (!movement)
            horizontal = 0;

        //Realiza el movimiento del personaje
        rigidbody2D.AddForce(Vector2.right * speed * horizontal);

        //Debug.Log(rigidbody2D.velocity.x);

        //Restringe la velocidad máxima creando un vector que rectifica (Cuando se mueve a la derecha)
        /*if (rigidbody2D.velocity.x > maxSpeed)
            rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);

        //Restringe la velocidad máxima creando un vector que rectifica (Cuando se mueve a la derecha)
        if (rigidbody2D.velocity.x < -maxSpeed)
            rigidbody2D.velocity = new Vector2(-maxSpeed, rigidbody2D.velocity.y);*/

        //ACTUALIZADO: El siguiente método realiza la misma función que los if
        float limitedSpeed = Mathf.Clamp(rigidbody2D.velocity.x, -maxSpeed, maxSpeed);
        rigidbody2D.velocity = new Vector2(limitedSpeed, rigidbody2D.velocity.y);

        //Checa hacia donde se mueve el personaje para voltearlo
        if (!(rigidbody2D.velocity.x >= -0.1f && rigidbody2D.velocity.x < 0.1f))
        {
            if (rigidbody2D.velocity.x > 0.1f)
            {
                //Volteando a la derecha
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            if (rigidbody2D.velocity.x < 0.1f)
            {
                //Volteando a la izquierda
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }

        //Checa si saltó el personaje y agrega una fuerza hacia arriba
        if (jump)
        {
            //Rectifica la velocidad y genera el impulso
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            rigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }
    }

    //Checa si el personaje desaparece de la vista
    private void OnBecameInvisible()
    {
        //Regresa a la posición donde inició
        transform.position = new Vector3(-5.9f, -2.26f, 0);
    }

    //Cuando se le pisa a un enemigo, el personaje brinca
    public void EnemyJump()
    {
        jump = true;
    }

    //Metodo que se llama cuando el enemigo provoca un retroceso al personaje (daño)
    public void EnemyKnockBack(float enemyPosX)
    {
        jump = true;

        //Devuelve -1, 0 o 1 dependiendo del lado donde resulte (Negativo - Izquierda, Positivo - Derecha)
        float side = Mathf.Sign(enemyPosX - transform.position.x);

        //Aplicamos la fuerza para provocar un movimiento en diagonal en la direccion contraria al enemigo
        rigidbody2D.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse);

        //Hace que el personaje no tenga movimiento al tener daño
        movement = false;
        Invoke("EnableMovement", 0.7f);

        //Cambia el color del sprite para simular daño
        Color color = new Color(255/255f, 106/255f, 0/255f);
        sprite.color = color;
    }

    //Recupera el movimiento y el color
    void EnableMovement()
    {
        movement = true;
        sprite.color = Color.white;
    }
}
