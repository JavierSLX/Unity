using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour
{
    //Las etiquetas de la pantalla
    public Text collectableLabel;
    public Text scoreLabel;
    public Text maxScoreLabel;

	// Update is called once per frame
	void Update ()
    {
        //Si el estado del juego esta IN GAME se actualiza el contador de monedas
		if(GameManager.getInstance.currentGameState == GameState.IN_GAME)
        {
            int currentObjects = GameManager.getInstance.collectedObject;
            this.collectableLabel.text = currentObjects.ToString();
        }
	}
}
