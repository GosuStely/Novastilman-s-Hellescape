using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProveduralGenerationAlgorithm : MonoBehaviour
{
    //HashSet is a collection of unique values (if we have 1 tile twice it will save it once)
public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLenght)
    {
        //startPosition is where we start and walkLenght is how much are we gonna walk before finishing
        //Making Hashset to return
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        //adding the first tile
        path.Add(startPosition);
        //making a property that we will add in the set later on
        var previousPosition = startPosition;

        for (int i = 0; i < walkLenght; i++)
        {
            // get a new position based on the previus position + random direction and adding it to the set
            var newPosition = previousPosition + Direction2D.GetRandomCordinalDirection();
            path.Add(newPosition);
            //updating the previous position
            previousPosition = newPosition;
        }
        //returning the set
        return path;
    }
}

public static class Direction2D
{
    //making a list with all the directions 
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(0,-1), //DOWN
        new Vector2Int(-1,0)  //LEFT
    };
    //making a method that returns random direction
    public static Vector2Int GetRandomCordinalDirection()
    {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
    }
}
