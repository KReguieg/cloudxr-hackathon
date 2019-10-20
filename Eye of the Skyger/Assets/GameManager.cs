using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [HideInInspector]
    public UnityEvent StopSpawnersEvent, GameOverEvent, StopEnemySpawnersEvent;
    public bool gameOver;

    public float gameTimer; // time since beginning of game (since score began counting)

    


    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;
    }

    

    public void PrepareGameOver()
    {
        StopSpawnersEvent.Invoke();
        StartCoroutine(WaitForLastSpawn());
    }

    IEnumerator WaitForLastSpawn()
    {
        int counter = 1;

        /*for (int i = 0; i < 1000; i++)
        {
            yield return new WaitForSeconds(1);
            Debug.Log("waited: " + counter);
            counter++;
        }*/

        yield return new WaitForSeconds(13f);
        StopEnemySpawnersEvent.Invoke();

        yield return new WaitForSeconds(5f);
        gameOver = true;
        GameOverEvent.Invoke();
    }

    private void Update()
    {
        gameTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.G))
            PrepareGameOver();

        if (gameTimer > 120f)
            PrepareGameOver();

    }

}
