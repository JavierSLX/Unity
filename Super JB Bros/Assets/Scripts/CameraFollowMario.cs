using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMario : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    private Vector2 velocity;
    private float posY;
    private float minPos, maxPos;

    private void Awake()
    {
        posY = target.position.y + 2f;
    }

    private void Start()
    {
        List<LevelBlock> blocks = LevelGenerator.getInstance.currentBlocks;
        minPos = blocks[0].startPoint.transform.position.x;
        maxPos = blocks[blocks.Count - 1].exitPoint.transform.position.x;
    }

    private void FixedUpdate()
    {
        //Mueve la camara solo cuando el personaje alcanzo el centro de la camara
        if (target.position.x > 0)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothTime);
            //float posY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smoothTime);

            //Cambia la posicion de la camara
            transform.position = new Vector3(Mathf.Clamp(posX, minPos, maxPos), posY, transform.position.z);
            Debug.Log("X: " + target.position.x);
        }
    }
}
