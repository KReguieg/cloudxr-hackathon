using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    [Tooltip("Simulated Player Speed in m/s")]
    public float playerSpeed = 55.5f;

    [Tooltip("Distance of spawned objects (hidden by fog) to camera")]
    public float spawnDistance = 400f;

    public List<Obstacle> rocketTargets;
    public float targetTime;    // time it takes to focus a target -> shoot rocket

    public int rocketCount;
    public Rocket rocketPrefab;


    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;
    }

}
