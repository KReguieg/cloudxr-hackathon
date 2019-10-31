using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    
    float spawnFrequency = 1f;
    //public Vector2 maxSpawnBounds = new Vector3(1, 0.6f, 0), minSpawnBounds = Vector3.zero;
    public float minSpawnRadius = 0, maxSpawnRadius = 1;

    float timer;

    [Header("Spawn Transforms")]
    public bool randomRotationAroundYAxis;
    public bool randomRotationAroundXAxis, randomScale;
    public float minScale, maxScale;

    [Header("Spawn Frequency")]
    public float minSpawnFrequency;
    public float maxSpawnFrequency;

    public float overwriteSpawnDistance = -1;

    public bool disableOnGameOver = true;
    [HideInInspector]
    public bool spawning = true;


    private void Start()
    {
        if (disableOnGameOver)
            GameManager.instance.StopSpawnersEvent.AddListener(StopSpawning);
    }

    // Start is called before the first frame update
    void StopSpawning()
    {
        spawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= spawnFrequency && spawning)
        {
            float spawnDistance = GameManager.instance.spawnDistance;
            if (overwriteSpawnDistance > 0)
                spawnDistance = overwriteSpawnDistance;

            Vector3 spawnPos = Random.insideUnitCircle.normalized;
            spawnPos *= Random.Range(minSpawnRadius, maxSpawnRadius);
            spawnPos += transform.position;
            spawnPos += new Vector3(0, 0, spawnDistance);

            Quaternion spawnRotation = Quaternion.identity;

            if (randomRotationAroundYAxis)
                spawnRotation *= Quaternion.AngleAxis(Random.Range(0, 360f), Vector3.up);
            if (randomRotationAroundXAxis)
                spawnRotation *= Quaternion.AngleAxis(Random.Range(0, 360f), Vector3.forward);
            int prefabIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject newObject = Instantiate(obstaclePrefabs[prefabIndex], spawnPos, spawnRotation, transform);
            if (randomScale)
            {
                float desiredScale = Random.Range(minScale, maxScale);
                newObject.transform.localScale = 
                    Vector3.Scale(newObject.transform.localScale, new Vector3(desiredScale, desiredScale, desiredScale));
            }

            spawnFrequency = Random.Range(minSpawnFrequency, maxSpawnFrequency);
            timer = 0;
        }
    }
}
