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
    private SpriteRenderer sprite;
    private float altura;
    private int healthPoints;
    private int forcePoints;
    private bool movement;

    private void Awake()
    {
        getInstance = this;
        startPosition = this.transform.position;
        movement = true;
    }

    // Use this for initialization
    void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
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

            //Si la salud es igual a 0, el personaje muere
            if (healthPoints <= 0)
                Kill();
        }
    }

    //Metodo actualizado por tiempo
    private void FixedUpdate()
    {
        if (GameManager.getInstance.currentGameState == GameState.IN_GAME)
        {
            float direccion = Input.GetAxis("Horizontal");

            //Paraliza el movimiento
            if (!movement)
                direccion = 0;

            if (direccion > 0f)
            {
                if (rigidbody.velocity.x < speed)
                {
                    animator.SetBool("isMove", true);
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

        //Manda llamar la corutina
        StartCoroutine("TirePlayer");
    }

    //Corutina que le quita salud al personaje cada segundo (Corre por frames)
    IEnumerator TirePlayer()
    {
        while(this.healthPoints > 0)
        {
            this.healthPoints--;
            yield return new WaitForSeconds(1f);
        }

        yield return null;
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

        //Mata la corutina
        StopCoroutine("TirePlayer");
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
            rigidbody.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
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

    //Metodo que se llama cuando el enemigo provoca un retroceso en el personaje(daño)
    public void EnemyKnockBack(float enemyPosX)
    {
        //Devuelve -1, 0 o 1 dependiendo del lado donde resulte (Negativo - Izquierda, Positivo - Derecha)
        float side = Mathf.Sign(enemyPosX - transform.position.x);

        //Aplicamos la fuerza para provocar un movimiento en forma parabolica en la direccion contraria al enemigo
        rigidbody.AddForce(Vector2.left * side * jumpForce *0.5f, ForceMode2D.Impulse);
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        //Paraliza al personaje al tener daño
        movement = false;
        animator.SetBool("isHurt", true);
        Invoke("EnableMovement", 0.7f);

        //Cambia el color del sprite para simular daño
        Color color = new Color(255 / 255f, 106 / 255f, 0 / 255f);
        sprite.color = color;
    }

    //Recupera el movimiento y el color
    private void EnableMovement()
    {
        movement = true;
        animator.SetBool("isHurt", false);
        sprite.color = Color.white;
    }

    //Salta por pisar un elemento posible
    public void JumpByObject()
    {
        rigidbody.AddForce(Vector2.up * this.jumpForce * 1.75f, ForceMode2D.Impulse);
    }

    //Get y Set de health y force
    public int GetHealth()
    {
        return healthPoints;
    }

    public void SetHealth(int value)
    {
        healthPoints = value;
    }

    public int GetForce()
    {
        return forcePoints;
    }

    public void SetForce(int value)
    {
        forcePoints = value;
    }
}
