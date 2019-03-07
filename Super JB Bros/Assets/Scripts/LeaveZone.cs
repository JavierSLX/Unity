using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveZone : MonoBehaviour
{ 
    //Cuando entra un elemento con la zona
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelGenerator.getInstance.AddLevelBlock();
        LevelGenerator.getInstance.RemoveOldestLevelBlock();
    }
}
