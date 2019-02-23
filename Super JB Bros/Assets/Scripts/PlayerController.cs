using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script con lógica de movimiento (Asignado a Player)
public class PlayerController : MonoBehaviour
{
    public float jumpForce = 15f;
    public float runningSpeed = 1.5f;
    public LayerMask groundLayer; //Esta variable sirve para detectar la capa del suelo (definida en Unity)
    public Animator animator; //Obtiene la animación y los parametros que contiene para los cambios de estado del personaje
    private Rigidbody2D rigidbody;

    //En el Awake se obtienen y configuran en el away
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start ()
    {
        //Arranca el estado inicial de la animación
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
        runningSpeed = 20f;
	}
	
	// Actualiza una vez por frame
	void Update ()
    {
        //Si debemos dejar que salte si el juego está en InGame
        if (GameManager.sharedInstance.currentGameState == GameState.IN_GAME)
        { 
                //Checa si se presionó Espacio para saltar
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0))
                {
                    Jump();
                }

            //Cambia el estado de la animación dependiendo si el personaje está en el aire o en el suelo
            animator.SetBool("isGrounded", IsTouchingTheGround());
        }
	}

    //Actualiza cada cierto tiempo fijo siempre (Parecido a Update pero en lugar de frames es por tiempo)
    private void FixedUpdate()
    {
        //Sólo se moverá si está en InGame
        if (GameManager.sharedInstance.currentGameState == GameState.IN_GAME)
        {
            //Saca la direccion a donde se presiona la tecla de movimiento y hace mover al personaje
            float horizontal = Input.GetAxis("Horizontal");
            rigidbody.AddForce(Vector2.right * runningSpeed * horizontal);

            //Al presionar la tecla de la flecha a la derecha se mueve el personaje
            /*if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //Verifica 
                if (rigidbody.velocity.x < runningSpeed)
                {
                    //Toma la velocidad de x y y
                    rigidbody.velocity = new Vector2(runningSpeed,  //Velocidad en el eje de las X 
                        rigidbody.velocity.y);                      //Velocidad en el eje de las Y
                }
            }

            //Al presionar la tecla de la flecha a la izquierda se mueve el personaje
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //Verifica 
                if (rigidbody.velocity.x > -runningSpeed)
                {
                    //Toma la velocidad de x y y
                    rigidbody.velocity = new Vector2(-runningSpeed,  //Velocidad en el eje de las X 
                        rigidbody.velocity.y);                      //Velocidad en el eje de las Y
                }
            }*/
        }
    }

    //Metodo que se encarga del salto
    private void Jump()
    {
        //Da el salto en el eje Y (Un impulso hacia el eje) F = ma =====> a = F / m
        if(IsTouchingTheGround())
            rigidbody.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
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
