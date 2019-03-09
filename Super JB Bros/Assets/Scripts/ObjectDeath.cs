using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeath : MonoBehaviour 
{

	private void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.tag == "Player")
			MarioController.getInstance.Kill();
	}
}
