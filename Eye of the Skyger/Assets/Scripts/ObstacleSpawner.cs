using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Obstacle obstaclePrefab;

    public float spawnFrequency = 1f, spawnDistance = 200;
    public Vector2 spawnBounds = new Vector3(10, 7, 0);

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
            Instantiate(obstaclePrefab, new Vector3(
                Random.Range(-spawnBounds.x, spawnBounds.x), 
                Random.Range(-spawnBounds.y, spawnBounds.y), 
                spawnDistance), Quaternion.identity, transform);

            timer -= spawnFrequency;
        }
    }
}
