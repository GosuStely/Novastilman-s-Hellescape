using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnManager : MonoBehaviour
{
    public GameObject[] prefabList; // The prefab you want to spawn
    public Transform[] spawnPoints; // List of potential spawn points

    void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            SpawnPrefabAtRandomLocation(spawnPoints[i]);
        }
    }

    void SpawnPrefabAtRandomLocation(Transform spawnPoint)
    {
        GameObject prefabToSpawn = prefabList[Random.Range(0, prefabList.Length - 1)];
        if (prefabToSpawn == null)
        {
            Debug.LogError("Prefab to spawn is not assigned.");
            return;
        }
        // Select a random spawn point
        Transform randomSpawnPoint = spawnPoint;

        // Instantiate the prefab at the selected spawn point inside the room
        Instantiate(prefabToSpawn, randomSpawnPoint.position, randomSpawnPoint.rotation, transform);
    }
}
