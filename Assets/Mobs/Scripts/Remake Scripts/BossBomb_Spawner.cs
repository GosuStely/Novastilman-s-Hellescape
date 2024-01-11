using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBomb_Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] bombPrefabs;
    public float waveInterval = 10f;  // Time between waves
    public float waveDuration = 6f;   // Duration of each wave
    public float spawnInterval = 0.5f; // Adjust this to control the spawn rate within each wave
    public float bombDuration = 1f;  // Adjust this to control how long bombs persist

    void Start()
    {
        StartCoroutine(SpawnBombWaves());
    }

    IEnumerator SpawnBombWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(waveDuration);

            // Spawn bombs for the current wave
            StartCoroutine(SpawnBombsContinuously());
            yield return new WaitForSeconds(waveDuration);

            // Wait for the next wave
            yield return new WaitForSeconds(waveInterval - waveDuration);
        }
    }

    IEnumerator SpawnBombsContinuously()
    {
        float waveEndTime = Time.time + waveDuration;

        while (Time.time < waveEndTime)
        {
            SpawnRandomBomb();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnRandomBomb()
    {
        int randBomb = Random.Range(0, bombPrefabs.Length);
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);

        GameObject bomb = Instantiate(bombPrefabs[randBomb], spawnPoints[randSpawnPoint].position, Quaternion.identity);

        // Start a coroutine to destroy the bomb after a certain duration
        StartCoroutine(DestroyAfterDuration(bomb, bombDuration));
    }

    IEnumerator DestroyAfterDuration(GameObject obj, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(obj);
    }
}
