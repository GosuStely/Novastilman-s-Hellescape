using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowingPrefabs : MonoBehaviour
{
    public GameObject tutorialRoom;
    public int floorRooms;
    public GameObject[] FloorOne;
    public GameObject[] FloorTwo;
    public GameObject[] FloorThree;
    public GameObject[] FloorFour;
    public GameObject[] FloorFive;
    public GameObject[] FloorSix;
    public GameObject[] FloorSeven;
    public GameObject[] FloorEight;
    public GameObject[] FloorNine;
    public GameObject teleportStart;

    private int roomNegativeX = -17;
    private int roomNegativeY = -9;
    private int roomX = 17;
    private int roomY = 9;
    bool isShopRoom = false;
    bool isTutorial = false;
    Vector3 vector3 = new Vector3(0, 0, 0);

    void Start()
    {
        FloorVisulizer();
    }
    public void FloorVisulizer()
    {
        // for the next 9 loops each one generated the said floor
        for (int i = 1; i <= 9; i++)
        {
            switch (i)
            {
                case 1:
                    VisualizePrefabs(FloorOne);
                    break;
                case 2:
                    VisualizePrefabs(FloorTwo);
                    break;
                case 3:
                    VisualizePrefabs(FloorThree);
                    break;
                case 4:
                    VisualizePrefabs(FloorFour);
                    break;
                case 5:
                    VisualizePrefabs(FloorFive);
                    break;
                case 6:
                    VisualizePrefabs(FloorSix);
                    break;
                case 7:
                    VisualizePrefabs(FloorSeven);
                    break;
                case 8:
                    VisualizePrefabs(FloorEight);
                    break;
                case 9:
                    VisualizePrefabs(FloorNine);
                    break;
            }
        }
    }
    public void VisualizePrefabs(GameObject[] rooms)
    {
        if (rooms != null || rooms.Length != 0)
        {

            //the position of our rooms and teleports
            // we make teleports and rooms that are random from the GameObject array in the lenght of the floorRooms variable
            for (int i = 0; i < floorRooms; i++)
            {

                showRooms(i, vector3, rooms);
                showTeleports(vector3);
                vector3.x += 80;
                vector3.y = 0;
            }
        }
    }

    void showRooms(int i, Vector3 vector3, GameObject[] rooms)
    {

        //we choose a random room from the array and put it in a variable of GameObject so we can instantiate it more clearly and debug it after that
        GameObject currentRoom = new GameObject();
        //if the room is the shop room after choosing it you cant get it second time because is zero or once per floor room
        //you cant get the room as first room
        if (!isShopRoom && i != 0)
        {
            currentRoom = rooms[Random.Range(1, rooms.Length - 2)];
        }
        else
        {
            currentRoom = rooms[Random.Range(1, rooms.Length - 2)];
        }
        if (!isTutorial && i == 0)
        {
            currentRoom = tutorialRoom;
            isTutorial = true;
        }

        if (i == 4 || i == 8)
        {
            currentRoom = rooms[0];
        }
        if (i == floorRooms - 1)
        {
            currentRoom = rooms[rooms.Length - 1];
        }
        Instantiate(currentRoom, vector3, Quaternion.identity);
    }
    void showTeleports(Vector3 vector3)
    {
        //we change vectors y so we can use it when we instantiate the teleport so we have a teleport in the current room but on the random place
        vector3.y = Random.Range(roomNegativeY, roomY);
        // we add a random x variable that we are gonna use to have a random location of the teleport
        //and after instatiating we remove the changes we made for the teleporter
        int teleporterX = Random.Range(roomNegativeX, roomX);
        vector3.x += teleporterX;
        Instantiate(teleportStart, vector3, Quaternion.identity);
        vector3.x -= teleporterX;
        //we add 80 to the x of vector3 to make every toom to be 80 pixels apart on the right than the previous 

    }
}
