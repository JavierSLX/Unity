using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public Transform target;    //Donde obtenemos la posicion de nuestro objetivo hacia donde se moverá
    public float speed; //La velocidad a la que se moverá la plataforma
    private Vector3 start, end; //Guarda las posiciones de la plataforma y el target

	// Use this for initialization
	void Start ()
    {
		//Checamos si el tarjet se definio en Unity
        if(target != null)
        {
            //Hace que target ya no sea hijo de platform
            target.parent = null;

            //Guarda las posiciones
            start = transform.position;
            end = target.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        //Si se definió el target desde Unity
        if(target != null)
        {
            //Se cambia hacia donde se moverá la plataforma con un vector
            float fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);
        }

        //Se cambian las posiciones cuando estas se igualan
        if(transform.position == target.position)
        {
            target.position = target.position == start ? end : start;
        }
    }
}
