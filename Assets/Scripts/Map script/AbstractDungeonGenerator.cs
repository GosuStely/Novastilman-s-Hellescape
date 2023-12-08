using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    //we use protected so the only child elements can have access to these properties
    [SerializeField]
    protected TilemapVisualisator tilemapVisualisator = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        tilemapVisualisator.Clear();
        RunProceduralGeneration();
    }
    protected abstract void RunProceduralGeneration();
}
