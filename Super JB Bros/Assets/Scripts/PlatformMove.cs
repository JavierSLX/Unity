using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public Transform target;
    public Transform origin;
    public float speed;
    private Vector3 start, end;

    private void Start()
    {
        if(origin != null && target != null)
        {
            start = origin.position;
            end = target.position;
        }
    }

    private void FixedUpdate()
    {
        if(origin != null && target != null)
        {
            float fixedSpeed = speed * Time.deltaTime;
            origin.position = Vector3.MoveTowards(origin.position, target.position, fixedSpeed);
        }

        //Se cambian las posiciones cuando estas se igualan
        if(origin.position == target.position)
        {
            target.position = target.position == start ? end : start;
        }
    }
}
