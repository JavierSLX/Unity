using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //Variable para saber si el coleccionable ha sido recogido
    private bool isCollected = false;
    public int value = 0;

    private void Awake()
    {
        switch(this.tag)
        {
            case "Moneda":
                value = 1;
                break;

            case "Estrella":
                value = 5;
                break;

            case "MonedaEstrella":
                value = 10;
                break;
        }
    }

    //Metodo para activar la moneda y su collider
    private void Show()
    {
        //Activamos la imagen del coleccionable (y de rebote tambien la animacion) y el collider
        if (this.tag == "MonedaEstrella")
        {
            //Obtiene los Sprites de los hijos del GameObject
            SpriteRenderer[] childs = this.GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < childs.Length; i++)
                childs[i].enabled = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
        this.GetComponent<CircleCollider2D>().enabled = true;
        isCollected = false;
    }

    //Metodo para desactivar el coleccionable y su collider
    private void Hide()
    {
        //Desactivamos la imagen del coleccionable (y de rebote tambien la animacion) y el collider
        if (this.tag == "MonedaEstrella")
        {
            //Obtiene los Sprites de los hijos del GameObject
            SpriteRenderer[] childs = this.GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < childs.Length; i++)
                childs[i].enabled = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
        this.GetComponent<CircleCollider2D>().enabled = false;
    }

    //Metodo para recolectar el coleccionable
    private void Collect()
    {
        isCollected = true;
        GameManager.getInstance.CollectObject(value);
        Hide();
    }

    //Cuando colisiona el coleccionable con un elemento
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //El jugador recolecta el coleccionable
        if(collision.tag == "Player")
        {
            Collect();
        }
    }
}
