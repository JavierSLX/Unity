using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject follow;   //Hacemos referencia desde Unity al objeto que se quiere seguir con la camara
    public Vector2 minCamPos, maxCamPos;    //Definimos la posicion minima y máxima a la que puede estar la camara definidas en Unity
    public float smoothTime;    //Segundos de retardo o suavizado del movimiento de la camara
    private Vector2 velocity;

	// Use this for initialization
	void Start () {
		
	}

    //Se ejecuta independientemente del frame rate
    private void FixedUpdate()
    {
        //Toma las posiciones suavizadas de cada eje
        float posX = Mathf.SmoothDamp(transform.position.x, follow.transform.position.x, ref velocity.x, smoothTime);
        float posY = Mathf.SmoothDamp(transform.position.y, follow.transform.position.y, ref velocity.y, smoothTime);

        //Cambia la posicion de la camara al objeto que se le indicó seguir
        transform.position = new Vector3(
            Mathf.Clamp(posX, minCamPos.x, maxCamPos.x),
            Mathf.Clamp(posY, minCamPos.y, maxCamPos.y),
            transform.position.z);
    }
}
