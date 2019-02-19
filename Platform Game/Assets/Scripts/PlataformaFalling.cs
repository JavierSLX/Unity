using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaFalling : MonoBehaviour
{
    public float fallDelay = 1f;    //El tiempo sobre el que el jugador puede estar sobre la plataforma antes de caerse
    public float respawnDelay = 5f; //Después de este tiempo la plataforma vuelve a aparecer
    private Rigidbody2D rigidbody;  //Obtenemos las reglas fisicas de la plataforma
    private PolygonCollider2D polygonCollider;  //Obtenemos el collider de la plataforma
    private Vector3 start;  //La posición inicial de la plataforma

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        start = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Le afecta la gravedad a la plataforma cuando el jugador la toca
        if(collision.gameObject.CompareTag("Player"))
        {
            //Llama al metodo después de un retraso
            Invoke("Fall", fallDelay);

            //Llama a la restauración de la plataforma
            Invoke("Respawn", fallDelay + respawnDelay);
        }
    }

    private void Fall()
    {
        rigidbody.isKinematic = false;

        //Se desactiva temporalmente la colision
        polygonCollider.isTrigger = true;
    }

    private void Respawn()
    {
        //Regresa la plataforma a la posición original
        transform.position = start;

        //Le quita la velocidad y le vuelve a poner kinematic (para que no le afecte la gravedad)
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector3.zero;

        //Activa de nuevo las colisiones
        polygonCollider.isTrigger = false;
    }
}
