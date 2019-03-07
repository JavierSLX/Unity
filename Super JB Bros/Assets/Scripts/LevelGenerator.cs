using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script Singleton para generar trozos de nivel
public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator getInstance;
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>(); //Una lista de todos los bloques de nivel
    public Transform levelStartPoint; //Donde se encuentra el punto de inicio
    public List<LevelBlock> currentBlocks = new List<LevelBlock>(); //Los bloques que estan en la escena


    private void Awake()
    {
        getInstance = this;
    }

    private void Start()
    {
        AddLevelBlock();
    }

    //Agrega un nuevo bloque de nivel
    public void AddLevelBlock()
    {
        //Saca un numero aleatorio
        int ramdomIndex = Random.Range(0, allTheLevelBlocks.Count);

        //Instanciamos el bloque actual de la lista
        LevelBlock currentBlock = (LevelBlock)Instantiate(allTheLevelBlocks[ramdomIndex]);

        //Lo añade como hijo e la jerarquia
        currentBlock.transform.SetParent(this.transform, false);

        //Para generar el escenario desde el centro
        Vector3 spawnPosition = Vector3.zero;

        //Si es el primero
        if(currentBlocks.Count == 0)
        {
            spawnPosition = levelStartPoint.position;
        }
        else
        {
            spawnPosition = currentBlocks[currentBlocks.Count - 1].exitPoint.position;
        }

        //Agrega la posicion y a la lista
        currentBlock.transform.position = spawnPosition;
        currentBlocks.Add(currentBlock);
    }

    //Elimina el bloque más viejo
    public void RemoveOldestLevelBlock()
    {

    }

    //Quita todos los bloques del nivel
    public void RemoveAllTheBlocks()
    {

    }

    //Genera los bloques iniciales del juego
    public void GenerateInitialBlocks()
    {

    }
}
