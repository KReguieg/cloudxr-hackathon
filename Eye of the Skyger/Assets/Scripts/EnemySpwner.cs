using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwner : MonoBehaviour
{
    [SerializeField] GazeManager gazeManager;
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] float spawnrate = 5f;
    [SerializeField] float spawnZ = -5f;
    [SerializeField] Transform PlayerTransform;
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnrate)
        {
            timer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject newEnemy = Instantiate(EnemyPrefab, transform);
        newEnemy.transform.localPosition = Vector3.back * spawnZ;
        Enemy enemy = newEnemy.GetComponent<Enemy>();
        enemy.playerLink = PlayerTransform;
        ObjectToGaze gaze = newEnemy.GetComponent<ObjectToGaze>();
        gazeManager.Gazeables.Add(gaze);
    }
}
