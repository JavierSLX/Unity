using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
    health, force
}

public class PlayerBar : MonoBehaviour
{
    private Slider slider;
    public BarType type;

	// Use this for initialization
	void Start ()
    {
        //Obtiene la barra que lo contiene
        slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch(type)
        {
            case BarType.health:
                slider.value = MarioController.getInstance.GetHealth();
                break;

            case BarType.force:
                slider.value = MarioController.getInstance.GetForce();
                break;
        }
	}
}
