using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadObject : MonoBehaviour
{
    public GameObject objeto;
    private Vector3 posicion;

	// Use this for initialization
	void Start ()
    {
        posicion = objeto.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Si el objeto no está en camara lo regresa a su posicion original
        if (!CameraFollowMario.getInstance.IsObjectInCamera(objeto.transform))
            objeto.transform.position = posicion;
	}

    
}
