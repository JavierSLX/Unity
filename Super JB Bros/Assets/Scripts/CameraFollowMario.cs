﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script Singleton para la camara
public class CameraFollowMario : MonoBehaviour
{
    public static CameraFollowMario getInstance;
    public Transform target;
    public float smoothTime = 0.3f;
    private Vector2 velocity;
    private float posY;

    private void Awake()
    {
        getInstance = this;
        posY = target.position.y + 2f;
    }

    private void FixedUpdate()
    {
        //Mueve la camara solo cuando el personaje alcanzo el centro de la camara
        if (target.position.x > 0)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothTime);
            //float posY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smoothTime);

            //Cambia la posicion de la camara
            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }

    public void ResetCameraPosition()
    {
        //Cambia la posicion de la camara
        transform.position = new Vector3(0f, posY, transform.position.z);
    }
}
