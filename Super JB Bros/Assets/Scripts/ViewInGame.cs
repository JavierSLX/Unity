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
        //Si el estado del juego esta IN GAME se actualiza el contador de monedas y la distancia recorrida
		if(GameManager.getInstance.currentGameState == GameState.IN_GAME)
        {
            int currentObjects = GameManager.getInstance.collectedObject;
            this.collectableLabel.text = currentObjects.ToString();

            float travelledDistance = MarioController.getInstance.GetDistanceX();

            //Distancia con 2 decimales
            this.scoreLabel.text = "Score\n" + travelledDistance.ToString("f2");

            //Obtiene el puntaje maximo
            float maxscore = PlayerPrefs.GetFloat("maxscore", 0f);
            this.maxScoreLabel.text = "Max Score\n" + maxscore.ToString("f2");
        }

        if(GameManager.getInstance.currentGameState == GameState.GAME_OVER)
        {
            int currentObjects = GameManager.getInstance.collectedObject;
            this.collectableLabel.text =  currentObjects.ToString();
        }
	}
}
