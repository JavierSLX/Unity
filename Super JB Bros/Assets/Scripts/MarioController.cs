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
    private bool isMove;

    private void Awake()
    {
        getInstance = this;
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Jump();

        if (IsTouchingTheGround())
            animator.SetBool("isGround", true);
        else
            animator.SetBool("isGround", false);
    }

    //Metodo actualizado por tiempo
    private void FixedUpdate()
    {
        float direccion = Input.GetAxis("Horizontal");
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

        if(direccion == 0f)
            animator.SetBool("isMove", false);

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
}
