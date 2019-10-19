using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Obstacle obstaclePrefab;

    public float spawnFrequency = 1f;
    public Vector2 maxSpawnBounds = new Vector3(1, 0.6f, 0), minSpawnBounds = Vector3.zero;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= spawnFrequency)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(minSpawnBounds.x, maxSpawnBounds.x),
                Random.Range(minSpawnBounds.y, maxSpawnBounds.y),
                GameManager.singleton.spawnDistance);
            
            if (Random.Range(0, 2) == 0)
                spawnPos = Vector3.Scale(spawnPos, new Vector3(-1, 1, 1));
            if (Random.Range(0, 2) == 0)
                spawnPos = Vector3.Scale(spawnPos, new Vector3(1, -1, 1));

            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity, transform);

            timer -= spawnFrequency;
        }
    }
}
