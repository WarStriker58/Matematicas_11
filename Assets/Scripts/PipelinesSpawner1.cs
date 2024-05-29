using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelinesSpawner1 : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 2f;
    public float gapSize = 2f;
    public float spawnHeight = 5f;
    public int spawnProbability = 1;

    private void Start()
    {
        InvokeRepeating(nameof(TrySpawnPipe), spawnRate, spawnRate);
    }

    private void TrySpawnPipe()
    {
        if (Random.Range(0, 2) < spawnProbability)
        {
            SpawnPipePair();
        }
    }

    private void SpawnPipePair()
    {
        Vector3 bottomPipePosition = new Vector3(transform.position.x, spawnHeight, transform.position.z);
        Vector3 topPipePosition = new Vector3(transform.position.x, spawnHeight + gapSize + pipePrefab.transform.localScale.y, transform.position.z);
        Instantiate(pipePrefab, bottomPipePosition, Quaternion.identity);
        Instantiate(pipePrefab, topPipePosition, Quaternion.Euler(0, 0, 180));
    }
}