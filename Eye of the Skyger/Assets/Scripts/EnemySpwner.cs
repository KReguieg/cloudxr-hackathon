using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwner : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] float spawnrate = 5f;
    [SerializeField] float spawnZ = -5f;
    [SerializeField] Transform PlayerTransform;
    [SerializeField] Camera linkedCamera;
    
    float timer = 0;
    bool spawning = true;

    public float initialStartDelay = 45f;

    private void Start()
    {
        GameManager.instance.StopEnemySpawnersEvent.AddListener(StopSpawner);
    }

    void StopSpawner()
    {

    }

    void Update()
    {
        if (spawning && GameManager.instance.gameTimer > initialStartDelay)
        {
            timer += Time.deltaTime;
            if (timer >= spawnrate)
            {
                timer = 0;
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        GameObject newEnemy = Instantiate(EnemyPrefab, linkedCamera.transform);
        newEnemy.transform.localPosition = Vector3.back * spawnZ;
        
        newEnemy.transform.SetParent(transform);
        
        Enemy enemy = newEnemy.GetComponent<Enemy>();
        enemy.playerLink = PlayerTransform;
        
    }
}
