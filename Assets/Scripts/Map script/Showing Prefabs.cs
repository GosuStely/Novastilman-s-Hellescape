using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowingPrefabs : MonoBehaviour
{
    public int floorRooms;
    public GameObject[] rooms;
    void Start()
    {
        VisualizePrefabs();
    }
    void VisualizePrefabs()
    {
        if (rooms != null)
        {
            //the position of our rooms
            Vector3 vector3 = new Vector3();
            vector3.x = 0;
            vector3.y = 0;
            vector3.z = 0;
            // we make rooms that are random from the GameObject array in the lenght of the floorRooms variable
            for (int i = 0; i <floorRooms; i++)
            {
                //we choose a random room from the array and put it in a variable of GameObject so we can instantiate it more clearly and debug it after that
                    GameObject currentRoom = rooms[Random.Range(0, rooms.Length - 1)];
                    Instantiate(currentRoom, vector3, Quaternion.identity);

                    Debug.Log(currentRoom);
                    Debug.Log(vector3);
                //we add 80 to the x of vector3 to make every toom to be 80 pixels apart on the right than the previous 
                    vector3.x += 80;
            }
        }
    }
}
