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
        AddLevelBlock();
    }

    //Agrega un nuevo bloque de nivel
    public void AddLevelBlock()
    {
        //Saca un numero aleatorio (0 <= x < b)
        int ramdomIndex = Random.Range(0, allTheLevelBlocks.Count);

        //Instanciamos el bloque actual de la lista por medio de una copia ya hecha
        LevelBlock currentBlock = (LevelBlock)Instantiate(allTheLevelBlocks[ramdomIndex]);

        //Lo añade como hijo de la jerarquia de LevelGenerator
        currentBlock.transform.SetParent(this.transform, false);

        //Para generar el escenario desde la posicion del centro
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

        //Operacion con vectores que corrige la operacion de los escenarios
        Vector3 correction = new Vector3(spawnPosition.x - currentBlock.startPoint.position.x,
            spawnPosition.y - currentBlock.startPoint.position.y, 0f);

        //Agrega la posicion y a la lista
        currentBlock.transform.position = correction;
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
