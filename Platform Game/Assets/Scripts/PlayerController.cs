using System.Collections;
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

	// Use this for initialization
	void Start ()
    {
        //Obtenemos el Rigidbody del personaje
        rigidbody2D = GetComponent<Rigidbody2D>();

        //Obtenemos las animaciones
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Definimos uno de los parametros de la animacion
        animator.SetFloat("Speed", Mathf.Abs(rigidbody2D.velocity.x)); //Sacamos el valor sin signo

        //Definimos si toca el suelo el personaje
        animator.SetBool("Grounded", grounded);

        //Checamos si el personaje tecleo la flecha de hacia arriba y cambia de estado cuando esté tocando el suelo
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
            jump = true;
    }

    private void FixedUpdate()
    {
        //Cuando se presiona una tecla que haga mover en horizontal al personaje (Negativo a la izquierda, positivo a la derecha)
        float horizontal = Input.GetAxis("Horizontal");

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
        if(jump)
        {
            rigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }
    }
}
