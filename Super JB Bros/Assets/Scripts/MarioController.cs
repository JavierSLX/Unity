using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    public static MarioController getInstance;
    public float speed = 3f;
    public float jumpForce = 6.5f;
    public LayerMask groundLayer;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private Vector3 startPosition;
    private float altura;
    private int healthPoints;
    private int forcePoints;

    private void Awake()
    {
        getInstance = this;
        startPosition = this.transform.position;
    }

    // Use this for initialization
    void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManager.getInstance.currentGameState == GameState.IN_GAME)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                Jump();

            //Saca la altura maxima del del personaje
            if (IsTouchingTheGround())
            {
                animator.SetBool("isGround", true);
                altura = transform.position.y;
                rigidbody.gravityScale = 1;
            }
            else
            {
                animator.SetBool("isGround", false);
                if (transform.position.y > altura)
                    altura = transform.position.y;
                else
                    rigidbody.gravityScale = 1;
            }
        }
    }

    //Metodo actualizado por tiempo
    private void FixedUpdate()
    {
        if (GameManager.getInstance.currentGameState == GameState.IN_GAME)
        {
            float direccion = Input.GetAxis("Horizontal");
            if (direccion > 0f)
            {
                if (rigidbody.velocity.x < speed)
                {
                    animator.SetBool("isMove", true);
                    float currentSpeed = (speed - 0.5f) * this.healthPoints / 100f;
                    rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }

            if (direccion < 0f)
            {
                if (rigidbody.velocity.x > -speed)
                {
                    animator.SetBool("isMove", true);
                    rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
            }

            if (direccion == 0f)
                animator.SetBool("isMove", false);
        }

    }

    //Comienza el juego (Es creado por el programador
    public void StartGame()
    {
        this.transform.position = startPosition;
        animator.SetBool("isLive", true);
        animator.SetBool("isGround", true);

        //Le da energia y fuerza al jugador
        this.healthPoints = 100;
        this.forcePoints = 10;
    }

    //Mata al jugador
    public void Kill()
    {
        GameManager.getInstance.GameOver();
        animator.SetBool("isLive", false);

        //Checa la puntuacion maxima y si es inferior lo guarda en preferencias
        float currentMaxScore = PlayerPrefs.GetFloat("maxscore", 0f);
        if (currentMaxScore < this.GetDistanceX())
            PlayerPrefs.SetFloat("maxscore", this.GetDistanceX());
    }

    //Checa si el personaje está tocando el suelo
    private bool IsTouchingTheGround()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, groundLayer);
    }

    //Realiza el salto
    private void Jump()
    {
        //Da un salto en el eje Y
        if (IsTouchingTheGround())
        {
            if (this.forcePoints > 5)
            {
                this.forcePoints -= 5;
                rigidbody.AddForce(Vector2.up * this.jumpForce * 1.5f, ForceMode2D.Impulse);
            }
            else
            {
                rigidbody.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    //Regresa la distancia recorrida en el eje X del personaje a partir de su posicion inicial
    public float GetDistanceX()
    {
        return Vector2.Distance(new Vector2(startPosition.x, 0), new Vector2(this.transform.position.x, 0));
    }

    //Agrega vida al personaje
    public void CollectHealth(int points)
    {
        this.healthPoints += points;

        //Condicion si se rebasa el limite de vida
        if (this.healthPoints >= 150)
            this.healthPoints = 150;
    }

    //Agrega fuerza al personaje
    public void CollectForce(int points)
    {
        this.forcePoints += points;

        if (this.forcePoints >= 25)
            this.forcePoints = 25;
    }
}
