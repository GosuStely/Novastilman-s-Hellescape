using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    //iterations are how many iterations we want to make to our randomWalk
    [SerializeField]
    private int iterations = 10;
    //walkLenght is passed to randomWalk
    [SerializeField]
    public int walkLenght = 10;
    [SerializeField]
    public bool startRandomlyEachIteration = true;
    [SerializeField]
    private TilemapVisualisator tilemapVisualisator;
    //method that get a HashSet of floor positions and call the paint method
    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tilemapVisualisator.Clear();
        tilemapVisualisator.PaintFloorTiles(floorPositions);
    }
    
    protected HashSet<Vector2Int> RunRandomWalk()
    {
        //declearing the current position
        var currentPosition = startPosition;
        //making HashSet and for each iteration we use simple walk method and add it to the hash set
        HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();
        for (int i = 0; i < iterations; i++)
        {
            var path = ProveduralGenerationAlgorithm.SimpleRandomWalk(currentPosition, walkLenght);
            // UnionWith add hashset to another hashset
            floorPosition.UnionWith(path);
            //changing the current position for starting to be the lenght of the hashset
            if (startRandomlyEachIteration)
            {
                currentPosition = floorPosition.ElementAt(Random.Range(0, floorPosition.Count));
            }
        }
        return floorPosition;

    }

}
