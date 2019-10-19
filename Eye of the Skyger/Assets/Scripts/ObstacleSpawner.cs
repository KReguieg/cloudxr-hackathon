﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    
    float spawnFrequency = 1f;
    public Vector2 maxSpawnBounds = new Vector3(1, 0.6f, 0), minSpawnBounds = Vector3.zero;

    float timer;

    [Header("Spawn Transforms")]
    public bool randomRotationAroundYAxis;
    public bool randomRotationAroundXAxis, randomScale;
    public float minScale, maxScale;

    [Header("Spawn Frequency")]
    public float minSpawnFrequency;
    public float maxSpawnFrequency;

    public float overwriteSpawnDistance = -1;
    

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
            float spawnDistance = GameManager.singleton.spawnDistance;
            if (overwriteSpawnDistance > 0)
                spawnDistance = overwriteSpawnDistance;


            Vector3 spawnPos = new Vector3(
                Random.Range(minSpawnBounds.x, maxSpawnBounds.x),
                Random.Range(minSpawnBounds.y, maxSpawnBounds.y),
                spawnDistance);
            
            if (Random.Range(0, 2) == 0)
                spawnPos = Vector3.Scale(spawnPos, new Vector3(-1, 1, 1));
            if (Random.Range(0, 2) == 0)
                spawnPos = Vector3.Scale(spawnPos, new Vector3(1, -1, 1));

            Quaternion spawnRotation = Quaternion.identity;

            if (randomRotationAroundYAxis)
                spawnRotation *= Quaternion.AngleAxis(Random.Range(0, 360f), Vector3.up);
            if (randomRotationAroundXAxis)
                spawnRotation *= Quaternion.AngleAxis(Random.Range(0, 360f), Vector3.forward);

            GameObject newObject = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], spawnPos, spawnRotation, transform);
            if (randomScale)
            {
                float desiredScale = Random.Range(minScale, maxScale);
                newObject.transform.localScale = Vector3.Scale(newObject.transform.localScale, new Vector3(desiredScale, desiredScale, desiredScale));
            }

            spawnFrequency = Random.Range(minSpawnFrequency, maxSpawnFrequency);
            timer = 0;
        }
    }
}
