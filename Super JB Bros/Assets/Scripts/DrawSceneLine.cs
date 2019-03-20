using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSceneLine : MonoBehaviour
{
    public Transform origin;
    public Transform to;

    private void OnDrawGizmosSelected()
    {
        //Dibuja la linea
        if(origin != null && to != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(origin.position, to.position);
            Gizmos.DrawSphere(origin.position, 0.15f);
            Gizmos.DrawSphere(to.position, 0.15f);
        }
    }
}
