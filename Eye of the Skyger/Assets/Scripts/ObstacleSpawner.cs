using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Obstacle obstaclePrefab;

    public float spawnFrequency = 2f;

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
            Vector3 spawnPos = Random.insideUnitCircle * 25;
            Instantiate(obstaclePrefab, spawnPos + new Vector3(0, 0, -50), Quaternion.identity, transform);

            timer -= spawnFrequency;
        }
    }
}
