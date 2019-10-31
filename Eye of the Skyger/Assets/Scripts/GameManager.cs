using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Tooltip("Simulated Player Speed in m/s")]
    public float playerSpeed = 55.5f;

    [Tooltip("Distance of spawned objects (hidden by fog) to camera")]
    public float spawnDistance = 400f;

    public List<Obstacle> rocketTargets;
    public float targetTime;    // time it takes to focus a target -> shoot rocket

    public int rocketCount;
    public Rocket rocketPrefab;

    
    public UnityEvent StopSpawnersEvent, GameOverEvent, StopEnemySpawnersEvent;
    public bool gameOver, gameRunning;

    public float gameTimer; // time since beginning of game (since score began counting)
    public UnityEvent CalcScore;
    public bool gameStarted = false;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void PrepareGameOver()
    {
        StopSpawnersEvent.Invoke();
        StartCoroutine(WaitForLastSpawn());
    }

    IEnumerator WaitForLastSpawn()
    {
        /*
        HARD CODED TIME UNTIL THE LAST SPAWNED OBJECT HITS THE DEATH WALL
         */
        yield return new WaitForSeconds(13f);
        StopEnemySpawnersEvent.Invoke();

        yield return new WaitForSeconds(5f);
        gameOver = true;
        CalcScore?.Invoke();
        GameOverEvent?.Invoke();
    }

    private void Update()
    {
        if (!gameStarted)
        {
            return;
        }

        gameTimer += Time.deltaTime;
        if (gameTimer > 120f)
        {
            PrepareGameOver();
        }
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(GameManager))]
public class GameManagerInspector : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        if(GUILayout.Button("LOOOL"))
        {
            GameManager.instance.PrepareGameOver();
        }
    }
}
#endif
