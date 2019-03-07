using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //A quien seguirá la camara
    public Vector3 offset = new Vector3(0.2f, 0.0f, -10f); //La distancia a seguirlo
    public float dampTime = 0.05f; //El tiempo en el que se comienza a seguir
    private Vector3 velocity = Vector3.zero; //La velocidad de la camara

    private void Awake()
    {
        Application.targetFrameRate = 60; //Intenta renderizar a 60 Frames por segundo
    }

    // Update is called once per frame
    private void Update()
    { 
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position); //Unimos la camara con el personaje (Segun el ViewPort)
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));    //Lo que se tiene que mover la camara
        Vector3 destination = point + delta;    //Hacia donde se tiene que mover la camara
        destination = new Vector3(destination.x, offset.y, offset.z);  //La camara solo se moverá en X

        this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampTime);   //Mueve la camara a su destino
	}
}
