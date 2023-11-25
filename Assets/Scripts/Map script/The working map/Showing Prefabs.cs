using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowingPrefabs : MonoBehaviour
{
    public int floorRooms;
    public GameObject[] rooms;
    public GameObject teleportStart;

    private int roomNegativeX = -17;
    private int roomNegativeY = -10;
    private int roomX = 10;
    private int roomY = 10;

    void Start()
    {
        VisualizePrefabs();
    }
    void VisualizePrefabs()
    {
        if (rooms != null)
        {
            //the position of our rooms and teleports
            Vector3 vector3 = new Vector3(0,0,0);

            // we make teleports and rooms that are random from the GameObject array in the lenght of the floorRooms variable
            for (int i = 0; i <floorRooms; i++)
            {
                //we choose a random room from the array and put it in a variable of GameObject so we can instantiate it more clearly and debug it after that
                    GameObject currentRoom = rooms[Random.Range(0, rooms.Length - 1)];
                    Instantiate(currentRoom, vector3, Quaternion.identity);
                //we change vectors y so we can use it when we instantiate the teleport so we have a teleport in the current room but on the random place
                    vector3.y = Random.Range(roomNegativeY, roomY);
                // we add a random x variable that we are gonna use to have a random location of the teleport
                //and after instatiating we remove the changes we made for the teleporter
                    int teleporterX = Random.Range(roomNegativeX, roomX);
                    vector3.x += teleporterX;
                    Instantiate(teleportStart, vector3, Quaternion.identity);
                    vector3.x -= teleporterX;

                    Debug.Log(currentRoom);
                    Debug.Log(vector3);
                //we add 80 to the x of vector3 to make every toom to be 80 pixels apart on the right than the previous 
                    vector3.x += 80;
                    vector3.y = 0;
            }
        }
    }
}
