using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float runningSpeed = 1.5f;
    public LayerMask groundLayer;
    public Animator animator;
    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start ()
    {
        animator.Play("Idle");
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (!IsTouchingTheGround())
            animator.Play("Jump");
        else
            animator.Play("Idle");
    }

    //Metodo que se encarga del salto
    private void Jump()
    {
        if (IsTouchingTheGround())
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    //Checa si el personaje está tocando el suelo
    private bool IsTouchingTheGround()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, 0.1f, groundLayer);
    }
}
