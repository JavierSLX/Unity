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

    //Contador de coleccionables
    public int collectedObject = 0;

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

        //Botón de salida del juego
        if (Input.GetKeyDown(KeyCode.Escape))
            ExitGame();
    }

    //Metodo encargado de iniciar el juego
    public void StartGame()
    {
        GameState estado = currentGameState;

        //Reinicia la posicion de Mario, la cámara y los bloques de los niveles
        MarioController.getInstance.StartGame();
        CameraFollowMario.getInstance.ResetCameraPosition();
        LevelGenerator.getInstance.RemoveAllTheBlocks();
        LevelGenerator.getInstance.GenerateInitialBlocks();

        SetGameState(GameState.IN_GAME);
        collectedObject = 0;
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

    //Metodo para finalizar la ejecucion del videojuego
    public void ExitGame()
    {
        //Una decision basada en la plataforma donde se va a ejecutar
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
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

    //Metodo que incrementa el valor del contador
    public void CollectObject(int objectValue)
    {
        this.collectedObject += objectValue;
        Debug.Log("Contador: " + collectedObject);
    }
}
