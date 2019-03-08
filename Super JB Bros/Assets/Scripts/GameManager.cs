using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Los estados del juego en un enum
public enum GameState
{
    MENU, IN_GAME, GAME_OVER
}

//Clase Singleton
public class GameManager : MonoBehaviour
{
    //Objeto que referencia al propio GameManager
    public static GameManager getInstance;

    //Variable para saber en que estado del juego nos encontramos, al inicio estará en el menu principal
    public GameState currentGameState = GameState.MENU;

    //El Canvas donde se encuentran los menús del juego
    public Canvas menuCanvas, gameCanvas, gameOverCanvas;

    private void Awake()
    {
        getInstance = this;
    }
    private void Start()
    {
        BackToMenu();
    }

    private void Update()
    {
        //Si se presiona un boton configurado llamado "Start" comienza el juego (Esto se hace para realizar multiplataforma)
        //Hay que especificar en el Input Manager en Unity (Edit->Project Settings->Input)
        if (Input.GetButtonDown("Start") && currentGameState != GameState.IN_GAME)
            StartGame();

        //Botón de pausa
        if (Input.GetButtonDown("Pause"))
            BackToMenu();
    }

    //Metodo encargado de iniciar el juego
    public void StartGame()
    {
        GameState estado = currentGameState;

        SetGameState(GameState.IN_GAME);
        MarioController.getInstance.StartGame();

        //Si se inicia el juego desde un Game Over reinicia los niveles y la camara
        if (estado == GameState.GAME_OVER)
        {
            CameraFollowMario.getInstance.ResetCameraPosition();
            LevelGenerator.getInstance.RemoveAllTheBlocks();
            LevelGenerator.getInstance.GenerateInitialBlocks();
        }
    }

    //Metodo que se llamará cuando el jugador muera
    public void GameOver()
    {
        SetGameState(GameState.GAME_OVER);
    }

    //Metodo para volver al menu principal cuando el usuario lo quiera hacer
    public void BackToMenu()
    {
        SetGameState(GameState.MENU);
    }

    //Metodo encargado de cambiar el estado del juego
    private void SetGameState(GameState gameState)
    {
        switch(gameState)
        {
            //Escena de Unity para mostrar el menú
            case GameState.MENU:
                menuCanvas.enabled = true;
                gameCanvas.enabled = false;
                gameOverCanvas.enabled = false;
                break;

            //Escena de Unity para mostrar el juego
            case GameState.IN_GAME:
                menuCanvas.enabled = false;
                gameCanvas.enabled = true;
                gameOverCanvas.enabled = false;
                break;

            //Escena de Unity para mostrar el fin del juego
            case GameState.GAME_OVER:
                menuCanvas.enabled = false;
                gameCanvas.enabled = false;
                gameOverCanvas.enabled = true;
                break;
        }

        //Asignamos el estado del juego
        this.currentGameState = gameState;
    }
}
